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

		public static T GetFilterValue<T>(this IList<FilterPropertyValue> filters, string propertyName) where T : IConvertible
		{
			return GetFilterValue<T>(filters, propertyName, default(T));
		}
		public static T GetFilterValue<T>(this IList<FilterPropertyValue> filters, string propertyName, T defaultValue) where T: IConvertible
		{
			var filter = GetFilter(filters, propertyName);
			if (filter == null)
				return defaultValue;
			if(filter.Value == null)
				return defaultValue;

			return (T)Convert.ChangeType(filter.Value, typeof(T));
		}

		public static IList<BaseReport> ConvertFrom<T>(this IList<T> reports) where T: BaseReport
		{
			var data = new List<BaseReport>();
			foreach (var report in reports)
				data.Add(report);
			return data;
		}

		public static void AddBetween(this IList<FilterPropertyValue> filters, string propertyName, object valueFrom, object valueTo)
		{
			Add(filters, propertyName, FilterPropertyValueType.BetweenLowerBound, valueFrom);
			Add(filters, propertyName, FilterPropertyValueType.BetweenTopBound, valueTo);
		}

		public static void Add(this IList<FilterPropertyValue> filters, string propertyName, object value)
		{
			Add(filters, propertyName, FilterPropertyValueType.Equals, value);
		}
		public static void Add(this IList<FilterPropertyValue> filters, string propertyName, FilterPropertyValueType type, object value)
		{
			filters.Add(new FilterPropertyValue { Property = propertyName, Type = type, Value = value });
		}
	}
}
