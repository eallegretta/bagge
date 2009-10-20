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
using Bagge.Seti.WebSite.Models;
using Bagge.Seti.Common;

namespace Bagge.Seti.WebSite.Presenters
{
	public class HomePresenter
	{
		IHomeView _view;
		ITicketManager _ticketManager;
		ITicketStatusManager _ticketStatusManager;
		IFunctionManager _functionManager;
		ISecurityManager _securityManager;
		IUser _user;

		public HomePresenter(IHomeView view, ITicketManager ticketManager, ITicketStatusManager ticketStatusManager, 
			IFunctionManager functionManager, 
			ISecurityManager securityManager,
			IUser user,
			string weeklyDateFormat, string monthlyDateFormat)
		{
			Check.Require(view != null);
			Check.Require(ticketManager != null);
			Check.Require(ticketStatusManager != null);
			Check.Require(functionManager != null);
			Check.Require(securityManager != null);
			Check.Require(user != null);
			Check.Require(!string.IsNullOrEmpty(weeklyDateFormat));
			Check.Require(!string.IsNullOrEmpty(monthlyDateFormat));

			_view = view;
			_view.Init += new EventHandler(OnInit);
			_view.Load += new EventHandler(OnLoad);
			_ticketManager = ticketManager;
			_ticketStatusManager = ticketStatusManager;
			_functionManager = functionManager;
			_securityManager = securityManager;
			_user = user;
			MonthDisplayFormat = monthlyDateFormat;
			WeekDisplayFormat = weeklyDateFormat;
		}

		protected virtual void OnInit(object sender, EventArgs e)
		{
			_view.PreviewDateSelected += new EventHandler(_view_PreviewDateSelected);
			_view.RewindDateSelected += new EventHandler(_view_RewindDateSelected);
			_view.NextDateSelected += new EventHandler(_view_NextDateSelected);
			_view.FastForwardDateSelected += new EventHandler(_view_FastForwardDateSelected);
			_view.DisplayFormatChanged += new EventHandler(_view_DisplayFormatChanged);
		}

		void _view_FastForwardDateSelected(object sender, EventArgs e)
		{
			if (_view.SelectedDisplayFormat == HomeViewDisplayFormat.Monthly)
				_view.CurrentDate = _view.CurrentDate.AddYears(1);
			else
				_view.CurrentDate = _view.CurrentDate.AddMonths(1);

			BindData();
		}

		void _view_RewindDateSelected(object sender, EventArgs e)
		{
			if (_view.SelectedDisplayFormat == HomeViewDisplayFormat.Monthly)
				_view.CurrentDate = _view.CurrentDate.AddYears(-1);
			else
				_view.CurrentDate = _view.CurrentDate.AddMonths(-1);

			BindData();
		}

		void _view_DisplayFormatChanged(object sender, EventArgs e)
		{
			_view.CurrentDate = DateTime.Now;
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


			if (!((Employee)_user).IsTechnician)
				_view.DataSource = GetViewModelDataSource(_ticketManager.FindAllByExecutionWeek(start, end));
			else
				_view.DataSource = GetViewModelDataSource(_ticketManager.FindAllByExecutionWeekAndTechnician(start, end, _user.Id));
			_view.DataBind();

			_view.CurrentDate = start;

			if (_view.SelectedDisplayFormat == HomeViewDisplayFormat.Monthly)
				_view.CurrentDateText = string.Format( MonthDisplayFormat, _view.CurrentDate);
			else
				_view.CurrentDateText = string.Format(WeekDisplayFormat, start, end);
		}

		private HomeViewModel[] GetViewModelDataSource(Ticket[] tickets)
		{
			HomeViewModel[] models = new HomeViewModel[tickets.Length];
			for (int index = 0; index < tickets.Length; index++)
			{
				models[index] = new HomeViewModel(tickets[index]);
			}
			return models;
		}

		public bool CanViewTickets()
		{
			if (_user.IsSuperAdministrator)
				return true;

			return HasAccess(FunctionAction.Get);

		}

		public bool CanViewTicket(Ticket ticket)
		{
			if (_user.IsSuperAdministrator)
				return true;

			return HasAccess(ticket, FunctionAction.Get);
		}

		private bool HasAccess(Ticket instance, FunctionAction action)
		{
			var function = _functionManager.Get(typeof(TicketEditor), action);
			var exceptions = _securityManager.FindAllSecurityExceptions(_user, function.Id);
			return _securityManager.UserHasAccessToInstance(instance, exceptions);
		}

		private bool HasAccess(FunctionAction action)
		{
			return _functionManager.UserHasAccessToFunction(_user, typeof(TicketEditor), action);
		}

		public bool CanEditTickets()
		{
			if (_user.IsSuperAdministrator)
				return true;

			if (((Employee)_user).IsTechnician)
				return false;

			return HasAccess(FunctionAction.Update);
		}

		public bool CanEditTicket(Ticket ticket)
		{
			if (_user.IsSuperAdministrator)
				return true;

			return HasAccess(ticket, FunctionAction.Update);
		}

		public bool CanUpdateProgressTickets()
		{
			if (_user.IsSuperAdministrator)
				return true;


			if (((Employee)_user).IsTechnician 
				&& HasAccess(FunctionAction.Get) 
				&& HasAccess(FunctionAction.Update))
				return true;

			return false;
		}

		public bool CanUpdateProgressTicket(Ticket ticket)
		{
			if (_user.IsSuperAdministrator)
				return true;

			return CanEditTicket(ticket) && !ticket.IsClosed;
		}

	}
}
