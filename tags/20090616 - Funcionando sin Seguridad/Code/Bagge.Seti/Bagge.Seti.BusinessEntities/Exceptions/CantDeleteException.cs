using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bagge.Seti.BusinessEntities.Exceptions
{
	public class CantDeleteException: BusinessRuleException
	{
		public CantDeleteException()
		{
		}

		public CantDeleteException(string message)
			: base(message)
		{
		}

		public CantDeleteException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
