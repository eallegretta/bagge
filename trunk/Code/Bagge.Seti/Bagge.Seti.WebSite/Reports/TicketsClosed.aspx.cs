using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.WebSite.Helpers;
using Bagge.Seti.BusinessEntities.Reports;

namespace Bagge.Seti.WebSite.Reports
{
	[SecurizableCrud("Securizable_TicketsClosed", typeof(TechniciansByTicket), FunctionAction.Retrieve)]
	public partial class TicketsClosed : FilteredReportPage<TicketsClosedReport>
	{
		protected override Button FilterButton
		{
			get { return _filter; }
		}

		public override string GetFormattedColumnValue(int columnIndex, string value)
		{
			if (string.IsNullOrEmpty(value))
				return null;

			switch (columnIndex)
			{
				case 3:
				case 4:
					return string.Format("{0:0.00}", decimal.Parse(value));
				case 5:
					return string.Format("%{0:0.00}", decimal.Parse(value));
				default:
					return null;
			}
		}

		public override IList<Bagge.Seti.BusinessEntities.FilterPropertyValue> Filters
		{
			get
			{
				var filters = new List<FilterPropertyValue>();
				FilterHelper.AddCalendarFilterValue(_dateFrom, "DateFrom", FilterPropertyValueType.Equals, filters);
				FilterHelper.AddCalendarFilterValue(_dateTo, "DateTo", FilterPropertyValueType.Equals, filters);
				FilterHelper.AddDropDownFilterValue<bool>(_groupBy, "GroupBy", FilterPropertyValueType.Equals, filters);

				return filters;
			}
		}
	}
}
