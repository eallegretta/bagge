using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.WebSite.Presenters;
using Bagge.Seti.BusinessLogic;
using Spring.Context.Support;
using Bagge.Seti.Common;

namespace Bagge.Seti.WebSite
{
	public partial class CustomerList : System.Web.UI.Page, IListView
	{
		ListPresenter<Customer, int> _presenter;

		public CustomerList()
		{
			_presenter = new ListPresenter<Customer, int>(this, SpringContext.CustomerManager);
		}
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		#region IListView Members

		public event GridViewPageEventHandler PageIndexChanging
		{
			add
			{
				_customers.PageIndexChanging += value;
			}
			remove
			{
				_customers.PageIndexChanging -= value;
			}
		}

		#endregion

		#region IView Members


		public object DataSource
		{
			get
			{
				return _customers.DataSource;
			}
			set
			{
				_customers.DataSource = value;
			}
		}

		#endregion
	}
}
