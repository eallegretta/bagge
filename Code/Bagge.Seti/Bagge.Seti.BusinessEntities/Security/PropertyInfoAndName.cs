using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Bagge.Seti.BusinessEntities.Security
{
	public class PropertyInfoAndName
	{
		public PropertyInfoAndName(PropertyInfo property, string name)
		{
			Property = property;
			Name = name;
		}

		public PropertyInfo Property { get; private set; }
		public string Name { get; private set; }
	}
}
