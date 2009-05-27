using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.WebSite.Views;

namespace Bagge.Seti.WebSite.Presenters
{
	public class TicketListPresenter: ListPresenter<Ticket, int>
	{
		ITicketStatusManager _ticketStatusManager;
		IEmployeeManager _employeeManager;
		ICustomerManager _customerManager;

		public TicketListPresenter(ITicketListView view, ITicketManager manager,
			ITicketStatusManager ticketStatusManager, IEmployeeManager employeeManager,
			ICustomerManager customerManager): base(view, manager)
		{
			_ticketStatusManager = ticketStatusManager;
			_employeeManager = employeeManager;
			_customerManager = customerManager;
		}

		protected override void OnInit(object sender, EventArgs e)
		{
			if (!View.IsPostBack)
			{
				var view = GetView<ITicketListView>();
				view.Status = _ticketStatusManager.FindAll();
				view.Technicians = _employeeManager.FindAllActiveTechnicians();
				view.Customers = _customerManager.FindAllActive();
			}

			base.OnInit(sender, e);
		}

		public bool CanAdministerTicket(Ticket ticket)
		{
			return !ticket.Customer.Deleted ||
				ticket.Employees.FirstOrDefault(te => te.Employee.Deleted == true) == null;
		}
	}
}
