using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bagge.Seti.BusinessEntities.Reports
{
	public class CustomersByTicketReport: BaseReport
	{
		public string Name { get; set; }

		public string Cuit { get; set; }

		public int TicketsCount { get; set; }

		public decimal RealDuration { get; set; }

		public decimal Budget { get; set; }

		public bool Subscription { get; set; }
	}
}
