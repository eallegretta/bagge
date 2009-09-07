using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Bagge.Seti.BusinessEntities.Properties;
using Bagge.Seti.DesignByContract;

namespace Bagge.Seti.Security.Constraints
{
	public class ContainsConstraint : StringConstraint
	{
		public ContainsConstraint()
			: base()
		{
		}

		public ContainsConstraint(object source, string propertyName, object value, bool negated)
			: base(source, propertyName, value, negated)
		{

		}
		public ContainsConstraint(object source, PropertyInfo property, object value, bool negated)
			: base(source, property, value, negated)
		{
		}

		public override bool IsTrue(string valueA, string valueB)
		{
			return valueA.Contains(valueB);
		}

		public override string ToString()
		{
			return Resources.Constraint_Contains;
		}

		public override string Symbol
		{
			get { return Constraint.Contains; }
		}
	}
}
