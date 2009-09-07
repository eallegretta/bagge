using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities.Security;

namespace Bagge.Seti.BusinessLogic.Contracts
{
	public interface ISecurityManager
	{
		SecurityException GetSecurityException(int id);
		SecureEntity GetSecureEntity(int functionId, string classFullQualifiedName);
		SecureEntity[] FindAllSecureEntities(int functionId);
		SecurityException[] FindAllSecurityExceptions(int roleId, int functionId);
		//void Save(int roleId, int functionId, SecurityException[] exceptions);
		void Save(SecurityException exception);
		void Delete(int id);
	}
}
