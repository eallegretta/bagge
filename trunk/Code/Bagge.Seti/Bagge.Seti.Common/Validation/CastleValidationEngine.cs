using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Components.Validator;

namespace Bagge.Seti.Common.Validation
{
	public class CastleValidationEngine: IValidationEngine
	{
		ValidatorRunner _runner;

		public CastleValidationEngine(ValidatorRunner runner)
		{
			
			_runner = runner;
		}

		#region IValidationEngine Members

		public bool IsValid(object instance)
		{
			return _runner.IsValid(instance);
		}

		#endregion

		#region IValidationEngine Members


		public string[] GetErrorMessages(object instance)
		{
			return _runner.GetErrorSummary(instance).ErrorMessages;
		}

		#endregion
	}
}
