using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using Bagge.Seti.BusinessEntities.Reports;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.WebSite.Helpers;

namespace Bagge.Seti.WebSite.Reports
{
	[SecurizableCrud("Securizable_ProductsByProvider", typeof(ProductsByProvider), FunctionAction.List)]
    public partial class ProductsByProvider : FilteredReportPage<ProductsByProviderReport>
    {
		public override string GetFormattedColumnValue(int columnIndex, string value)
		{
			if (!string.IsNullOrEmpty(value) && columnIndex == 2)
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
				FilterHelper.AddTextBoxFilterValue<string>(_productName, "ProductName", FilterPropertyValueType.Like, filters);
				FilterHelper.AddTextBoxFilterValue<string>(_providerName, "ProviderName", FilterPropertyValueType.Like, filters);
				return filters;
			}
		}
	}
}
