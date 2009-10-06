using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Bagge.Seti.BusinessEntities.Properties;

namespace Bagge.Seti.Security.Constraints
{
	public class GreaterThanConstraint : InequalityConstraint
	{
		public GreaterThanConstraint()
			: base()
		{
		}

		public GreaterThanConstraint(object source, string propertyName, object value, bool negated)
			: base(source, propertyName, value, negated)
		{

		}
		public GreaterThanConstraint(object source, PropertyInfo property, object value, bool negated)
			: base(source, property, value, negated)
		{
		}

		protected override bool IsTrue(System.IComparable valueA, System.IComparable valueB)
		{
			return valueA.CompareTo(valueB) > 0;
		}

		public override string ToString()
		{
			return Resources.Constraint_GreaterThan;
		}

		public override string Symbol
		{
			get { return Constraint.GreaterThan; }
		}
	}
}
