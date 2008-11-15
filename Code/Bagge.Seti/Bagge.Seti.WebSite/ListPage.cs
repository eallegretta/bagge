using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.WebSite.Presenters;
using Bagge.Seti.BusinessLogic;

namespace Bagge.Seti.WebSite
{
	public abstract class ListPage<T,PK> : System.Web.UI.Page, IListView where T: PrimaryKeyDomainObject<T, PK>
	{
		protected abstract ListPresenter<T, PK> Presenter
		{
			get;
		}

		protected abstract GridView GridView
		{
			get;
		}

		#region IListView Members

		public event GridViewPageEventHandler PageIndexChanging
		{
			add
			{
				GridView.PageIndexChanging += value;
			}
			remove
			{
				GridView.PageIndexChanging -= value;
			}
		}

		public event GridViewSortEventHandler Sorting
		{
			add
			{
				GridView.Sorting += value;
			}
			remove
			{
				GridView.Sorting -= value;
			}
		}

		#endregion

		#region IView Members


		public object DataSource
		{
			get
			{
				return GridView.DataSource;
			}
			set
			{
				GridView.DataSource = value;
			}
		}

		#endregion
	}
}
