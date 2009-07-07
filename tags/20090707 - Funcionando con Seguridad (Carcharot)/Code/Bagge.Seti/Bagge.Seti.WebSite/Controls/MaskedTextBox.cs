using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Threading;
using System.Web.UI;

namespace Bagge.Seti.WebSite.Controls
{
	public class MaskedTextBox: TextBox
	{
		public string Mask
		{
			get { return ViewState["Mask"] as string; }
			set
			{
				value = value.Replace("{NumberSeparator}", Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
				ViewState["Mask"] = value;
			}
		}

		public string MaskPlaceHolder
		{
			get { return ViewState["MaskPlaceHolder"] as string; }
			set { ViewState["MaskPlaceHolder"] = value; }
		}

		protected override void OnInit(EventArgs e)
		{
			if (!Page.ClientScript.IsClientScriptIncludeRegistered("MaskedTextBoxMask"))
				Page.ClientScript.RegisterClientScriptInclude("MaskedTextBoxMask", Page.ResolveUrl("~/Scripts/jquery.maskedinput-1.2.2.min.js"));
			
			base.OnInit(e);
		}

		protected override void Render(System.Web.UI.HtmlTextWriter writer)
		{
			base.Render(writer);
			string placeHolder = string.Empty;
			if (!string.IsNullOrEmpty(MaskPlaceHolder))
				placeHolder = string.Format(", {{ placeholder: '{0}' }}", MaskPlaceHolder);

			string mask = string.Format("<script type='text/javascript'>$('#{0}').mask('{1}'{2});</script>", ClientID, Mask, placeHolder);

			if (ScriptManager.GetCurrent(Page).IsInAsyncPostBack)
				ScriptManager.RegisterStartupScript(this, typeof(string), "MaskedTextBoxMask_Mask", mask, false);
			else
				writer.Write(mask);
		}
	}
}
