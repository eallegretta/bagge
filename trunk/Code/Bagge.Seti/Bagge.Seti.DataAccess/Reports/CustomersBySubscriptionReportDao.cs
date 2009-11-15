using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities.Reports;
using Bagge.Seti.DataAccess.Contracts.Reports;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.Extensions;

namespace Bagge.Seti.DataAccess.Reports
{
	public class CustomersBySubscriptionReportDao: BaseReportDao<CustomersBySubscriptionReport>, IReportDao
	{
		public CustomersBySubscriptionReportDao()
		{
		}

		#region IReportDao<CustomersBySubscriptionReport> Members

		public BaseReport GetReport(IList<FilterPropertyValue> filters)
		{
            return GetReport("CustomersBySubscriptionReport",
                filters.GetFilterValue<string>("CustomerName", string.Empty) + "%",
                0);
		}
		#endregion
	}
}
