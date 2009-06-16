﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Bagge.Seti.Security.Constraints
{
	public class GreaterThanConstraint : InequalityConstraint
	{

		public GreaterThanConstraint(object source, string propertyName, object value)
			: base(source, propertyName, value)
		{

		}
		public GreaterThanConstraint(object source, PropertyInfo property, object value)
			: base(source, property, value)
		{
		}

		public override bool IsValid()
		{
			object value = GetPropertyValue();
			if (value != null)
				return ((IComparable)value).CompareTo((IComparable)Value) > 0;
			return false;
		}
	}
}
