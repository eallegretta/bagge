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
using Bagge.Seti.WebSite.Security;

namespace Bagge.Seti.WebSite
{
	public partial class Site : System.Web.UI.MasterPage
	{
		protected bool IsWindowsAuthentication
		{
			get;
			set;
		}


		protected override void OnInit(EventArgs e)
		{
			StringBuilder script = new StringBuilder();
			script.AppendFormat(@"<script type=""text/javascript"" src=""{0}""></script>", Page.ResolveUrl("~/Scripts/jquery-1.3.2.min.js"));
			script.AppendFormat(@"<script type=""text/javascript"" src=""{0}""></script>", Page.ResolveUrl("~/Scripts/jquery.numeric.pack.js"));
			script.AppendFormat(@"<script type=""text/javascript"" src=""{0}""></script>", Page.ResolveUrl("~/Scripts/json2.js"));
			script.AppendFormat(@"<script type=""text/javascript"" src=""{0}""></script>", Page.ResolveUrl("~/Scripts/Site.Master.js"));
			script.AppendFormat(@"<script type=""text/javascript"" src=""{0}""></script>", Page.ResolveUrl("~/Scripts/MaskedEditFix.js"));

			Page.Header.Controls.AddAt(0, new LiteralControl(script.ToString()));

			base.OnInit(e);

			SetLoginStatusVisiblity();

			IsWindowsAuthentication = AuthenticationManager.IsWindowsAuthentication(IoCContainer.AuthenticationProvider);

			if (IsWindowsAuthentication)
				_editProfile.Visible = false;


			SetCurrentViewTitle();
		}

		private void SetLoginStatusVisiblity()
		{
			_loginFullnameView.Visible = _menuLoginView.Visible = Page.Request.IsAuthenticated;
			if (Page.Request.IsAuthenticated)
				SetLoggedInFullName();
		}

		private void SetCurrentViewTitle()
		{
			if (this._siteMapDataSource.Provider.CurrentNode != null)
			{
				if (Page is IEditorView)
				{
					switch (((IEditorView)Page).Mode)
					{
						case EditorAction.Update:
							CurrentViewTitle = this._siteMapDataSource.Provider.CurrentNode["editTitle"];
							break;
						case EditorAction.View:
							CurrentViewTitle = this._siteMapDataSource.Provider.CurrentNode["viewTitle"];
							break;
					}
				}
				else
					CurrentViewTitle = this._siteMapDataSource.Provider.CurrentNode["title"];
			}
			else
				CurrentViewTitle = Page.Title;
		}


		public string CurrentViewTitle
		{
			get;
			private set;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(CurrentViewTitle))
			{
				if (_siteMapPath.Controls.Count == 0)
				{
					var label = new Label();
					label.Text = CurrentViewTitle;
					_siteMapPath.CurrentNodeStyle.CopyTo(label.ControlStyle);
					_siteMapPath.Controls.Add(label);
				}
				else
					((Literal)this._siteMapPath.Controls[_siteMapPath.Controls.Count - 1].Controls[0]).Text = CurrentViewTitle;
			}
		}


		private void SetLoggedInFullName()
		{
			if (Page.User is SetiPrincipal && Page.User.Identity is Employee)
			{
				_loginFullname.Text = ((Employee)Page.User.Identity).Fullname;
			}
		}

		protected void _scriptManager_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
		{
			Session["LastError"] = e.Exception;
			Response.Redirect("~/Error.aspx");
		}
	}
}
