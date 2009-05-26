using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities.Reports;
using Bagge.Seti.DataAccess.Contracts.Reports;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.DataAccess.Reports
{
	public class ProductsConsumedReportDao: BaseReportDao<ProductsConsumedReport>, IReportDao
	{

		#region IReportDao Members

		public BaseReport GetReport(IList<FilterPropertyValue> filters)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
