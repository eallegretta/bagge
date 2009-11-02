using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities.Properties;

namespace Bagge.Seti.BusinessEntities.Exceptions
{
	public class AccessDeniedException: BusinessRuleException
	{
		public AccessDeniedException()
			: base(Resources.AccessDeniedErrorMessage)
		{
		}
		public AccessDeniedException(string message)
			: base(message)
		{
		}
	}
}
