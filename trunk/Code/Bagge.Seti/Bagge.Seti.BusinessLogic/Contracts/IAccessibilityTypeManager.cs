using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.BusinessEntities.Security;

namespace Bagge.Seti.BusinessLogic.Contracts
{
	public interface IAccessibilityTypeManager: IFindManager<AccessibilityType, byte>, IGetManager<AccessibilityType, byte>
	{
		AccessibilityType[] FindAllByType(AccessibilityTypeType type);

		AccessibilityTypes GetUserAccessibilityForProperty(IUser employee, Type targetObject, string propertyName);

		AccessibilityTypes GetUserAccessibilityForMethod(IUser employee, Type targetObject, string methodName);
	}
}
