using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Bagge.Seti.WebSite
{
	public class Logout : IHttpHandler
	{

		public void ProcessRequest(HttpContext context)
		{
			FormsAuthentication.SignOut();
			context.Response.Redirect("~/Default.aspx");
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
