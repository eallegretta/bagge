using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.DataAccess.Contracts;
using System.Reflection;
using System.IO;
using Bagge.Seti.BusinessEntities.Security;

namespace Bagge.Seti.BusinessLogic
{
	public class FunctionManager : AuditableGenericManager<Function, int>, IFunctionManager
	{
		private IAccessibilityDao _accessibilityDao;

		public FunctionManager(IFunctionDao dao, IAccessibilityDao accessibilityDao)
			: base(dao)
		{
			_accessibilityDao = accessibilityDao;
		}


		public IList<System.Reflection.Assembly> GetSecuredAssemblies()
		{
			List<Assembly> securedAssemblies = new List<Assembly>();
			foreach (string file in Directory.GetFiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "*.dll"))
			{
				var assembly = Assembly.LoadFile(file);
				if (assembly.IsDefined(typeof(SecurizableAttribute), false))
					securedAssemblies.Add(assembly);
			}

			return securedAssemblies;
		}

		public IList<Type> GetSecuredTypes(Assembly assembly)
		{
			var securedTypes = from type in assembly.GetTypes()
							   where type.IsDefined(typeof(SecurizableAttribute), true)
							   select type;

			return securedTypes.ToList();
		}

		public IList<MemberInfo> GetSecuredMembers(Type type)
		{
			var securedMembers = from member in type.GetMembers()
								 where member.IsDefined(typeof(SecurizableAttribute), true)
								 select member;

			return securedMembers.ToList();
		}
	}
}
