using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities.Properties;
using System.Reflection;

namespace Bagge.Seti.Security.Constraints
{
	public class GreaterEqualsThanConstraint : InequalityConstraint
	{
		public GreaterEqualsThanConstraint()
			: base()
		{
		}

		public GreaterEqualsThanConstraint(object source, string propertyName, object value, bool negated)
			: base(source, propertyName, value, negated)
		{

		}
		public GreaterEqualsThanConstraint(object source, PropertyInfo property, object value, bool negated)
			: base(source, property, value, negated)
		{
		}

		protected override bool IsTrue(System.IComparable valueA, System.IComparable valueB)
		{
			return valueA.CompareTo(valueB) >= 0;
		}

		public override string ToString()
		{
			return Resources.Constraint_GreaterEqualsThan;
		}

		public override string Symbol
		{
			get { return Constraint.GreaterEqualsThan; }
		}
	}
}
