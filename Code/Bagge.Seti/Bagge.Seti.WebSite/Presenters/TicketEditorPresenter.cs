using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.BusinessLogic.Contracts;

namespace Bagge.Seti.WebSite.Presenters
{
	public class TicketEditorPresenter: EditorPresenter<Ticket, int>
	{
		IEmployeeManager _employeeManager;
		ICustomerManager _customerManager;
		ITicketStatusManager _ticketStatusManager;
		IProductManager _productManager;

		public TicketEditorPresenter(ITicketEditorView view, 
			ITicketManager ticketManager,
			IEmployeeManager employeeManager,
			ICustomerManager customerManager,
			ITicketStatusManager ticketStatusManager,
			IProductManager productManager): base(view, ticketManager)
		{
			_employeeManager = employeeManager;
			_customerManager = customerManager;
			_ticketStatusManager = ticketStatusManager;
			_productManager = productManager;
		}

	}
}
