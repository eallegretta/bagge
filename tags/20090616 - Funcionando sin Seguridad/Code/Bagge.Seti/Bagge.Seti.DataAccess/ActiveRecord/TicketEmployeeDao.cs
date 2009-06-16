using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.DataAccess.Contracts;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;

namespace Bagge.Seti.DataAccess.ActiveRecord
{
	public class TicketEmployeeDao: GenericDao<TicketEmployee, int>, ITicketEmployeeDao
	{
		#region ITicketEmployeeDao Members

		public TicketEmployee[] FindAllByEmployee(int employeeId)
		{
			string hql = "from TicketEmployee t where t.Employee.Id = ?";
			var query = new SimpleQuery<TicketEmployee>(hql, employeeId);

			return query.Execute();
		}

		public TicketEmployee[] FindAllByTicket(int ticketId)
		{
			string hql = "from TicketEmployee t where t.Ticket.Id = ?";
			var query = new SimpleQuery<TicketEmployee>(hql, ticketId);

			return query.Execute();
		}

		public void DeleteByEmpoloyee(int employeeId)
		{
			ActiveRecordMediator<TicketEmployee>.DeleteAll("EmployeeId = " + employeeId);
		}

		public void DeleteByTicket(int ticketId)
		{
			ActiveRecordMediator<TicketEmployee>.DeleteAll("TicketId = " + ticketId);
		}

		#endregion
	}
}
