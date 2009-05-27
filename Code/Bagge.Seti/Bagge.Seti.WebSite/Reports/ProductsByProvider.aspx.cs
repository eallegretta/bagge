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

namespace Bagge.Seti.WebSite.Reports
{
    public partial class ProductsByProvider : ReportPage<ProductsByProviderReport>
    {
        public override IList<Bagge.Seti.BusinessEntities.FilterPropertyValue> Filters
        {
            get { return null; }
        }
    }
}
