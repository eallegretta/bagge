using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.DataAccess.Contracts;

namespace Bagge.Seti.DataAccess.ActiveRecord
{
	public class RoleDao: GenericDao<Role, int>, IRoleDao
	{
	}
}
