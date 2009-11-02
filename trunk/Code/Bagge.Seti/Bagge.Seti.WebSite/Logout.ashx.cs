using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Bagge.Seti.WebSite.Security;
using Bagge.Seti.Common;

namespace Bagge.Seti.WebSite
{
	public class Logout : IHttpHandler
	{

		public void ProcessRequest(HttpContext context)
		{
			if (AuthenticationManager.IsWindowsAuthentication(IoCContainer.AuthenticationProvider))
			{
				context.Response.Redirect("~/Logoff.aspx");
			}
			else
			{
				FormsAuthentication.SignOut();
				context.Response.Redirect("~/Default.aspx");
			}
		}

		public bool IsReusable
		{
			get
			{
				return false;
			}
		}
	}
}
