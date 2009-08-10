using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.BusinessEntities.Security
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class SecurizableCrudAttribute: SecurizableAttribute
	{
		public SecurizableCrudAttribute(string name, FunctionAction action)
			: base(name)
		{
			Action = action;
		}

		public SecurizableCrudAttribute(string nameResourceName, Type nameResourceType, FunctionAction action)
			: base(nameResourceName, nameResourceType)
		{
			Action = action;
		}

		public FunctionAction Action { get; set; }
	}
}
