using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Bagge.Seti.BusinessEntities.Validators
{
	public class RequiredStringValidator: ValueValidator<string>
	{
		public RequiredStringValidator(string messageTemplate, bool negated)
			: base(messageTemplate, null, negated)
		{
		}

		protected override string DefaultNegatedMessageTemplate
		{
			get { return "Field cannot have a value"; }
		}

		protected override string DefaultNonNegatedMessageTemplate
		{
			get { return "Required field"; }
		}

		protected override void DoValidate(string objectToValidate, object currentTarget, string key, Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResults validationResults)
		{
			if (string.IsNullOrEmpty(objectToValidate) != Negated)
				LogValidationResult(validationResults, GetMessage(objectToValidate, key), currentTarget, key);
		}
	}
}
