using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bagge.Seti.BusinessEntities.Reports
{
	public class TechniciansByTicketReport: BaseReport
	{

		public string Firstname { get; set; }

		public string Lastname { get; set; }

		public int TicketsCount { get; set; }

		public decimal RealDuration { get; set; }
	}
}
