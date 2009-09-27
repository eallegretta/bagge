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

		public EqualsConstraint(object source, string propertyName, object value, bool negated)
			: base(source, propertyName, value, negated)
		{

		}
		public EqualsConstraint(object source, PropertyInfo property, object value, bool negated)
			: base(source, property, value, negated)
		{
		}


		public override bool IsTrue()
		{
			object value = GetPropertyValue();

			if (value == null || Value == null)
				return false;

			if (value.GetType() != Value.GetType())
				return value.ToString().Equals(Value.ToString(), StringComparison.InvariantCultureIgnoreCase);
			else			
				return value.Equals(Value);
		}

		public override string ToString()
		{
			return Resources.Constraint_Equal;
		}

		public override string Symbol
		{
			get { return Constraint.Equal; }
		}
	}
}
