using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities.Reports;
using Bagge.Seti.DataAccess.Contracts.Reports;
using Bagge.Seti.BusinessEntities;
using Eaa.Framework.Data;
using Eaa.Framework.Collections.Adapters;
using Bagge.Seti.Extensions;

namespace Bagge.Seti.DataAccess.Reports
{
	public class CustomersBySubscriptionReportDao: DBRepository<CustomersBySubscriptionReport>, IReportDao
	{
		public CustomersBySubscriptionReportDao(): base(Constants.DEFAULT_CONNECTION_STRING_NAME)
		{
		}

		#region IReportDao<CustomersBySubscriptionReport> Members

		public IList<BaseReport> GetReport(IList<FilterPropertyValue> filters)
		{
			var filter = filters.GetFilter("Value");
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

			return GetRecords("CustomersBySubscriptionReport", valueFrom, valueTo).ConvertFrom<CustomersBySubscriptionReport>();

			
		}

		#endregion

		PropertyMappingCollection _mappings;

		protected override PropertyMappingCollection Mappings
		{
			get
			{
				if (_mappings == null)
				{
					_mappings = new PropertyMappingCollection();
					_mappings.Add("Name", "Name");
					_mappings.Add("Cuit", "Cuit");
					_mappings.Add("Address", "Address");
					_mappings.Add("Floor", "Floor");
					_mappings.Add("ZipCode", "ZipCode");
					_mappings.Add("City", "City");
					_mappings.Add("Phone", "Phone");
					_mappings.Add("MobilePhone", "MobilePhone");
					_mappings.Add("Email", "Email");
					_mappings.Add("Budget", "Budget");
					_mappings.Add("Subscription", "Subscription");
				}

				return _mappings;
			}
		}
	}
}
