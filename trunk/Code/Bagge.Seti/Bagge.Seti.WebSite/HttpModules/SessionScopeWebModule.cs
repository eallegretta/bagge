using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Scopes;

namespace Bagge.Seti.WebSite.HttpModules
{
	public class SessionScopeWebModule: IHttpModule
	{
		private static readonly string SessionKey;

		static SessionScopeWebModule()
		{
			SessionKey = "SessionScopeWebModule.session";
		}

 


		#region IHttpModule Members

		public void Dispose()
		{
		}

		public void Init(HttpApplication context)
		{
			context.BeginRequest += new EventHandler(context_BeginRequest);
			context.EndRequest += new EventHandler(context_EndRequest); 
		}

		void context_EndRequest(object sender, EventArgs e)
		{
			try
			{
				SessionScope scope = (SessionScope)HttpContext.Current.Items[SessionKey];
				if (scope != null)
				{
					scope.Dispose();
				}
			}
			catch (ScopeMachineryException)
			{
			}
		}

		void context_BeginRequest(object sender, EventArgs e)
		{
			HttpContext.Current.Items.Add(SessionKey, new SessionScope());
		}

		#endregion
	}
}
