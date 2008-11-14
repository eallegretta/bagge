using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.BusinessLogic;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Bagge.Seti.WebSite.Presenters
{
	public class ListPresenter<T,PK> where T: PrimaryKeyDomainObject<T,PK>
	{
		IListView _view;
		IManager<T, PK> _manager;

		private StateBag ViewState
		{
			get;
			set;
		}
		private string LastSortExpression
		{
			get { return ViewState["LastSortExpression"] as string; }
			set { ViewState["LastSortExpression"] = value; }
		}
		private SortDirection LastSortDirection
		{
			get
			{
				object lastSortDirection = ViewState["LastSortDirection"];
				if (lastSortDirection == null)
					return SortDirection.Ascending;
				return (SortDirection)lastSortDirection;
			}
			set { ViewState["LastSortDirection"] = value; }
		}

		private void SetSorting(string expression, SortDirection direction)
		{
			if (LastSortExpression == expression)
				LastSortDirection = (LastSortDirection == SortDirection.Ascending) ? SortDirection.Descending : SortDirection.Ascending;
			else
				LastSortDirection = SortDirection.Ascending;
			LastSortExpression = expression;
		}

		public ListPresenter(IListView view, StateBag viewState, IManager<T, PK> manager)
		{
			ViewState = viewState;
			_view = view;
			_view.Init += new EventHandler(OnInit);
			_view.Load += new EventHandler(OnLoad);
			_manager = manager;
		}

		protected virtual void OnInit(object sender, EventArgs e)
		{
			_view.PageIndexChanging += new GridViewPageEventHandler(OnPageIndexChanging);
			_view.Sorting += new GridViewSortEventHandler(OnSorting);
		}

		protected virtual void OnSorting(object sender, GridViewSortEventArgs e)
		{
			SetSorting(e.SortExpression, e.SortDirection);
			DataBind(LastSortExpression, LastSortDirection == SortDirection.Ascending);
		}

		protected virtual void OnLoad(object sender, EventArgs e)
		{
			if (!_view.IsPostBack)
				DataBind();
		}

		protected virtual void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			((GridView)sender).PageIndex = e.NewPageIndex;
			DataBind(LastSortExpression, LastSortDirection == SortDirection.Ascending);
		}

		private void DataBind(string orderBy, bool ascending)
		{
			if (string.IsNullOrEmpty(orderBy))
				_view.DataSource = _manager.FindAll();
			else
				_view.DataSource = _manager.FindAllOrdered(orderBy, ascending);
			_view.DataBind();
		}
		private void DataBind()
		{
			DataBind(null, true);
		}


	}
}
