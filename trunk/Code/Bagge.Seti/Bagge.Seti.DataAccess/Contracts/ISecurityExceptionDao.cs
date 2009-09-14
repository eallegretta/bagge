using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities.Security;

namespace Bagge.Seti.DataAccess.Contracts
{
	public interface ISecurityExceptionDao
	{
		void DeleteAll(int roleId, int functionId);
		SecurityException[] FindAll(int roleId, int functionId);
		SecurityException[] FindAll(int[] roleIds, int functionId);
		void Save(SecurityException securityException);
		SecurityException Get(int id);
		void Delete(int id);
	}
}
