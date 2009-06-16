using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Bagge.Seti.WebSite.Controls
{
	public partial class ListCommands : System.Web.UI.UserControl
	{
		public string PostBackUrl
		{
			get { return ViewState["PostBackUrl"].ToString() ?? string.Empty; }
			set { ViewState["PostBackUrl"] = value; }
		}
		public string NewText
		{
			get { return _newText.Text; }
			set { _newText.Text = value; }
		}
	
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		protected void _new_ServerClick(object sender, EventArgs e)
		{
			Response.Redirect(PostBackUrl);
		}
	} 
}
