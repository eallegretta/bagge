using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities.Properties;
using System.Reflection;

namespace Bagge.Seti.Security.Constraints
{
	public class LowerEqualsThanConstraint : InequalityConstraint
	{
		public LowerEqualsThanConstraint()
			: base()
		{
		}

		public LowerEqualsThanConstraint(object source, string propertyName, object value, bool negated)
			: base(source, propertyName, value, negated)
		{

		}
		public LowerEqualsThanConstraint(object source, PropertyInfo property, object value, bool negated)
			: base(source, property, value, negated)
		{
		}


		public override bool IsTrue(IComparable valueA, IComparable valueB)
		{
			return valueA.CompareTo(valueB) <= 0;
		}

		public override string ToString()
		{
			return Resources.Constraint_LowerEqualsThan;
		}

		public override string Symbol
		{
			get { return Constraint.LowerEqualsThan; }
		}
	}
}
