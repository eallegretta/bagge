using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.BusinessLogic.Contracts
{
	public interface IFunctionManager: IManager<Function, int>
	{
		AccessibilityType[] ListMethodAccessibilities();
		AccessibilityType[] ListPropertyAccessibilities(); 
	}
}
