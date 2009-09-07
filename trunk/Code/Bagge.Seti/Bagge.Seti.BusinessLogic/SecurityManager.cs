using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.DataAccess.Contracts;
using Bagge.Seti.BusinessEntities.Security;

namespace Bagge.Seti.BusinessLogic
{
	public class SecurityManager: ISecurityManager 
	{
		ISecurityExceptionDao _securityExceptionDao;
		ISecureEntityDao _secureEntityDao;

		public SecurityManager(ISecurityExceptionDao securityExceptionDao, ISecureEntityDao secureEntityDao)
		{
			_secureEntityDao = secureEntityDao;
			_securityExceptionDao = securityExceptionDao;
		}

		#region ISecurityManager Members

		public SecurityException GetSecurityException(int id)
		{
			return _securityExceptionDao.Get(id);
		}

		public SecureEntity GetSecureEntity(int functionId, string classFullQualifiedName)
		{
			return _secureEntityDao.Get(functionId, classFullQualifiedName);
		}

		public SecureEntity[] FindAllSecureEntities(int functionId)
		{
			return _secureEntityDao.FindAll(functionId);
		}

		public SecurityException[] FindAllSecurityExceptions(int roleId, int functionId)
		{
			return _securityExceptionDao.FindAll(roleId, functionId);
		}

		public void Save(SecurityException securityException)
		{
			_securityExceptionDao.Save(securityException);
		}

		public void Save(int roleId, int functionId, SecurityException[] exceptions)
		{
			_securityExceptionDao.DeleteAll(roleId, functionId);
			foreach (var exception in exceptions)
				_securityExceptionDao.Save(exception);
		}

		public void Delete(int securityExceptionId)
		{
			_securityExceptionDao.Delete(securityExceptionId);
		}

		#endregion
	}
}
