using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bagge.Seti.Common;
using System.Web.Security;

namespace Bagge.Seti.WebSite
{
	public partial class Login : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.QueryString["Logout"] != null)
			{
				FormsAuthentication.SignOut();
				Response.Redirect("~/Default.aspx");
			}
		}

		protected void _login_Authenticate(object sender, AuthenticateEventArgs e)
		{
			e.Authenticated = IoCContainer.EmployeeManager.Authenticate(_login.UserName, _login.Password);
		}

		protected void _login_LoggedIn(object sender, EventArgs e)
		{
			FormsAuthentication.RedirectFromLoginPage(_login.UserName, _login.RememberMeSet);
		}
	}
}
