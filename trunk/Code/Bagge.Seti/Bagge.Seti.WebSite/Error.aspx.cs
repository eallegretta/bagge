using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bagge.Seti.BusinessEntities.Exceptions;
using System.Net;

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
			Exception ex = Context.Items["LastError"] as Exception;
			if (ex == null)
				ex = Session["LastError"] as Exception;

			if (ex != null)
			{
				var newEx = GetBusinessRuleException(ex);
				if (newEx != null)
					ShowBusinessRuleErrorMessage(newEx);
				else
					ShowErrorMessage(ex);
			}
			else if (Request.QueryString["StatusCode"] != null)
			{
				int statusCode;
				if (int.TryParse(Request.QueryString["StatusCode"], out statusCode))
				{
					switch ((HttpStatusCode)statusCode)
					{
						case HttpStatusCode.Unauthorized:
							ShowBusinessRuleErrorMessage(Properties.Resources.StatusCode401Error);
							break;
						case HttpStatusCode.NotFound:
							ShowBusinessRuleErrorMessage(Properties.Resources.StatusCode404Error);
							break;
						case HttpStatusCode.Forbidden:
							ShowBusinessRuleErrorMessage(Properties.Resources.StatusCode403Error);
							break;
					}
				}
			}
            Server.ClearError();
		}

		private void ShowBusinessRuleErrorMessage(string error)
		{
			_errorMessage.Text = error;
		}
		private void ShowBusinessRuleErrorMessage(Exception ex)
		{
			_errorMessage.Text = ex.Message;
		}
		private void ShowErrorMessage(string error)
		{
			_error.Text = error;
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
