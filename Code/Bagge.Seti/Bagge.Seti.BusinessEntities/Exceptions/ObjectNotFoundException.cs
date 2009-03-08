using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities.Properties;

namespace Bagge.Seti.BusinessEntities.Exceptions
{
	public class ObjectNotFoundException: BusinessRuleException
	{
		public ObjectNotFoundException()
			: base(Resources.ObjectNotFoundErrorMessage)
		{
		}
	}
}
