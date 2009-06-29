using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.BusinessEntities.Security;

namespace Bagge.Seti.WebSite.Controls
{
	public class SiteMapProvider: XmlSiteMapProvider
	{
		public override bool IsAccessibleToUser(System.Web.HttpContext context, System.Web.SiteMapNode node)
		{
			var user = context.User.Identity as IUser;
			if (user == null)
				return false;

			return true;
		}
	}
}
