using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace Bagge.Seti.BusinessEntities.Validators
{
	public class RequiredStringValidatorAttribute: ValueValidatorAttribute
	{
		protected override Validator DoCreateValidator(Type targetType)
		{
			return new RequiredStringValidator(base.MessageTemplate, base.Negated);
		}
	}
}
