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

		public TicketManager(ITicketDao dao): base(dao)
		{
		}
	}
}
