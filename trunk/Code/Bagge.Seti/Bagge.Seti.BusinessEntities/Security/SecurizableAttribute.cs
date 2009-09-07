using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Common;

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

		private string _name = null;

		public string Name {
			get
			{
				if (_name == null && NameResourceType != null && !string.IsNullOrEmpty(NameResourceName))
					SetNameFromResource();
				return _name;
			}
			set { _name = value; }
		}

		private void SetNameFromResource()
		{
			Name = ResourceStringLoader.LoadString(NameResourceType.FullName, NameResourceName, NameResourceType.Assembly);
		}

		public Type NameResourceType { get; set; }
		public string NameResourceName { get; set; }


		public static IList<PropertyInfoAndName> GetSecurizableProperties(Type type)
		{
			var properties = new List<PropertyInfoAndName>();

			if (type == null)
				return properties;
			
			foreach (var property in type.GetProperties())
			{
				string name = GetName(property);
				if (!string.IsNullOrEmpty(name))
				{
					properties.Add(new PropertyInfoAndName(property, name));
				}
			}
			return properties.OrderBy(x => x.Name).ToList();
		}

	
		public static string GetName(object target)
		{
			if (target == null)
				return null;

			SecurizableAttribute attr = null;
			if (target is Assembly)
				attr = (SecurizableAttribute)SecurizableAttribute.GetCustomAttribute(target as Assembly, typeof(SecurizableAttribute), true);
			else if (target is MemberInfo)
				attr = (SecurizableAttribute)SecurizableAttribute.GetCustomAttribute(target as MemberInfo, typeof(SecurizableAttribute), true);
			else if (target is Type)
			{
				Type type = target as Type;
				if (type.IsDefined(typeof(SecurizableAttribute), true))
					attr = (SecurizableAttribute)type.GetCustomAttributes(typeof(SecurizableAttribute), true)[0];
			}
			else
			{
				Type type = target.GetType();
				if (type.IsDefined(typeof(SecurizableAttribute), true))
					attr = (SecurizableAttribute)type.GetCustomAttributes(typeof(SecurizableAttribute), true)[0];
			}

			if (attr != null)
				return attr.Name;

			return null;
		}


	}
}
