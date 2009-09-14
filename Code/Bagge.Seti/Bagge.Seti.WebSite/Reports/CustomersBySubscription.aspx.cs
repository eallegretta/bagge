using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bagge.Seti.BusinessEntities.Reports;
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.WebSite.Reports
{
	[SecurizableCrud("Securizable_CustomersBySubscription", typeof(CustomersBySubscription), FunctionAction.List)]
	public partial class CustomersBySubscription : ReportPage<CustomersBySubscriptionReport>
	{

	}
}
