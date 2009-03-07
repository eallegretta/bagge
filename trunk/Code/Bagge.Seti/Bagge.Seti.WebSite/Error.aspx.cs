using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bagge.Seti.WebSite
{
	public partial class Error : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Exception ex = null;
			if (Session != null && Session["LastError"] != null)
			{
				ex = (Exception)Session["LastError"];
			}
			else if (Context != null && Context.Items["LastError"] != null)
				ex = (Exception)Context.Items["LastError"];

			if (ex != null)
			{
				ShowErrorMessage(ex);
			}
		}

		private void ShowErrorMessage(Exception ex)
		{
			_errorDescription.Text = ex.StackTrace.Replace(Environment.NewLine, "<br />");

			_error.Text += "<ul>";
			while (ex != null)
			{
				_error.Text += "<li>" + ex.Message + "</li>";

				ex = ex.InnerException;
			}
			_error.Text += "</ul>";
		}
	}
}
