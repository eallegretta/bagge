﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Bagge.Seti.BusinessEntities.Properties;

namespace Bagge.Seti.Security.Constraints
{
	public class LowerThanConstraint: InequalityConstraint
	{
		public LowerThanConstraint()
			: base()
		{
		}

		public LowerThanConstraint(object source, string propertyName, object value)
			: base(source, propertyName, value)
		{

		}
		public LowerThanConstraint(object source, PropertyInfo property, object value)
			: base(source, property, value)
		{
		}


		public override bool IsValid()
		{
			object value = GetPropertyValue();
			if (value != null)
				return ((IComparable)value).CompareTo((IComparable)Value) < 0;
			return false;
		}

		public override string ToString()
		{
			return Resources.Constraint_LowerThan;		
		}
	}
}