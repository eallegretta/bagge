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


		#region IEquatable<AccesibilityTypes> Members

		public bool Equals(AccessibilityTypes other)
		{
			return ((AccessibilityTypes)Id) == other;
		}


		public static AccessibilityTypes GetAccessibilityForProperty(Type type, string propertyName, IList<Function> functions)
		{
			var filteredFunctions = (from function in functions
									 where function.MemberType == 'P' &&
											function.MemberName == propertyName &&
											function.TargetType.Equals(type)
									 select function).ToArray();

			if (filteredFunctions.Length == 0)
				return AccessibilityTypes.Edit;


			bool canEdit = (from function in filteredFunctions
							where function.Accessibility.Equals(AccessibilityTypes.Edit)
							select function).Count() > 0;
			if (canEdit)
				return AccessibilityTypes.Edit;

			bool canView = (from function in filteredFunctions
							where function.Accessibility.Equals(AccessibilityTypes.View)
							select function).Count() > 0;

			if (canView)
				return AccessibilityTypes.View;

			return AccessibilityTypes.None;

		}

		public static AccessibilityTypes GetAccessibilityForMethod(Type type, string methodName, IList<Function> functions)
		{
			var filteredFunctions = (from function in functions
						 where function.MemberType == 'M' &&
								function.MemberName == methodName &&
								function.TargetType.Equals(type)
						 select function).ToArray();

			if (filteredFunctions.Length == 0)
				return AccessibilityTypes.Execute;
			
			bool canExecute = (from function in filteredFunctions
							where function.Accessibility.Equals(AccessibilityTypes.Edit)
							select function).Count() > 0;
			if (canExecute)
				return AccessibilityTypes.Execute;

			return AccessibilityTypes.None;
		}
		#endregion
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