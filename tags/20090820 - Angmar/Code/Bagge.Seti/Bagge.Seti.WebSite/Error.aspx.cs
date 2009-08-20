using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bagge.Seti.BusinessEntities.Exceptions;

namespace Bagge.Seti.WebSite
{
	public partial class Error : System.Web.UI.Page
	{
		private Exception GetBusinessRuleException(Exception ex)
		{
			if (ex is BusinessRuleException)
				return ex;
			else if (ex.InnerException != null)
				return GetBusinessRuleException(ex.InnerException);

			return null;
			
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			Exception ex = null;
			if (Context != null && Context.Items["LastError"] != null) 
				ex = (Exception)Context.Items["LastError"];
			else if (Session != null && Session["LastError"] != null)
				ex = (Exception)Session["LastError"];

			if (ex != null)
			{
				var newEx = GetBusinessRuleException(ex);
				if (newEx != null)
					ShowBusinessRuleErrorMessage(newEx);
				else
					ShowErrorMessage(ex);
			}
		}

		private void ShowBusinessRuleErrorMessage(Exception ex)
		{
			_errorMessage.Text = ex.Message;
		}

		private void ShowErrorMessage(Exception ex)
		{
			

			_error.Text += "<ul>";
			while (ex != null)
			{
				_error.Text += "<li>" + ex.Message + "</li>";
				_errorDescription.Text = ex.StackTrace.Replace(Environment.NewLine, "</li><li>") + _errorDescription.Text;
				ex = ex.InnerException;
			}
			_errorDescription.Text = "<ul><li>" + _errorDescription.Text + "</li></ul>";
			_error.Text += "</ul>";
		}
	}
}
