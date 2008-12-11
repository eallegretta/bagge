﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.DataAccess.Contracts;

namespace Bagge.Seti.BusinessLogic
{
	public class FunctionManager: GenericManager<Function, int>, IFunctionManager
	{
		private IAccessibilityDao _accessibilityDao;

		public FunctionManager(IFunctionDao dao, IAccessibilityDao accessibilityDao)
			: base(dao)
		{
			_accessibilityDao = accessibilityDao;
		}

		#region IFunctionManager Members

		public AccessibilityType[] ListMethodAccessibilities()
		{
			throw new NotImplementedException();
		}

		public AccessibilityType[] ListPropertyAccessibilities()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
