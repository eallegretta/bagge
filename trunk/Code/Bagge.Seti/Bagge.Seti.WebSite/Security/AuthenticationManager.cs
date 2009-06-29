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

namespace Bagge.Seti.WebSite.Security
{
	public class AuthenticationManager: IAuthenticator
	{
		#region IAuthenticator Members

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

		#endregion
	}
}
