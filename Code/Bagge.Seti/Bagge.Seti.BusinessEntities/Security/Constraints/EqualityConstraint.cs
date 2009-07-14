using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Bagge.Seti.BusinessEntities.Properties;

namespace Bagge.Seti.Security.Constraints
{
	public class EqualsConstraint : Constraint
	{
		public EqualsConstraint(): base()
		{
		}

		public EqualsConstraint(object source, string propertyName, object value)
			: base(source, propertyName, value)
		{

		}
		public EqualsConstraint(object source, PropertyInfo property, object value)
			: base(source, property, value)
		{
		}


		public override bool IsValid()
		{
			object value = GetPropertyValue();
			if (value != null)
				return value.Equals(Value);
			return false;
		}

		public override string ToString()
		{
			return Resources.Constraint_Equal;
		}
	}
}
