using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Bagge.Seti.DesignByContract;
using Bagge.Seti.BusinessEntities.Properties;
using Bagge.Seti.Extensions;

namespace Bagge.Seti.Security.Constraints
{
	public abstract class Constraint
	{
		public const string Equal = "=";
		public const string NotEqual = "!=";
		public const string LowerThan = "<";
		public const string LowerEqualsThan = "<=";
		public const string GreaterThan = ">";
		public const string GreaterEqualsThan = ">=";
		public const string Contains = "In";
		public const string StartsWith = "S%";
		public const string EndsWith = "%S";


	

		public static Constraint[] AvailableConstraints = { 
				Parse(Equal),
				Parse(NotEqual),
				Parse(LowerThan),
				Parse(GreaterThan),
				Parse(Contains),
				Parse(StartsWith),
				Parse(EndsWith), 
				Parse(LowerEqualsThan),
				Parse(GreaterEqualsThan)
		};

		public static Constraint[] NumericConstraints = {
				Parse(Equal),
				Parse(NotEqual),
				Parse(LowerThan),
				Parse(LowerEqualsThan),
				Parse(GreaterThan),
				Parse(GreaterEqualsThan)
		};

		public static Constraint[] DateConstraints = {
				Parse(Equal),
				Parse(NotEqual),
				Parse(LowerThan),
				Parse(LowerEqualsThan),
				Parse(GreaterThan),
				Parse(GreaterEqualsThan)
		};

		public static Constraint[] StringConstraints = {
				Parse(Equal),
				Parse(Contains),
				Parse(StartsWith),
				Parse(EndsWith),
				Parse(NotEqual)
		};


		public static Constraint[] BooleanConstraints = {
				Parse(Equal),
				Parse(NotEqual)
		};


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
				case StartsWith:
					return Resources.Constraint_StartsWith;
				case EndsWith:
					return Resources.Constraint_EndsWith;
				case LowerEqualsThan:
					return Resources.Constraint_LowerEqualsThan;
				case GreaterEqualsThan:
					return Resources.Constraint_GreaterEqualsThan;
				default:
					return string.Empty;
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
				case StartsWith:
					return new StartsWithConstraint();
				case EndsWith:
					return new EndsWithConstraint();
				case LowerEqualsThan:
					return new LowerEqualsThanConstraint();
				case GreaterEqualsThan:
					return new GreaterEqualsThanConstraint();
				default:
					throw new ArgumentOutOfRangeException("constraint");
			}
		}

		public Constraint Parse(string constraint, object source, string propertyName, object value, bool negated)
		{
			switch (constraint)
			{
				case Equal:
					return new EqualsConstraint(source, propertyName, value, negated);
				case NotEqual:
					return new NotEqualsConstraint(source, propertyName, value, negated);
				case LowerThan:
					return new LowerThanConstraint(source, propertyName, value, negated);
				case LowerEqualsThan:
					return new LowerEqualsThanConstraint(source, propertyName, value, negated);
				case GreaterThan:
					return new GreaterThanConstraint(source, propertyName, value, negated);
				case GreaterEqualsThan:
					return new GreaterEqualsThanConstraint(source, propertyName, value, negated);
				case Contains:
					return new ContainsConstraint(source, propertyName, value, negated);
				case StartsWith:
					return new StartsWithConstraint(source, propertyName, value, negated);
				case EndsWith:
					return new EndsWithConstraint(source, propertyName, value, negated);
				default:
					throw new ArgumentOutOfRangeException("constraint");
			}
		}

		public static Constraint[] GetConstraintsForType(Type type)
		{
			if (type.IsOfType(true, typeof(DateTime), typeof(TimeSpan)))
				return DateConstraints;
			else if (type.IsOfType(true,
				typeof(byte), typeof(sbyte), typeof(short), typeof(ushort),
				typeof(int), typeof(uint), typeof(long), typeof(ulong),
				typeof(float), typeof(double), typeof(decimal)))
				return NumericConstraints;
			else if (type.IsOfType(true, typeof(string), typeof(char)))
				return StringConstraints;
			else if (type.IsOfType(true, typeof(bool)))
				return BooleanConstraints;
			else
				return StringConstraints;
		}

		public Constraint()
		{
		}

		public bool Negated { get; set; }

		public abstract string Symbol { get; }

		public Constraint(object source, string propertyName, object value, bool negated)
		{
			if (source == null)
				throw new ArgumentNullException("source");

			if (propertyName == null)
				throw new ArgumentNullException("propertyName");

			Source = source;
			Property = source.GetType().GetProperty(propertyName);
			Value = value;
			Negated = negated;

			if (!IsPropertyTypeValid())
				throw new ArgumentException("Property Type Not Valid");
		}
		public Constraint(object source, PropertyInfo property, object value, bool negated)
		{
			if (source == null)
				throw new ArgumentNullException("source");

			if (property == null)
				throw new ArgumentNullException("property");

			Source = source;
			Property = property;
			Value = value;
			Negated = negated;

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
