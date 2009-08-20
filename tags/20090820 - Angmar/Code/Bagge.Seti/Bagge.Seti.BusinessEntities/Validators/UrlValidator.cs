using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System.Text.RegularExpressions;

namespace Bagge.Seti.BusinessEntities.Validators
{

	public class UrlValidator : ValueValidator<string>
	{
		private const string UrlPattern = @"(((file|gopher|news|nntp|telnet|http|ftp|https|ftps|sftp)://)|(www\.))+(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(/[a-zA-Z0-9\&amp;%_\./-~-]*)?";

		private bool _required;

		public UrlValidator(bool required, string messageTemplate, bool negated)
			: base(messageTemplate, null, negated)
		{
			_required = required;
		}

		protected override string DefaultNegatedMessageTemplate
		{
			get { return "Field must not be an url"; }
		}

		protected override string DefaultNonNegatedMessageTemplate
		{
			get { return "Field must be an url"; }
		}

		protected override void DoValidate(string objectToValidate, object currentTarget, string key, Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResults validationResults)
		{
			if (!_required && string.IsNullOrEmpty(objectToValidate))
				return;

			if (Negated)
			{
				if (Regex.IsMatch(objectToValidate, UrlPattern))
					LogValidationResult(validationResults, GetMessage(objectToValidate, key), currentTarget, key);
			}
			else
			{
				if (!Regex.IsMatch(objectToValidate, UrlPattern))
					LogValidationResult(validationResults, GetMessage(objectToValidate, key), currentTarget, key);
			}
		}
	}
}
