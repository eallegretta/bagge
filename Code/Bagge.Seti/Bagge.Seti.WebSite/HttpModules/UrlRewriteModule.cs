using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;

namespace Bagge.Seti.WebSite.HttpModules
{
	public class UrlRewriteModule: IHttpModule
	{
		#region IHttpModule Members

		public void Dispose()
		{
		}

		public void Init(HttpApplication context)
		{
			context.BeginRequest += new EventHandler(context_BeginRequest);
		}

		void context_BeginRequest(object sender, EventArgs e)
		{
			string path = HttpContext.Current.Request.Url.AbsolutePath;
			if (path.Contains("App_Themes"))
			{
				path = path.Substring(path.IndexOf("App_Themes", StringComparison.InvariantCultureIgnoreCase));
				path = HttpContext.Current.Request.ApplicationPath + path;
				HttpContext.Current.RewritePath(path);
			}
		}

		#endregion
	}
}
