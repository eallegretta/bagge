using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
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

namespace Bagge.Seti.WebSite.Reports
{
	[SecurizableCrud("Securizable_CustomersWithPendingPayment", typeof(CustomersWithPendingPayment), FunctionAction.Retrieve)]
    public partial class CustomersWithPendingPayment : ReportPage<CustomersWithPendingPaymentReport>
    {
    }
}
	