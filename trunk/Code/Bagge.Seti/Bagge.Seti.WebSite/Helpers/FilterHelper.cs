using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.BusinessEntities;
using System.Web.UI.WebControls;

namespace Bagge.Seti.WebSite.Helpers
{
	public static class FilterHelper
	{
		private static void AddFilterValue<TValue>(object value, string property,
			FilterPropertyValueType type, IList<FilterPropertyValue> filters) where TValue: IConvertible
		{
			filters.Add(
					new FilterPropertyValue
					{
						Property = property,
						Value = Convert.ChangeType(value, typeof(TValue)),
						Type = type
					});
		}

		public static void AddCalendarFilterValue(Controls.Calendar calendar, string property,
			FilterPropertyValueType type, IList<FilterPropertyValue> filters) 
		{
			if (calendar.Date.HasValue)
			{
				filters.Add(new FilterPropertyValue { Property = property, Value = calendar.Date, Type = type });
			}
		}

		public static void AddCalendarBetweenFilterValue(Controls.Calendar calendar, string property,
			IList<FilterPropertyValue> filters)
		{
			if (calendar.Date.HasValue)
			{
				DateTime from = new DateTime(calendar.Date.Value.Year, calendar.Date.Value.Month, calendar.Date.Value.Day, 0, 0, 0);
				DateTime to = new DateTime(calendar.Date.Value.Year, calendar.Date.Value.Month, calendar.Date.Value.Day, 23, 59, 59);

				filters.AddBetween(property, from, to);
			}
		}

		public static void AddTextBoxFilterValue<TValue>(TextBox control, string property,
			FilterPropertyValueType type, IList<FilterPropertyValue> filters) where TValue : IConvertible
		{
			if (!string.IsNullOrEmpty(control.Text))
			{
				AddFilterValue<TValue>(control.Text, property, type, filters);
			}
		}

		public static void AddDropDownFilterValue<TValue>(DropDownList control, string property,
			FilterPropertyValueType type, IList<FilterPropertyValue> filters) where TValue : IConvertible
		{
			if (!string.IsNullOrEmpty(control.SelectedValue))
			{
				AddFilterValue<TValue>(control.SelectedValue, property, type, filters);
			}
		}

		public static void AddCheckBoxListFilterValue<TValue>(CheckBoxList control, string property,
			FilterPropertyValueType type, IList<FilterPropertyValue> filters) where TValue : IConvertible
		{
			foreach (ListItem item in control.Items)
			{
				if (item.Selected)
				{
					AddFilterValue<TValue>(item.Value, property, type, filters);
				}
			}
		}
	}
}
