using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.DataAccess.Contracts
{
	public interface IRoleDao: IDao<Role, int>
	{
	}
}
