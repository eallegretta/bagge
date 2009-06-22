using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.Security.BusinessEntities;
using System.Reflection;

namespace Bagge.Seti.BusinessLogic.Contracts
{
	public interface IFunctionManager: IAuditableManager<Function, int>
	{
		IList<Assembly> GetSecuredAssemblies();
		IList<Type> GetSecuredTypes(Assembly assembly);
		IList<MemberInfo> GetSecuredMembers(Type type);
	}
}
