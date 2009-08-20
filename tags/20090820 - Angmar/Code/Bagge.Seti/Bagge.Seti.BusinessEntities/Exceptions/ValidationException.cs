using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bagge.Seti.BusinessEntities.Exceptions
{
	public class ValidationException: BusinessRuleException
	{
		public ValidationException(string message)
			: base(message)
		{
		}
	}
}
