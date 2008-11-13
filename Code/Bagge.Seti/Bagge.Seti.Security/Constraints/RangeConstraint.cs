using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Bagge.Seti.Security.Constraints
{
	public class RangeConstraint<T>: Constraint where T: IComparable<T>
	{


		public RangeConstraint(object source, string propertyName, T from, T to)
			: base(source, propertyName)
		{

			if(from == null)
				throw new ArgumentNullException("from");

			if (to == null)
				throw new ArgumentNullException("to");


			From = from;
			To = to;
		}

		public RangeConstraint(object source, PropertyInfo property, T from, T to)
			: base(source, property)
		{
			if (from == null)
				throw new ArgumentNullException("from");

			if (to == null)
				throw new ArgumentNullException("to");


			From = from;
			To = to;
		}

		public T From
		{
			get;
			private set;
		}
		public T To
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
				{
					T realValue = (T)value;
					return realValue.CompareTo(From) >= 0 && realValue.CompareTo(To) <= 0;
				}
				return false;
			}
		}
	}
}
