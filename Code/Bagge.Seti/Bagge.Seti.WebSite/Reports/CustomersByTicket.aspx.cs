﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bagge.Seti.BusinessEntities.Reports;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.WebSite.Helpers;
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.WebSite.Reports
{
	[SecurizableCrud("Securizable_CustomersByTicket", typeof(CustomersByTicket), FunctionAction.List)]
	public partial class CustomersByTicket : FilteredReportPage<CustomersByTicketReport>
	{

		protected override Button FilterButton
		{
			get { return _filter; }
		}

		public override string GetFormattedColumnValue(int columnIndex, string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				switch(columnIndex)
				{
					case 3:
						return value + "hs";
					case 4:
						return string.Format("{0:c}", decimal.Parse(value));
				}
			}
			return base.GetFormattedColumnValue(columnIndex, value);
		}

		public override IList<Bagge.Seti.BusinessEntities.FilterPropertyValue> Filters
		{
			get
			{
				var filters = new List<FilterPropertyValue>();
                FilterHelper.AddCalendarFilterValue(_dateFrom, "DateFrom", FilterPropertyValueType.Equals, filters);
                FilterHelper.AddCalendarFilterValue(_dateTo, "DateTo", FilterPropertyValueType.Equals, filters);
				FilterHelper.AddTextBoxFilterValue<string>(_customerName, "CustomerName", FilterPropertyValueType.Like, filters);
				FilterHelper.AddTextBoxFilterValue<int>(_ticketCountFrom, "TicketCountFrom", FilterPropertyValueType.GreaterEquals, filters);
				FilterHelper.AddTextBoxFilterValue<int>(_ticketCountTo, "TicketCountTo", FilterPropertyValueType.LowerEquals, filters);
				FilterHelper.AddTextBoxFilterValue<decimal>(_totalRealDurationFrom, "RealDurationFrom", FilterPropertyValueType.GreaterEquals, filters);
				FilterHelper.AddTextBoxFilterValue<decimal>(_totalRealDurationTo, "RealDurationTo", FilterPropertyValueType.LowerEquals, filters);
				FilterHelper.AddTextBoxFilterValue<decimal>(_totalBudgetFrom, "BudgetFrom", FilterPropertyValueType.GreaterEquals, filters);
				FilterHelper.AddTextBoxFilterValue<decimal>(_totalBudgetTo, "BudgetTo", FilterPropertyValueType.LowerEquals, filters);
				return filters;
			}
		}
	}
}
