using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.BusinessEntities.Security
{
	[AttributeUsage(AttributeTargets.Class)]
	public class SecurizableWebAttribute: SecurizableAttribute
	{
		public SecurizableWebAttribute(string name, FunctionAction action)
			: base(name)
		{
			Action = action;
		}

		public SecurizableWebAttribute(string nameResourceName, Type nameResourceType, FunctionAction action)
			: base(nameResourceName, nameResourceType)
		{
			Action = action;
		}

		public FunctionAction Action { get; set; }
	}
}
