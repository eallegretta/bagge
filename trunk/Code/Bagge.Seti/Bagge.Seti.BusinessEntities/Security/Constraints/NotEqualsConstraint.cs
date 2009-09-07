using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.Security.Constraints;
using System.Reflection;
using Bagge.Seti.BusinessEntities.Properties;

namespace Bagge.Seti.Security.Constraints
{
	public class NotEqualsConstraint : InequalityConstraint
	{
		public NotEqualsConstraint()
			: base()
		{
		}

		public NotEqualsConstraint(object source, string propertyName, object value, bool negated)
			: base(source, propertyName, value, negated)
		{

		}
		public NotEqualsConstraint(object source, PropertyInfo property, object value, bool negated)
			: base(source, property, value, negated)
		{
		}


		public override bool IsTrue(System.IComparable valueA, System.IComparable valueB)
		{
			return valueA.CompareTo(valueB) != 0;
		}

		public override string ToString()
		{
			return Resources.Constraint_NotEqual;
		}

		public override string Symbol
		{
			get { return Constraint.NotEqual; }
		}
	}
}
