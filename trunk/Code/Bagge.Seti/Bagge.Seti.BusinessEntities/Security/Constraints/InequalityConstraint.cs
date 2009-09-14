using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Bagge.Seti.DesignByContract;

namespace Bagge.Seti.Security.Constraints
{
	public abstract class InequalityConstraint : Constraint
	{
		public InequalityConstraint()
			: base()
		{
		}

		public InequalityConstraint(object source, string propertyName, object value, bool negated)
			: base(source, propertyName, value, negated)
		{

		}
		public InequalityConstraint(object source, PropertyInfo property, object value, bool negated)
			: base(source, property, value, negated)
		{
		}

		protected override bool IsPropertyTypeValid()
		{
			Check.Require(Property != null);

			if (Property.PropertyType.Equals(typeof(IComparable)))
				return true;
			return false;
		}

		public abstract bool IsTrue(IComparable valueA, IComparable valueB);

		public override bool IsTrue()
		{
			var valueA = GetPropertyValue() as IComparable;
			var valueB = Value as IComparable;
			if (valueB != null && valueB != null)
			{
				bool value = IsTrue(valueA, valueB);
				return Negated ? !value : value;
			}
			return false;
		}
	
	}


}
