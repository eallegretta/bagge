using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bagge.Seti.BusinessEntities.Reports;

namespace Bagge.Seti.WebSite.Reports
{
	public partial class CustomersBySubscription : ReportPage<CustomersBySubscriptionReport>
	{
		public override IList<Bagge.Seti.BusinessEntities.FilterPropertyValue> Filters
		{
			get { return null; }
		}
	}
}
