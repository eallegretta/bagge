using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bagge.Seti.BusinessEntities.Reports
{
	public class ProductsByProviderReport: BaseReport
	{
		public string ProductName { get; set; }

		public string ProductDescription { get; set; }

		public decimal UnitaryPrice { get; set; }

		public string ProviderName { get; set; }

	}
}
