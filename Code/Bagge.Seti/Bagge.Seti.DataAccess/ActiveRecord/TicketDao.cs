using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.DataAccess.Contracts;

namespace Bagge.Seti.DataAccess.ActiveRecord
{
	public class TicketDao : GenericDao<Ticket, int>, ITicketDao
	{
	}
}
