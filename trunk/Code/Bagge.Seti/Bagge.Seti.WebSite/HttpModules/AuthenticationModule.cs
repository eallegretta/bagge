using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.Common;

namespace Bagge.Seti.WebSite.HttpModules
{
	public class AuthenticationModule: IHttpModule
	{
		#region IHttpModule Members

		public void Dispose()
		{
		}

		public void Init(HttpApplication context)
		{
			context.AuthenticateRequest += new EventHandler(context_AuthenticateRequest);
		}


		void context_AuthenticateRequest(object sender, EventArgs e)
		{
			HttpApplication app = (HttpApplication)sender;
			
			if (app.Context.User != null && app.Context.User.Identity.IsAuthenticated)
			{
				IUser user = IoCContainer.EmployeeManager.GetByUsername(app.Context.User.Identity.Name);
				user.IsAuthenticated = true;
				app.Context.User = new SetiPrincipal(user);
			}
		}

		#endregion
	}
}
