using Microsoft.Practices.EnterpriseLibrary.Validation;
using System.Collections.Generic;


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
			Validator validator = ValidationFactory.CreateValidatorFromConfiguration(instance.GetType(), "Rules");

			ValidationResults results = validator.Validate(instance);

			return results;
		}

		public string[] GetErrorMessages(object instance)
		{
			ValidationResults results = GetValidationResults(instance);

			List<string> errors = new List<string>();

			if (!results.IsValid)
			{
				foreach (ValidationResult result in results)
				{
					errors.Add(result.Message);
				}
			}

			return errors.ToArray();
		}

		#endregion
	}
}
