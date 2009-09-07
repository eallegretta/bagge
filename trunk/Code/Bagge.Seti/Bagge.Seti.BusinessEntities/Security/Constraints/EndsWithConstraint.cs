using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities.Properties;
using System.Reflection;

namespace Bagge.Seti.Security.Constraints
{
	public class EndsWithConstraint : StringConstraint
	{
		public EndsWithConstraint()
			: base()
		{
		}

		public EndsWithConstraint(object source, string propertyName, object value, bool negated)
			: base(source, propertyName, value, negated)
		{

		}
		public EndsWithConstraint(object source, PropertyInfo property, object value, bool negated)
			: base(source, property, value, negated)
		{
		}

		public override bool IsTrue(string valueA, string valueB)
		{
			return valueA.EndsWith(valueB);
		}

		public override string ToString()
		{
			return Resources.Constraint_EndsWith;
		}

		public override string Symbol
		{
			get { return Constraint.EndsWith; }
		}
	}
}
