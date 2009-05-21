using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bagge.Seti.BusinessEntities.Reports
{
	public class ProductsConsumedReport: BaseReport
	{

		public string Name { get; set; }

		public string Description { get; set; }

		public int EstimatedQuantity { get; set; }
	}
}
