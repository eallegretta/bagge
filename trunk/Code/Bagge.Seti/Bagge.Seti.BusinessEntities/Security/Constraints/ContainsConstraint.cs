using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Bagge.Seti.BusinessEntities.Properties;
using Bagge.Seti.DesignByContract;
using System.Collections;

namespace Bagge.Seti.Security.Constraints
{
	public class ContainsConstraint : Constraint
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

		public override string ToString()
		{
			return Resources.Constraint_Contains;
		}

		public override string Symbol
		{
			get { return Constraint.Contains; }
		}

		protected override bool IsPropertyTypeValid()
		{
            //if(Property.PropertyType.In(typeof(IEnumerable), typeof(string)))
            //    return true;
            //return false;
            return true;
		}

		public override bool IsTrue()
		{
			if (Property.PropertyType.Equals(typeof(IEnumerable)))
			{
				string valueB = Value as string;
				if (string.IsNullOrEmpty(valueB))
					return false;
				var en = ((IEnumerable)GetPropertyValue()).GetEnumerator();
				bool contains = false;
				
				while (en.MoveNext() && !contains)
				{
					if(en.Current != null)
					{
						string valueA = en.Current.ToString().ToLowerInvariant();
						if (valueA.Contains(valueB.ToLowerInvariant()))
							contains = true;
					}
				}
				return Negated ? !contains : contains;
			}
			else
			{
                object propertyValueA = GetPropertyValue();
                string valueA = propertyValueA != null ? propertyValueA.ToString() : null;
                string valueB = Value as string;
                if (!string.IsNullOrEmpty(valueA) && !string.IsNullOrEmpty(valueB))
                {
                    bool retValue = valueA.ToLowerInvariant().Contains(valueB.ToLowerInvariant());
                    return Negated ? !retValue : retValue;
                }
                return false;
			}
		}
	}
}
