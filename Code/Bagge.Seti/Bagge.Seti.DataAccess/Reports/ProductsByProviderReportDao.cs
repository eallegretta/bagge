using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.DataAccess.Contracts.Reports;
using Bagge.Seti.BusinessEntities.Reports;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.DataAccess.Reports
{
	public class ProductsByProviderReportDao: BaseReportDao<ProductsByProviderReport>, IReportDao
	{
        public ProductsByProviderReportDao()
		{
		}

		#region IReportDao Members

		public BaseReport GetReport(IList<FilterPropertyValue> filters)
        {
            return GetReport("ProductsByProviderReport",
				filters.GetFilterValue<string>("ProductName") + "%",
				filters.GetFilterValue<string>("ProviderName") + "%");
		}

		#endregion
	}
}
