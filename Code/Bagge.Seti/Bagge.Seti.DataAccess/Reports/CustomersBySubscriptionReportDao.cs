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
			/*var filter = filters.GetFilter("Value");
			decimal minValue = 0;
			decimal maxValue = 999999999;

			decimal valueFrom, valueTo;
			valueFrom = minValue;
			valueTo = maxValue;
			if (filter != null)
			{
				switch(filter.Type)
				{
					case FilterPropertyValueType.Equals:
						valueFrom = valueTo = (decimal)filter.Value;
						break;
					case FilterPropertyValueType.Greater:
						valueFrom = ((decimal)filter.Value) + 0.0000001m;
						valueTo = maxValue;
						break;
					case FilterPropertyValueType.GreaterEquals:
						valueFrom = (decimal)filter.Value;
						valueTo = maxValue;
						break;
					case FilterPropertyValueType.Lower:
						valueFrom = minValue;
						valueTo = ((decimal)filter.Value) - 0.0000001m;
						break;
					case FilterPropertyValueType.LowerEquals:
						valueFrom = minValue;
						valueTo = (decimal)filter.Value;
						break;
				}
			}

			return  GetReport("CustomersBySubscriptionReport", valueFrom, valueTo);*/
	
			return  GetReport("CustomersBySubscriptionReport");

		}
		#endregion
	}
}
