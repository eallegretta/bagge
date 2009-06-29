using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Bagge.Seti.BusinessEntities.Security.Constraints;

namespace Bagge.Seti.Security.Constraints
{
	public abstract class Constraint
	{
		public Constraint Parse(string constraint, object source, string propertyName, object value)
		{
			switch (constraint)
			{
				case "==":
					return new EqualsConstraint(source, propertyName, value);
				case "!=":
					return new NotEqualsConstraint(source, propertyName, value);
				case "<":
					return new LowerThanConstraint(source, propertyName, value);
				case ">":
					return new GreaterThanConstraint(source, propertyName, value);
				default:
					throw new ArgumentOutOfRangeException("constraint");
			}
		}

		public Constraint(object source, string propertyName, object value)
		{
			if (source == null)
				throw new ArgumentNullException("source");

			if (propertyName == null)
				throw new ArgumentNullException("propertyName");

			Source = source;
			Property = source.GetType().GetProperty(propertyName);
			Value = value;

			if (!IsPropertyTypeValid())
				throw new ArgumentException("Property Type Not Valid");
		}
		public Constraint(object source, PropertyInfo property, object value)
		{
			if (source == null)
				throw new ArgumentNullException("source");

			if (property == null)
				throw new ArgumentNullException("property");

			Source = source;
			Property = property;
			Value = value;

			if (!IsPropertyTypeValid())
				throw new ArgumentException("Property Type Not Valid");
		}

		protected virtual bool IsPropertyTypeValid()
		{
			if (Property.PropertyType.Equals(Value.GetType()))
				return true;
			return false;
		}

		public object Source
		{
			get;
			private set;
		}

		public PropertyInfo Property
		{
			get;
			private set;
		}

		protected object GetPropertyValue()
		{
			if (Source != null)
				return Property.GetValue(Source, null);
			return null;
		}

		public virtual object Value
		{
			get;
			private set;
		}

		public abstract bool IsValid();
	}
}
