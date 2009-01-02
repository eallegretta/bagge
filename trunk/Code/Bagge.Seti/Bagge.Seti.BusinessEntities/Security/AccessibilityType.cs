using Bagge.Seti.BusinessEntities;
using System;
using Castle.ActiveRecord;

namespace Bagge.Seti.Security.BusinessEntities
{
	[ActiveRecord]
	public class AccessibilityType : PrimaryKeyWithNameDomainObject<AccessibilityType, byte>, IEquatable<AccesibilityTypes>
	{


		#region IEquatable<AccesibilityTypes> Members

		public bool Equals(AccesibilityTypes other)
		{
			return ((AccesibilityTypes)Id) == other;
		}

		#endregion
	}

	public enum AccesibilityTypeType: byte
	{
		Both = 0,
		Property = 1,
		Method = 2
	}

	public enum AccesibilityTypes
	{
		Execute = 4,
		View = 3,
		Edit = 2,
		None = 1
	}
}