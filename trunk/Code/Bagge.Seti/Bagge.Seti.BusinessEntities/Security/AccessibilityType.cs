using Bagge.Seti.BusinessEntities;
using System;
using Castle.ActiveRecord;
using System.Collections.Generic;
using System.Linq;

namespace Bagge.Seti.Security.BusinessEntities
{
	[ActiveRecord]
	[Serializable]
	public class AccessibilityType : PrimaryKeyWithNameDomainObject<AccessibilityType, byte>, IEquatable<AccessibilityTypes>
	{
		public bool Equals(AccessibilityTypes other)
		{
			return ((AccessibilityTypes)Id) == other;
		}
	}
		

	public enum AccessibilityTypeType: byte
	{
		Both = 0,
		Property = 1,
		Method = 2
	}

	public enum AccessibilityTypes
	{
		Execute = 4,
		View = 3,
		Edit = 2,
		None = 1
	}
}