using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.BusinessEntities.Security;
using System.Globalization;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.DesignByContract;

namespace Bagge.Seti.WebSite.Presenters
{
	public class HomePresenter
	{
		IHomeView _view;
		ITicketManager _ticketManager;
		ITicketStatusManager _ticketStatusManager;
		IFunctionManager _functionManager;
		IUser _user;

		public HomePresenter(IHomeView view, ITicketManager ticketManager, ITicketStatusManager ticketStatusManager, 
			IFunctionManager functionManager, IUser user,
			string weeklyDateFormat, string monthlyDateFormat)
		{
			Check.Require(view != null);
			Check.Require(ticketManager != null);
			Check.Require(ticketStatusManager != null);
			Check.Require(functionManager != null);
			Check.Require(user != null);
			Check.Require(!string.IsNullOrEmpty(weeklyDateFormat));
			Check.Require(!string.IsNullOrEmpty(monthlyDateFormat));

			_view = view;
			_view.Init += new EventHandler(OnInit);
			_view.Load += new EventHandler(OnLoad);
			_ticketManager = ticketManager;
			_ticketStatusManager = ticketStatusManager;
			_functionManager = functionManager; 
			_user = user;
			MonthDisplayFormat = monthlyDateFormat;
			WeekDisplayFormat = weeklyDateFormat;
		}

		protected virtual void OnInit(object sender, EventArgs e)
		{
			_view.PreviewDateSelected += new EventHandler(_view_PreviewDateSelected);
			_view.NextDateSelected += new EventHandler(_view_NextDateSelected);
			_view.DisplayFormatChanged += new EventHandler(_view_DisplayFormatChanged);
		}

		void _view_DisplayFormatChanged(object sender, EventArgs e)
		{
			BindData();
		}

		void _view_NextDateSelected(object sender, EventArgs e)
		{
			if (_view.SelectedDisplayFormat == HomeViewDisplayFormat.Monthly)
				_view.CurrentDate = _view.CurrentDate.AddMonths(1);
			else
				_view.CurrentDate = _view.CurrentDate.AddDays(7);

			BindData();
		}

		void _view_PreviewDateSelected(object sender, EventArgs e)
		{
			if (_view.SelectedDisplayFormat == HomeViewDisplayFormat.Monthly)
				_view.CurrentDate = _view.CurrentDate.AddMonths(-1);
			else
				_view.CurrentDate = _view.CurrentDate.AddDays(-7);

			BindData();
		}

		protected virtual void OnLoad(object sender, EventArgs e)
		{
			if (!_view.IsPostBack)
			{
				_view.CurrentDate = DateTime.Now;
				BindLegends();
				BindData();
			}
		}

		private void BindLegends()
		{
			_view.Legends = _ticketStatusManager.FindAll();
		}

		private string MonthDisplayFormat
		{
			get;
			set;
		}

		private string WeekDisplayFormat
		{
			get;
			set;
		}


		private void BindData()
		{
			DateTime start, end;

			if (_view.SelectedDisplayFormat == HomeViewDisplayFormat.Weekly)
			{
				DayOfWeek dayOfWeek = _view.CurrentDate.DayOfWeek;
				start = _view.CurrentDate;
				int days = dayOfWeek - DayOfWeek.Sunday;
				start = start.AddDays(-days);
				end = start.AddDays(6);
			}
			else
			{
				start = new DateTime(_view.CurrentDate.Year, _view.CurrentDate.Month, 1);
				end = start.AddMonths(1).AddDays(-1);
			}

			if (_user.IsSuperAdministrator)
				_view.DataSource = _ticketManager.FindAllByExecutionWeek(start, end);
			else
				_view.DataSource = _ticketManager.FindAllByExecutionWeekAndTechnician(start, end, _user.Id);
			_view.DataBind();

			_view.CurrentDate = start;

			if (_view.SelectedDisplayFormat == HomeViewDisplayFormat.Monthly)
				_view.CurrentDateText = string.Format( MonthDisplayFormat, _view.CurrentDate);
			else
				_view.CurrentDateText = string.Format(WeekDisplayFormat, start, end);
		}

		public bool CanViewTicket()
		{
			if (_user.IsSuperAdministrator)
				return true;

			return HasAccess(FunctionAction.Retrieve);

		}

		private bool HasAccess(FunctionAction action)
		{
			return _functionManager.UserHasAccessToFunction(_user, typeof(TicketEditor), action);
		}

		public bool CanEditTicket()
		{
			if (_user.IsSuperAdministrator)
				return true;

			if (((Employee)_user).IsTechnician)
				return false;

			return HasAccess(FunctionAction.Update);
		}

		public bool CanUpdateProgressTicket()
		{
			if (_user.IsSuperAdministrator)
				return true;


			if (((Employee)_user).IsTechnician 
				&& HasAccess(FunctionAction.Retrieve) 
				&& HasAccess(FunctionAction.Update))
				return true;

			return false;
		}

	}
}
