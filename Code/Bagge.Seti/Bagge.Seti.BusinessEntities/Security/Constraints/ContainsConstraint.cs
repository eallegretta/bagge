using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Bagge.Seti.BusinessEntities.Properties;
using Bagge.Seti.DesignByContract;

namespace Bagge.Seti.Security.Constraints
{
	public class ContainsConstraint : Constraint
	{
		public ContainsConstraint()
			: base()
		{
		}

		public ContainsConstraint(object source, string propertyName, object value)
			: base(source, propertyName, value)
		{

		}
		public ContainsConstraint(object source, PropertyInfo property, object value)
			: base(source, property, value)
		{
		}

		protected override bool IsPropertyTypeValid()
		{
			Check.Require(Property != null);

			if (Property.PropertyType.Equals(typeof(string)))
				return true;
			return false;
		}

		public override bool IsValid()
		{
			object value = GetPropertyValue();
			if (value != null)
				return value.ToString().Contains(Value.ToString());
			return false;
		}

		public override string ToString()
		{
			return Resources.Constraint_Contains;
		}
	}
}
