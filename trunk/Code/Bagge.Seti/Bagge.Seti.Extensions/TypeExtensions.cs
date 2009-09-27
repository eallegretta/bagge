using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bagge.Seti.Extensions
{
	public static class TypeExtensions
	{

		public static bool IsNullable(this Type theType)
		{
			return (theType.IsGenericType && theType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)));
		}

		public static bool IsOfType(this Type type, params Type[] otherTypes)
		{
			return IsOfType(type, false, otherTypes);
		}

		public static bool IsOfType(this Type type, bool includeNullables, params Type[] otherTypes)
		{
			if (includeNullables && type.IsNullable())
				type = Nullable.GetUnderlyingType(type);

			foreach (var otherType in otherTypes)
			{
				if (type.IsAssignableFrom(otherType))
					return true;
			}

			return false;
		}
	}
}
