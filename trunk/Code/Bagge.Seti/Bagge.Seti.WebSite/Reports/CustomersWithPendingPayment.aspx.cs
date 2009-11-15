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
		protected override void OnLoad(EventArgs e)
		{
			if (!IsPostBack)
			{
				_hasSubscription.Items.Add(new ListItem(Resources.WebSite.YesText, "true"));
				_hasSubscription.Items.Add(new ListItem(Resources.WebSite.NoText, "false"));
			}
			base.OnLoad(e);
		}


        public override IList<Bagge.Seti.BusinessEntities.FilterPropertyValue> Filters
        {
            get
            {
                var filters = new List<FilterPropertyValue>();
                FilterHelper.AddTextBoxFilterValue<string>(_customerName, "CustomerName", FilterPropertyValueType.Like, filters);
				FilterHelper.AddDropDownFilterValue<bool>(_hasSubscription, "HasSubscription", FilterPropertyValueType.Equals, filters);
				return filters;
            }
        }
    }
}
	