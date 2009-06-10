using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.DataAccess.Contracts;
using Castle.ActiveRecord;

namespace Bagge.Seti.DataAccess.ActiveRecord
{
	public class TicketDao : GenericDao<Ticket, int>, ITicketDao
	{
		public override void Update(Ticket instance)
		{
			SessionScopeUtils.FlushSessionScope();

			base.Update(instance);
		}

		#region ITicketDao Members

		public void DeleteProducts(int ticketId)
		{
			ActiveRecordMediator<ProductTicket>.DeleteAll("TicketId = " + ticketId);
		}

		#endregion
	}
}
