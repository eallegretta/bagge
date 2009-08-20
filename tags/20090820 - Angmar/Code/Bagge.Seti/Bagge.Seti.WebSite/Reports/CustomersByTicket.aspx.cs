using System;
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
	[SecurizableCrud("Securizable_CustomersByTicket", typeof(CustomersByTicket), FunctionAction.Retrieve)]
	public partial class CustomersByTicket : FilteredReportPage<CustomersByTicketReport>
	{

		protected override Button FilterButton
		{
			get { return _filter; }
		}

		public override IList<Bagge.Seti.BusinessEntities.FilterPropertyValue> Filters
		{
			get
			{
				var filters = new List<FilterPropertyValue>();
                FilterHelper.AddCalendarFilterValue(_dateFrom, "DateFrom", FilterPropertyValueType.Equals, filters);
                FilterHelper.AddCalendarFilterValue(_dateTo, "DateTo", FilterPropertyValueType.Equals, filters);
				return filters;
			}
		}
	}
}
