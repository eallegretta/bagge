using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Bagge.Seti.Common;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.WebSite.Properties;

namespace Bagge.Seti.WebSite.Security
{
	public class AuthenticationManager: IAuthenticator
	{
		#region IAuthenticator Members

		public static bool IsWindowsAuthentication(IAuthenticator auth)
		{
			return string.Equals(auth.AuthenticationType, "Windows", StringComparison.InvariantCultureIgnoreCase);
		}

		public System.Security.Principal.IPrincipal LoggedInUser
		{
			get
			{
				return HttpContext.Current.User;
			}
			set
			{
				HttpContext.Current.User = value;
			}
		}

		public string AuthenticationType
		{
			get
			{

				object authenticationType = HttpContext.Current.Items["AuthenticationType"];
				if (authenticationType is string)
					return authenticationType.ToString();
				return null;
			}
			set
			{
				HttpContext.Current.Items["AuthenticationType"] = value;
			}
		}

		public string LoggedInUsername
		{
			get
			{
				if (!LoggedInUser.Identity.IsAuthenticated)
					return null;

				return LoggedInUser.Identity.Name;
			}
		}

		#endregion
	}
}
