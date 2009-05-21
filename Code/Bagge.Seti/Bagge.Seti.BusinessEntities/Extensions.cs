using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities.Reports;

namespace Bagge.Seti.BusinessEntities
{
	public static class Extensions
	{
		public static FilterPropertyValue GetFilter(this IList<FilterPropertyValue> filters, string propertyName)
		{
			if (filters == null)
				return null;

			return (from filter in filters
							   where filter.Property == propertyName
							   select filter).FirstOrDefault();
		}

		public static IList<BaseReport> ConvertFrom<T>(this IList<T> reports) where T: BaseReport
		{
			var data = new List<BaseReport>();
			foreach (var report in reports)
				data.Add(report);
			return data;
		}
	}
}
