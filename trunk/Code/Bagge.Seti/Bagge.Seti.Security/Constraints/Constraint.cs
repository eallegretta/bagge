using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Bagge.Seti.Security
{
	public abstract class Constraint
	{
		public Constraint(object source, string propertyName)
		{
			if (source == null)
				throw new ArgumentNullException("source");

			if (propertyName == null)
				throw new ArgumentNullException("propertyName");

			Source = source;
			Property = source.GetType().GetProperty(propertyName);

			if (!IsPropertyTypeValid())
				throw new ArgumentException("Property Type Not Valid");
		}
		public Constraint(object source, PropertyInfo property)
		{
			if (source == null)
				throw new ArgumentNullException("source");

			if (property == null)
				throw new ArgumentNullException("property");

			Source = source;
			Property = property;

			if (!IsPropertyTypeValid())
				throw new ArgumentException("Property Type Not Valid");
		}

		protected virtual bool IsPropertyTypeValid()
		{
			return true;
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

		public abstract bool IsValid { get; }
	}
}
