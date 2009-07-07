using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bagge.Seti.Extensions
{
	public static class TypeExtensions
	{
		public static bool IsOfType(this Type type, params Type[] otherTypes)
		{
			if (type.In<Type>(otherTypes))
				return true;
			else
			{
				if (type.BaseType.Equals(typeof(Object)))
					return type.In<Type>(otherTypes);
				else
					return IsOfType(type.BaseType, otherTypes);
			}
		}
	}
}
