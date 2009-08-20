using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.DataAccess.Contracts;

namespace Bagge.Seti.BusinessLogic
{
	public class SecurityManager: ISecurityManager 
	{
		ISecurityDao _dao;

		public SecurityManager(ISecurityDao dao)
		{
			_dao = dao;
		}

		#region ISecurityManager Members

		public Bagge.Seti.BusinessEntities.Security.SecurityException[] FindAll(int roleId, int functionId)
		{
			throw new NotImplementedException();
		}

		public void Save(int roleId, int functionId, Bagge.Seti.BusinessEntities.Security.SecurityException[] exceptions)
		{

			throw new NotImplementedException();
		}

		#endregion
	}
}
