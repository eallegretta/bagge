using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bagge.Seti.BusinessEntities.Reports
{
	public class BaseCustomerTicketReport: BaseReport
	{
		public string Name { get; set; }

		public string Cuit { get; set; }

		public string Address { get; set; }

		public string Floor { get; set; }

		public string ZipCode { get; set; }

		public string City { get; set; }

		public string Phone { get; set; }

		public string MobilePhone { get; set; }

		public string Email { get; set; }

		public float Budget { get; set; }
	}
}
