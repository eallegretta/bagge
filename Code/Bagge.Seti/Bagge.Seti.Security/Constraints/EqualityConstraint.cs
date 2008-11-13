using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Bagge.Seti.Security.Constraints
{
	public class EqualityConstraint<T>: Constraint
	{


		public EqualityConstraint(object source, string propertyName, T equalsTo)
			: base(source, propertyName)
		{

			if(equalsTo == null)
				throw new ArgumentNullException("contains");

			Value = equalsTo;
		}

		public EqualityConstraint(object source, PropertyInfo property, T equalsTo)
			: base(source, property)
		{
			if (equalsTo == null)
				throw new ArgumentNullException("contains");

			Value = equalsTo;
		}

		public T Value
		{
			get;
			private set;
		}

		protected override bool IsPropertyTypeValid()
		{
			if (Property.PropertyType.Equals(typeof(T)))
				return true;
			return false;
		}

		public override bool IsValid
		{
			get
			{
				object value = Property.GetValue(Source, null);
				if (value != null)
					return ((T)value).Equals(Value);
				return false;
			}
		}
	}
}
