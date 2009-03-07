using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bagge.Seti.WebSite
{
	public partial class Site : System.Web.UI.MasterPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		protected void _scriptManager_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
		{
			Session["LastError"] = e.Exception;
			Response.Redirect("~/Error.aspx");
		}
	}
}
