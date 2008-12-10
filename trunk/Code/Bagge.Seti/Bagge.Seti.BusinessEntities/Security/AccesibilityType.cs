﻿using Bagge.Seti.BusinessEntities;
using System;

namespace Bagge.Seti.Security.BusinessEntities
{
	public class AccesibilityType : PrimaryKeyWithNameDomainObject<AccesibilityType, byte>, IEquatable<AccesibilityTypes>
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