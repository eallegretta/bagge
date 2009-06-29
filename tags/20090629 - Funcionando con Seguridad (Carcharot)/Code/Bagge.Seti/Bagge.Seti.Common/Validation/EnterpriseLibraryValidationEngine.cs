using Microsoft.Practices.EnterpriseLibrary.Validation;
using System.Collections.Generic;
using System.Linq;


namespace Bagge.Seti.Common.Validation
{
	public class EnterpriseLibraryValidationEngine: IValidationEngine
	{
		#region IValidationEngine Members

		public bool IsValid(object instance)
		{
			ValidationResults results = GetValidationResults(instance);

			return results.IsValid;
		}

		private static ValidationResults GetValidationResults(object instance)
		{
			Validator validator = ValidationFactory.CreateValidator(instance.GetType());

			ValidationResults results = validator.Validate(instance);

			return results;
		}

		public string[] GetErrorMessages(object instance)
		{
			return EnterpriseLibraryValidationResultsHelper.GetErrorMessages(GetValidationResults(instance));
		}
		#endregion
	}
}
