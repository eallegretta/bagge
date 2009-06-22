using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;

namespace Bagge.Seti.BusinessEntities.Security
{
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
	public class SecurizableAttribute: Attribute
	{
		public SecurizableAttribute(string name)
		{
			Name = name;
		}

		public SecurizableAttribute(string nameResourceName, Type nameResourceType)
		{
			NameResourceName = nameResourceName;
			NameResourceType = nameResourceType;
		}


		public string Name { get; set; }
		public Type NameResourceType { get; set; }
		public string NameResourceName { get; set; }

		public SecurizableAttribute()
		{
			if (NameResourceType != null && !string.IsNullOrEmpty(NameResourceName))
			{
				ResourceManager rm = new ResourceManager(NameResourceType);
				Name = rm.GetString(NameResourceName);
			}
		}
	}
}
