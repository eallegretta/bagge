using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.WebSite.Presenters;
using Bagge.Seti.Common;

namespace Bagge.Seti.WebSite
{
	public partial class TicketEditor : EditorPage<Ticket, int>, ITicketEditorView
	{
		TicketEditorPresenter _presenter;

		public TicketEditor()
		{
			_presenter = new TicketEditorPresenter(this, IoCContainer.TicketManager, 
				IoCContainer.EmployeeManager, IoCContainer.CustomerManager, 
				IoCContainer.TicketStatusManager, IoCContainer.ProductManager);

		}


		protected override Bagge.Seti.WebSite.Presenters.EditorPresenter<Ticket, int> Presenter
		{
			get { return _presenter; }
		}

		protected override CompositeDataBoundControl Details
		{
			get { throw new NotImplementedException(); }
		}

		protected override Microsoft.Practices.Web.UI.WebControls.ObjectContainerDataSource ObjectDataSource
		{
			get { throw new NotImplementedException(); }
		}


		#region ITicketEditorView Members

		public Employee[] Employees
		{
			set { throw new NotImplementedException(); }
		}

		public int SelectedEmployeeId
		{
			get { throw new NotImplementedException(); }
		}

		public Customer[] Customers
		{
			set { throw new NotImplementedException(); }
		}

		public int SelectedCustomerId
		{
			get { throw new NotImplementedException(); }
		}

		public TicketStatus[] TicketStatus
		{
			set { throw new NotImplementedException(); }
		}

		public int SelectedTicketStatus
		{
			get { throw new NotImplementedException(); }
		}

		public Product[] Products
		{
			set { throw new NotImplementedException(); }
		}

		public int SelectedProductId
		{
			get { throw new NotImplementedException(); }
		}

		#endregion
	}
}
