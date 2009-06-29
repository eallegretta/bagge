using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.Security.BusinessEntities;
using System.Reflection;
using Bagge.Seti.BusinessEntities.Security;

namespace Bagge.Seti.BusinessLogic.Contracts
{
	public interface IFunctionManager: IAuditableManager<Function, int>
	{
		bool UserHasAccessToFunction(IUser user, Function function);
	}
}
