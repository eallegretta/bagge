using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Bagge.Seti.Security.Constraints
{
	public class EqualityConstraint : Constraint
	{
		public EqualityConstraint(object source, string propertyName, object value)
			: base(source, propertyName, value)
		{

		}
		public EqualityConstraint(object source, PropertyInfo property, object value)
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
	}
}
