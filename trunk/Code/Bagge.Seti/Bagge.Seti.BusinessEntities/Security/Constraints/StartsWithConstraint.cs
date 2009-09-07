using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities.Properties;
using System.Reflection;

namespace Bagge.Seti.Security.Constraints
{
	public class StartsWithConstraint : StringConstraint
	{
		public StartsWithConstraint()
			: base()
		{
		}

		public StartsWithConstraint(object source, string propertyName, object value, bool negated)
			: base(source, propertyName, value, negated)
		{

		}
		public StartsWithConstraint(object source, PropertyInfo property, object value, bool negated)
			: base(source, property, value, negated)
		{
		}

		public override bool IsTrue(string valueA, string valueB)
		{
			return valueA.StartsWith(valueB);
		}

		public override string ToString()
		{
			return Resources.Constraint_StartsWith;
		}

		public override string Symbol
		{
			get { return Constraint.StartsWith; }
		}
	}
}
