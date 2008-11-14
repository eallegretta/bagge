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
	public partial class CustomerList : ListPage
	{
		ListPresenter<Customer, int> _presenter;

		public CustomerList()
		{
			_presenter = new ListPresenter<Customer, int>(this, ViewState, SpringContext.CustomerManager);
		}


		protected override GridView GridView
		{
			get { return _customers; }
		}
	}
}
