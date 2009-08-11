using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bagge.Seti.WebSite.Presenters
{
	//public class SecurityExceptionListPresenter: ListPresenter<Ticket, int>
	//{
	//    ITicketStatusManager _ticketStatusManager;
	//    IEmployeeManager _employeeManager;
	//    ICustomerManager _customerManager;
	//    IUser _loggedUser;

	//    public TicketListPresenter(ITicketListView view, ITicketManager manager,
	//        ITicketStatusManager ticketStatusManager, IEmployeeManager employeeManager,
	//        ICustomerManager customerManager, IUser loggedUser): base(view, manager)
	//    {
	//        _ticketStatusManager = ticketStatusManager;
	//        _employeeManager = employeeManager;
	//        _customerManager = customerManager;
	//        _loggedUser = loggedUser;
	//    }

	//    protected override void OnInit(object sender, EventArgs e)
	//    {
	//        if (!View.IsPostBack)
	//        {
	//            var view = GetView<ITicketListView>();
	//            view.Status = _ticketStatusManager.FindAll();
	//            view.Technicians = _employeeManager.FindAllActiveTechnicians();
	//            view.Customers = _customerManager.FindAllActive();

	//            var loggedUser = _loggedUser as Employee;
	//            if (loggedUser.IsTechnician && !loggedUser.IsSuperAdministrator)
	//                view.IsTechnicianView = true;
	//        }

	//        base.OnInit(sender, e);
	//    }
	
		
	//    public bool CanAdministerTicket(Ticket ticket)
	//    {
	//        return !ticket.Customer.Deleted ||
	//            ticket.Employees.FirstOrDefault(e => e.Deleted == true) == null;
	//    }

	//    public bool CanUpdateProgress(Ticket ticket)
	//    {
	//        return 
	//            !ticket.Status.In(TicketStatusEnum.Closed, TicketStatusEnum.PendingPayment) && (
	//            _loggedUser.IsSuperAdministrator ||
	//            ((Employee)_loggedUser).IsTechnician);
				

	//    }
	//}
}
