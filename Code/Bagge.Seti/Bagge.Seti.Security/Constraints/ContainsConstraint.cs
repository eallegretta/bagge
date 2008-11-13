using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Bagge.Seti.Security.Constraints
{
	public class ContainsConstraint: Constraint
	{
		public ContainsConstraint(object source, string propertyName, string contains)
			: base(source, propertyName)
		{
			if (string.IsNullOrEmpty(contains))
				throw new ArgumentNullException("contains");

			Value = contains;
		}

		public ContainsConstraint(object source, PropertyInfo property, string contains)
			: base(source, property)
		{
			if (string.IsNullOrEmpty(contains))
				throw new ArgumentNullException("contains");

			Value = contains;
		}

		public string Value
		{
			get;
			private set;
		}

		protected override bool IsPropertyTypeValid()
		{
			if (Property.PropertyType.Equals(typeof(string)))
				return true;
			return false;
		}

		public override bool IsValid
		{
			get
			{
				object value = Property.GetValue(Source, null);
				if (value != null)
					return value.ToString().Contains(Value);
				return false;
			}
		}
	}
}
