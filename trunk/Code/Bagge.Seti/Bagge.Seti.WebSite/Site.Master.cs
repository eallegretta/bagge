using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.Common;
using Bagge.Seti.BusinessEntities.Security;
using System.Text;

namespace Bagge.Seti.WebSite
{
	public partial class Site : System.Web.UI.MasterPage
	{
		protected override void OnInit(EventArgs e)
		{
			StringBuilder script = new StringBuilder();
			script.AppendFormat(@"<script type=""text/javascript"" src=""{0}""></script>", Page.ResolveUrl("~/Scripts/jquery-1.3.2.min.js"));
			script.AppendFormat(@"<script type=""text/javascript"" src=""{0}""></script>", Page.ResolveUrl("~/Scripts/jquery.numeric.pack.js"));
			script.AppendFormat(@"<script type=""text/javascript"" src=""{0}""></script>", Page.ResolveUrl("~/Scripts/json2.js"));
			script.AppendFormat(@"<script type=""text/javascript"" src=""{0}""></script>", Page.ResolveUrl("~/Scripts/Site.Master.js"));

			Page.Header.Controls.AddAt(0, new LiteralControl(script.ToString()));

			base.OnInit(e);
			SetLoggedInFullName();
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Page is IEditorView)
			{
				string title = string.Empty;
				if (this._siteMapDataSource.Provider.CurrentNode != null)
				{
					switch (((IEditorView)Page).Mode)
					{
						case EditorAction.Update:
							title = this._siteMapDataSource.Provider.CurrentNode["editTitle"];
							break;
						case EditorAction.View:
							title = this._siteMapDataSource.Provider.CurrentNode["viewTitle"];
							break;
					}
				}
				else
					title = Page.Title;

				if (!string.IsNullOrEmpty(title))
				{
					if (_siteMapPath.Controls.Count == 0)
					{
						var label = new Label();
						label.Text = title;
						_siteMapPath.CurrentNodeStyle.CopyTo(label.ControlStyle);
						_siteMapPath.Controls.Add(label);
					}
					else
						((Literal)this._siteMapPath.Controls[_siteMapPath.Controls.Count - 1].Controls[0]).Text = title;
				}
			}
		}


		private void SetLoggedInFullName()
		{
			if (Page.User is SetiPrincipal && Page.User.Identity is Employee)
			{
				var fullName = _loginFullnameView.FindControl("_loginFullname") as Literal;
				if(fullName != null)
					fullName.Text = ((Employee)Page.User.Identity).Fullname;
			}
		}

		protected void _scriptManager_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
		{
			Session["LastError"] = e.Exception;
			Response.Redirect("~/Error.aspx");
		}
	}
}
