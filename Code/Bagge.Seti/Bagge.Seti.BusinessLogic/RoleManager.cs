using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.DataAccess.Contracts;

namespace Bagge.Seti.BusinessLogic
{
	public class RoleManager: GenericManager<Role, int>, IRoleManager
	{
		public RoleManager(IRoleDao dao)
			: base(dao)
		{
		}

	}
}
