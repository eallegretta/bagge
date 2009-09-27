using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.WebSite.Security;
using System.Web.UI;

namespace Bagge.Seti.WebSite.Controls
{
	public class SiteMapProvider: XmlSiteMapProvider
	{
		private Dictionary<string, bool> _checkedUrls;

		public Dictionary<string, bool> CheckedUrls
		{
			get
			{
				if (_checkedUrls == null)
					_checkedUrls = new Dictionary<string, bool>();
				return _checkedUrls;
			}
		}


		public override bool IsAccessibleToUser(System.Web.HttpContext context, System.Web.SiteMapNode node)
		{

			var user = context.User.Identity as IUser;
			if (user == null)
				return false;

			if (user.IsSuperAdministrator)
				return true;

			string absoluteUrl = context.Server.MapPath(node.Url);

			if (!string.IsNullOrEmpty(node.Url))
			{
				if (CheckedUrls.ContainsKey(node.Url))
					return CheckedUrls[node.Url];

				var page = PageParser.GetCompiledPageInstance(node.Url, absoluteUrl, context);
				CheckedUrls.Add(node.Url, AuthorizationManager.UserHasAccess(page, false));

				return CheckedUrls[node.Url];
			}
			else
				return CheckAccessToChildNodes(context, node);
		}

		private bool CheckAccessToChildNodes(HttpContext context, SiteMapNode rootNode)
		{
			foreach (SiteMapNode node in rootNode.ChildNodes)
			{
				if (IsAccessibleToUser(context, node))
					return true;
				if (CheckAccessToChildNodes(context, node))
					return true;
			}
			return false;
		}
	}
}
