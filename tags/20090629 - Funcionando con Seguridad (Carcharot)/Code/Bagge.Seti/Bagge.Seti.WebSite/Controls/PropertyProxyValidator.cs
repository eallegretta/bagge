using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Validation.Integration;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System.Text;
using System.Web.UI.WebControls;
using Bagge.Seti.Common.Validation;

namespace Bagge.Seti.WebSite.Controls
{
	public class PropertyProxyValidator: Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet.PropertyProxyValidator
	{
		protected override bool EvaluateIsValid()
		{
			Validator validator = new ValidationIntegrationHelper(this).GetValidator();
			if (validator != null)
			{
				ValidationResults results = validator.Validate(this);
				base.ErrorMessage = FormatErrorMessage(results, this.DisplayMode);
				return results.IsValid;
			}
			base.ErrorMessage = "";
			return true;

		}

		private static string FormatErrorMessage(ValidationResults results, System.Web.UI.WebControls.ValidationSummaryDisplayMode validationSummaryDisplayMode)
		{
			string str;
			string str2;
			string str3;
			string str4;
			StringBuilder builder = new StringBuilder();
			switch (validationSummaryDisplayMode)
			{
				case ValidationSummaryDisplayMode.List:
					str = string.Empty;
					str2 = string.Empty;
					str3 = "<br/>";
					str4 = string.Empty;
					break;

				case ValidationSummaryDisplayMode.SingleParagraph:
					str = string.Empty;
					str2 = string.Empty;
					str3 = " ";
					str4 = "<br/>";
					break;

				default:
					str = "<ul>";
					str2 = "<li>";
					str3 = "</li>";
					str4 = "</ul>";
					break;
			}
			if (!results.IsValid)
			{
				builder.Append(str);
				foreach (string errorMessage in EnterpriseLibraryValidationResultsHelper.GetErrorMessages(results))
				{
					builder.Append(str2);
					builder.Append(errorMessage);
					builder.Append(str3);
				}
				builder.Append(str4);
			}
			return builder.ToString();

		}
	}
}
