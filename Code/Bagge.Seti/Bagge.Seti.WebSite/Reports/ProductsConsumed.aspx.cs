﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bagge.Seti.BusinessEntities.Reports;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.WebSite.Helpers;

namespace Bagge.Seti.WebSite.Reports
{
    public partial class ProductsConsumed : FilteredReportPage<ProductsConsumedReport>
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
                FilterHelper.AddTextBoxFilterValue<DateTime>(_dateFrom, "DateFrom", FilterPropertyValueType.Equals, filters);
                FilterHelper.AddTextBoxFilterValue<DateTime>(_dateFrom, "DateTo", FilterPropertyValueType.Equals, filters);

                return filters;
            }
        }
    }
}