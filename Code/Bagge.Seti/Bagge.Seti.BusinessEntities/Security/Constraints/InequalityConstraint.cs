using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Bagge.Seti.Security.Constraints
{
	public abstract class InequalityConstraint : Constraint
	{
		public InequalityConstraint(object source, string propertyName, object value)
			: base(source, propertyName, value)
		{

		}
		public InequalityConstraint(object source, PropertyInfo property, object value)
			: base(source, property, value)
		{
		}

		protected override bool IsPropertyTypeValid()
		{
			if (Property.PropertyType.Equals(typeof(IComparable)))
				return true;
			return false;
		}
	}
}
