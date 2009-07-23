using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bagge.Seti.Common;

namespace Bagge.Seti.WebSite
{
	public partial class RecoverPassword : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(Request.QueryString["k"]))
				SendPassword();
		}

		private void SendPassword()
		{
			_recoverPassword.ActiveViewIndex = 2;
			IoCContainer.EmployeeManager.RegeneratePassword(Request.QueryString["k"]);
		}

		protected void _send_Click(object sender, EventArgs e)
		{
			IoCContainer.EmployeeManager.RecoverPassword(_emailAddress.Text, Page.Request.Url.AbsoluteUri + "?k=");
			_recoverPassword.ActiveViewIndex = 1;
		}
	}
}
