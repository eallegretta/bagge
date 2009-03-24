using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bagge.Seti.WebSite.Views;

namespace Bagge.Seti.WebSite
{
	public partial class Site : System.Web.UI.MasterPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Page is IEditorView)
			{
				string title = string.Empty;
				switch (((IEditorView)Page).Mode)
				{
					case EditorAction.Update:
						title = this._siteMapDataSource.Provider.CurrentNode["editTitle"];
						break;
					case EditorAction.View:
						title = this._siteMapDataSource.Provider.CurrentNode["viewTitle"];
						break;
				}

				if (!string.IsNullOrEmpty(title))
					((Literal)this._siteMapPath.Controls[_siteMapPath.Controls.Count - 1].Controls[0]).Text = title;
			}
		}

		protected void _scriptManager_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
		{
			Session["LastError"] = e.Exception;
			Response.Redirect("~/Error.aspx");
		}
	}
}
