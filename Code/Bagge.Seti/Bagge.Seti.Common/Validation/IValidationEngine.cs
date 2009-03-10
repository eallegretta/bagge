using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bagge.Seti.Common.Validation
{
	public interface IValidationEngine
	{
		bool IsValid(object instance);
		string[] GetErrorMessages(object instance);
	}
}
