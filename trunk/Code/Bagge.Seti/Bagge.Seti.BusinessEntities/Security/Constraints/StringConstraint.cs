using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Bagge.Seti.DesignByContract;

namespace Bagge.Seti.Security.Constraints
{
	public abstract class StringConstraint : Constraint
	{
		public StringConstraint()
			: base()
		{
		}

		public StringConstraint(object source, string propertyName, object value, bool negated)
			: base(source, propertyName, value, negated)
		{

		}
		public StringConstraint(object source, PropertyInfo property, object value, bool negated)
			: base(source, property, value, negated)
		{
		}

		protected override bool IsPropertyTypeValid()
		{
			Check.Require(Property != null);

			return true;

			//if (Property.PropertyType.Equals(typeof(string)))
			//    return true;
			//return false;
		}

		public abstract bool IsTrue(string valueA, string valueB);

		public override bool IsTrue()
		{
            object propertyValueA = GetPropertyValue();
			string valueA = propertyValueA != null ? propertyValueA.ToString() : null;
			string valueB = Value as string;
			if (!string.IsNullOrEmpty(valueA) && !string.IsNullOrEmpty(valueB))
			{
				bool value = IsTrue(valueA, valueB);
				return Negated ? !value : value;
			}
			return false;
		}
	}
}
