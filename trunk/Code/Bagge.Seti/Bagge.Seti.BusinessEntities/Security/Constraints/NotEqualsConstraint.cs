using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.Security.Constraints;
using System.Reflection;
using Bagge.Seti.BusinessEntities.Properties;

namespace Bagge.Seti.BusinessEntities.Security.Constraints
{
	public class NotEqualsConstraint : InequalityConstraint
	{
		public NotEqualsConstraint()
			: base()
		{
		}

		public NotEqualsConstraint(object source, string propertyName, object value)
			: base(source, propertyName, value)
		{

		}
		public NotEqualsConstraint(object source, PropertyInfo property, object value)
			: base(source, property, value)
		{
		}


		public override bool IsValid()
		{
			object value = GetPropertyValue();
			if (value != null)
				return ((IComparable)value).CompareTo((IComparable)Value) != 0;
			return false;
		}

		public override string ToString()
		{
			return Resources.Constraint_NotEqual;
		}
	}
}
