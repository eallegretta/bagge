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
using Microsoft.Practices.Web.UI.WebControls;

namespace Bagge.Seti.WebSite
{
	public partial class CustomerList : ListPage<Customer, int>
	{
		ListPresenter<Customer, int> _presenter;

		public CustomerList()
		{
			_presenter = new ListPresenter<Customer, int>(this, IoCContainer.CustomerManager);
		}

		protected override ObjectContainerDataSource ObjectDataSource
		{
			get { return _dataSource; }
		}

		protected override ListPresenter<Customer, int> Presenter
		{
			get { return _presenter; }
		}

		protected override GridView Grid
		{
			get { return _customers; }
		}
	}
}
