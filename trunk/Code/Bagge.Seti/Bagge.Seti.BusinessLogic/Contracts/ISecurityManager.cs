using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities.Security;

namespace Bagge.Seti.BusinessLogic.Contracts
{
	public interface ISecurityManager
	{
		SecurityException[] FindAll(int roleId, int functionId);
		void Save(int roleId, int functionId, SecurityException[] exceptions);
	}
}
