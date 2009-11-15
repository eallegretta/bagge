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
	[SecurizableCrud("Securizable_CustomersWithPendingPayment", typeof(CustomersWithPendingPayment), FunctionAction.List)]
    public partial class CustomersWithPendingPayment : FilteredReportPage<CustomersWithPendingPaymentReport>
    {
		public override string GetFormattedColumnValue(int columnIndex, string value)
		{
			if (!string.IsNullOrEmpty(value) && columnIndex == 9)
			{
				return string.Format("{0:c}", decimal.Parse(value));
			}
			return base.GetFormattedColumnValue(columnIndex, value);
		}
        protected override Button FilterButton
        {
            get { return _filter; }
        }


        public override IList<Bagge.Seti.BusinessEntities.FilterPropertyValue> Filters
        {
            get
            {
                var filters = new List<FilterPropertyValue>();
                FilterHelper.AddTextBoxFilterValue<string>(_customerName, "CustomerName", FilterPropertyValueType.Like, filters);
                return filters;
            }
        }
    }
}
	