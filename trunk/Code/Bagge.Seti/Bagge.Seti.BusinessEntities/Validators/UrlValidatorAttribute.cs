using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Bagge.Seti.BusinessEntities.Validators
{
	public class UrlValidatorAttribute : ValueValidatorAttribute
	{
		public bool Required
		{
			get;
			set;
		}

		protected override Microsoft.Practices.EnterpriseLibrary.Validation.Validator DoCreateValidator(Type targetType)
		{
			return new UrlValidator(Required, base.MessageTemplate, base.Negated);
		}
	}
}
