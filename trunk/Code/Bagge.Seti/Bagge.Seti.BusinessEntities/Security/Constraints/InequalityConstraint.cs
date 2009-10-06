using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Bagge.Seti.DesignByContract;
using Bagge.Seti.Extensions;

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

			Type type;

			if (Property.PropertyType.IsNullable())
				type = Nullable.GetUnderlyingType(Property.PropertyType);
			else
				type = Property.PropertyType;

			if (type.IsOfType(typeof(IComparable)))
				return true;

			return false;
		}

		protected abstract bool IsTrue(IComparable valueA, IComparable valueB);


		private IComparable GetIComparable(object value) 
		{
			if (value == null)
				return null;

			if (value.GetType().IsNullable())
			{
				if (!(bool)value.GetPropertyValue("HasValue"))
					return null;

				return value.GetPropertyValue("Value") as IComparable;
			}
			else
				return value as IComparable;
		}

		public override bool IsTrue()
		{
			var valueA = GetIComparable(GetPropertyValue());
			var valueB = GetIComparable(Value);

			if (valueA != null && valueB != null)
			{
				bool value = IsTrue(valueA, valueB);
				return Negated ? !value : value;
			}
			return false;
		}
	
	}


}
