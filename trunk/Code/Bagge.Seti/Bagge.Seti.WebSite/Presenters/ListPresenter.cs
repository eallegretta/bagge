using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.BusinessLogic;
using System.Web.UI.WebControls;

namespace Bagge.Seti.WebSite.Presenters
{
	public class ListPresenter<T,PK> where T: PrimaryKeyDomainObject<T,PK>
	{
		IListView _view;
		IManager<T, PK> _manager;

		public ListPresenter(IListView view, IManager<T, PK> manager)
		{
			_view = view;
			_view.PageIndexChanging += new GridViewPageEventHandler(OnPageIndexChanging);
			_view.Load += new EventHandler(OnLoad);
			_manager = manager;
		}

		protected virtual void OnLoad(object sender, EventArgs e)
		{
			if (!_view.IsPostBack)
				DataBind();
		}

		protected virtual void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			((GridView)sender).PageIndex = e.NewPageIndex;
			DataBind();
		}

		private void DataBind()
		{
			_view.DataSource = _manager.FindAll();
			_view.DataBind();
		}


	}
}
