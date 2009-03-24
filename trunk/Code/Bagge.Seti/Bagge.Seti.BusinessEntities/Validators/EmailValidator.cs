using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System.Text.RegularExpressions;

namespace Bagge.Seti.BusinessEntities.Validators
{
	public class EmailValidator: ValueValidator<string>
	{
		private const string EmailPattern = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

		private bool _required;

		public EmailValidator(bool required, string messageTemplate, bool negated): base(messageTemplate, null, negated)
		{
			_required = required;
		}

		protected override string DefaultNegatedMessageTemplate
		{
			get { return "Field must not be an email"; }
		}

		protected override string DefaultNonNegatedMessageTemplate
		{
			get { return "Field must be an email"; }
		}

		protected override void DoValidate(string objectToValidate, object currentTarget, string key, Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResults validationResults)
		{
			if (!_required && string.IsNullOrEmpty(objectToValidate))
				return;

			if (Negated)
			{
				if (Regex.IsMatch(objectToValidate, EmailPattern))
					LogValidationResult(validationResults, GetMessage(objectToValidate, key), currentTarget, key);
			}
			else
			{
				if (!Regex.IsMatch(objectToValidate, EmailPattern))
					LogValidationResult(validationResults, GetMessage(objectToValidate, key), currentTarget, key);
			}
		}
	}
}
