using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bagge.Seti.BusinessEntities.Reports
{
	public class CustomersBySubscriptionReport: BaseCustomerTicketReport
	{
		public bool Subscription { get; set; }
	}
}
