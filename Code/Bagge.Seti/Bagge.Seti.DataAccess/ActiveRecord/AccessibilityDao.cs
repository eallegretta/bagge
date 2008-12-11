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
		#region IAccessibilityDao Members

		public AccessibilityType[] FindByType(AccesibilityTypeType type)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region IGetDao<AccessibilityType,int> Members

		public AccessibilityType Get(byte id)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
