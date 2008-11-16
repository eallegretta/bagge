using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.WebSite.Presenters;
using Bagge.Seti.Common;
using Microsoft.Practices.Web.UI.WebControls;

namespace Bagge.Seti.WebSite
{
	public partial class CustomerEditor : EditorPage<Customer, int>
	{
		EditorPresenter<Customer, int> _presenter;

		public CustomerEditor()
		{
			_presenter = new EditorPresenter<Customer, int>(this, SpringContext.CustomerManager);
		}

		protected override EditorPresenter<Customer, int> Presenter
		{
			get { return _presenter; }
		}

		protected override CompositeDataBoundControl Details
		{
			get { return _details; }
		}

		protected override ObjectContainerDataSource ObjectDataSource
		{
			get { return _dataSource; }
		}

	}
}
