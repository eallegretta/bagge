using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Bagge.Seti.BusinessEntities.Security.Constraints;
using Bagge.Seti.DesignByContract;
using Bagge.Seti.BusinessEntities.Properties;

namespace Bagge.Seti.Security.Constraints
{
	public abstract class Constraint
	{
		public const string Equal = "=";
		public const string NotEqual = "!=";
		public const string LowerThan = "<";
		public const string GreaterThan = ">";
		public const string Contains = "In";


		public static Constraint[] AvailableConstraints = { 
				Parse(Equal),
				Parse(NotEqual),
				Parse(LowerThan),
				Parse(GreaterThan),
				Parse(Contains) };


		public static string GetConstraintName(string constraint)
		{
			switch (constraint)
			{
				case Equal:
					return Resources.Constraint_Equal;
				case NotEqual:
					return Resources.Constraint_NotEqual;
				case LowerThan:
					return Resources.Constraint_LowerThan;
				case GreaterThan:
					return Resources.Constraint_GreaterThan;
				case Contains:
					return Resources.Constraint_Contains;
				default:
					throw new ArgumentOutOfRangeException("constraint");
			}
		}

		public static Constraint Parse(string constraint)
		{
			switch (constraint)
			{
				case Equal:
					return new EqualsConstraint();
				case NotEqual:
					return new NotEqualsConstraint();
				case LowerThan:
					return new LowerThanConstraint();
				case GreaterThan:
					return new GreaterThanConstraint();
				case Contains:
					return new ContainsConstraint();
				default:
					throw new ArgumentOutOfRangeException("constraint");
			}
		}

		public Constraint Parse(string constraint, object source, string propertyName, object value)
		{
			switch (constraint)
			{
				case Equal:
					return new EqualsConstraint(source, propertyName, value);
				case NotEqual:
					return new NotEqualsConstraint(source, propertyName, value);
				case LowerThan:
					return new LowerThanConstraint(source, propertyName, value);
				case GreaterThan:
					return new GreaterThanConstraint(source, propertyName, value);
				case Contains:
					return new ContainsConstraint(source, propertyName, value);
				default:
					throw new ArgumentOutOfRangeException("constraint");
			}
		}

		public Constraint()
		{
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
			Check.Require(Property != null);

			if (Property.PropertyType.Equals(Value.GetType()))
				return true;

			return false;
		}

		public object Source
		{
			get;
			set;
		}

		public PropertyInfo Property
		{
			get;
			set;
		}

		protected object GetPropertyValue()
		{
			Check.Require(Source != null);

			return Property.GetValue(Source, null);
		}

		public virtual object Value
		{
			get;
			set;
		}

		public abstract bool IsValid();
	}
}
