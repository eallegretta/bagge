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
			get { return _new.PostBackUrl; }
			set { _new.PostBackUrl = value; }
		}
		public string NewText
		{
			get { return _new.Text; }
			set { _new.Text = value; }
		}
		public Unit Width
		{
			get { return _new.Width; }
			set { _new.Width = value; }
		}
		protected void Page_Load(object sender, EventArgs e)
		{

		}
	} 
}
