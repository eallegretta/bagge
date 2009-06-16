using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace Bagge.Seti.Common.Validation
{
	public static class EnterpriseLibraryValidationResultsHelper
	{
		public static string[] GetErrorMessages(ValidationResults results)
		{
			List<string> errors = new List<string>();

			if (!results.IsValid)
			{
				foreach (ValidationResult result in results)
				{
					if (result.NestedValidationResults != null && result.NestedValidationResults.Count() > 0)
						AddNestedErrorMessages(errors, result);
					else
						errors.Add(result.Message);

				}
			}

			return errors.ToArray();
		}

		private static void AddNestedErrorMessages(List<string> errors, ValidationResult parentResult)
		{
			foreach (ValidationResult result in parentResult.NestedValidationResults.ToArray())
			{
				if (result.NestedValidationResults != null && result.NestedValidationResults.Count() > 0)
					AddNestedErrorMessages(errors, result);
				else
					errors.Add(result.Message);
			}
		}
	}
}
