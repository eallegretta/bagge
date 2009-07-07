using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.DataAccess.Contracts;
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.DataAccess.ActiveRecord
{
	public class AccessibilityDao: IAccessibilityDao
	{
		public AccessibilityType[] FindAllByType(AccessibilityTypeType type)
		{
			return FindAllByProperty("Id", (byte)type, null, null);
		}

		public AccessibilityType Get(byte id)
		{
			return new GenericDao<AccessibilityType, byte>().Get(id);
		}

		public AccessibilityType[] FindAll(string orderBy, bool? ascending)
		{
			return new GenericDao<AccessibilityType, byte>().FindAll(orderBy, ascending);
		}

		public AccessibilityType[] FindAllByProperty(string property, object value, string orderBy, bool? ascending)
		{
			return new GenericDao<AccessibilityType, byte>().FindAllByProperty(property, value, orderBy, ascending);
		}

		public AccessibilityType[] FindAllByProperties(IList<Bagge.Seti.BusinessEntities.FilterPropertyValue> filter, string orderBy, bool? ascending)
		{
			return new GenericDao<AccessibilityType, byte>().FindAllByProperties(filter, orderBy, ascending);
		}

	}
}
