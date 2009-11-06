using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Bagge.Seti.BusinessEntities.Validators
{
	public class DateTimeValidator : ValueValidator
	{
		private bool _required;

		public DateTimeValidator(bool required, string messageTemplate, bool negated)
			: base(messageTemplate, null, negated)
		{
			_required = required;
		}

		protected override string DefaultNegatedMessageTemplate
		{
			get { return "Field must be non valid Date"; }
		}

		protected override string DefaultNonNegatedMessageTemplate
		{
			get { return "Field must be a valid Date"; }
		}

		

		protected override void DoValidate(object objectToValidate, object currentTarget, string key, Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResults validationResults)
		{
			if (!_required && objectToValidate == null)
				return;

			DateTime date = new DateTime();
			if (objectToValidate is DateTime)
				date = (DateTime)objectToValidate;
			else if (objectToValidate is DateTime?)
				date = ((DateTime?)objectToValidate).Value;

			bool isValid = date.Year >= 1950 && date.Year <= 2030;

			if ((!Negated && !isValid) || (Negated && isValid))
			{
				LogValidationResult(validationResults, GetMessage(objectToValidate, key), currentTarget, key);
			}
		}
	}
}
