using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.DataAccess.Contracts
{
	public interface ITicketEmployeeDao: IDao<TicketEmployee, int>
	{
		TicketEmployee[] FindAllByEmployee(int employeeId);
		TicketEmployee[] FindAllByTicket(int ticketId);
		void DeleteByEmployee(int employeeId);
		void DeleteByTicket(int ticketId);
	}
}
