using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.DataAccess.Contracts;

namespace Bagge.Seti.BusinessLogic
{
	public class TicketManager : AuditableGenericManager<Ticket, int>, ITicketManager
	{
		ITicketStatusManager _ticketStatusManager;

		public TicketManager(ITicketDao dao, ITicketStatusManager ticketStatusManager)
			: base(dao)
		{
			_ticketStatusManager = ticketStatusManager;
		}

		#region ITicketManager Members

		public Ticket[] FindAllByStatus(TicketStatusEnum status)
		{
			return FindAllActiveByProperty("Status",
				_ticketStatusManager.Get(status));
		}

		#endregion
	}
}
