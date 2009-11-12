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
	[SecurizableCrud("Securizable_ProductsConsumed", typeof(ProductsConsumed), FunctionAction.List)]
    public partial class ProductsConsumed : FilteredReportPage<ProductsConsumedReport>
    {
		public override string GetFormattedColumnValue(int columnIndex, string value)
		{
			if (!string.IsNullOrEmpty(value) && columnIndex == 2)
			{
				return string.Format("{0:0}", decimal.Parse(value));
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
                FilterHelper.AddCalendarFilterValue(_dateFrom, "DateFrom", FilterPropertyValueType.Equals, filters);
                FilterHelper.AddCalendarFilterValue(_dateTo, "DateTo", FilterPropertyValueType.Equals, filters);
                return filters;
            }
        }
    }
}
