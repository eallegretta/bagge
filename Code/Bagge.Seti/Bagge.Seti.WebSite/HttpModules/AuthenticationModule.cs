using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.Common;
using Bagge.Seti.WebSite.Properties;
using Bagge.Seti.BusinessEntities.Exceptions;
using System.Security.Principal;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.WebSite.Security;
using System.Web.SessionState;

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
			context.PostAcquireRequestState += new EventHandler(context_PostAcquireRequestState);
		}

		void context_PostAcquireRequestState(object sender, EventArgs e)
		{
			HttpApplication app = (HttpApplication)sender;

			if (!(app.Context.Handler is IRequiresSessionState))
				return;


			if(AuthenticationManager.IsWindowsAuthentication(IoCContainer.AuthenticationProvider))
			{
				if (app.Session["IsAuthenticated"] == null)
				{
					if (Settings.Default.UpdateUserInformationOnLogin)
					{
						Employee user = IoCContainer.EmployeeManager.GetByUsername(IoCContainer.AuthenticationProvider.LoggedInUsername);
						var employeeWithUpdatedInfo = IoCContainer.EmployeeManager.GetFromActiveDirectory(Settings.Default.DCServerAddress,
							Settings.Default.DCLoginUsername, Settings.Default.DCLoginPassword,
							user.Username);
						user.Firstname = employeeWithUpdatedInfo.Firstname;
						user.Lastname = employeeWithUpdatedInfo.Lastname;
						user.Phone = employeeWithUpdatedInfo.Phone;
						user.EmergencyPhone = employeeWithUpdatedInfo.EmergencyPhone;
						user.Email = employeeWithUpdatedInfo.Email;
						user.Password = null;
						IoCContainer.EmployeeManager.UpdateProfile(user);
					}

					app.Session["IsAuthenticated"] = true;
				}
			}
		}

		void context_AuthenticateRequest(object sender, EventArgs e)
		{
			HttpApplication app = (HttpApplication)sender;
			
			if (app.Context.User != null && app.Context.User.Identity.IsAuthenticated)
			{
				try
				{
					if (app.Context.User is WindowsPrincipal)
						IoCContainer.AuthenticationProvider.AuthenticationType = "Windows";
					else
						IoCContainer.AuthenticationProvider.AuthenticationType = "Forms";

					Employee user = IoCContainer.EmployeeManager.GetByUsername(IoCContainer.AuthenticationProvider.LoggedInUsername);
					user.IsAuthenticated = true;
					app.Context.User = new SetiPrincipal(user);
				}
				catch
				{
					app.Context.User = new SetiPrincipal(IoCContainer.EmployeeManager.GetNotAuthorizedUser());
				}
			}
		}

		#endregion
	}
}
