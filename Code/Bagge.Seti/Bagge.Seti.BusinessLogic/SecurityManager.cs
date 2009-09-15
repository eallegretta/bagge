using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.DataAccess.Contracts;
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.Security.Constraints;
using System.Reflection;

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

		private int[] GetUserRoleIds(IUser user)
		{
			return (from role in user.Roles
					select role.Id).ToArray();
		}

		public SecurityException[] FindAllSecurityExceptions(IUser user, int functionId)
		{
			return user.SecurityExceptions.Where(se => se.SecureEntity.Function.Id == functionId).ToArray();

			//return _securityExceptionDao.FindAll(GetUserRoleIds(user), functionId);
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

		public bool UserHasAccessToInstance(object instance, SecurityException[] exceptions)
		{
			if (exceptions == null)
				return true;

			string classFullQualifiedName = Assembly.CreateQualifiedName(
				instance.GetType().Assembly.GetName().Name, instance.GetType().FullName);

			var query = from se in exceptions
						where se.SecureEntity.ClassFullQualifiedName == classFullQualifiedName
						select se;

			foreach (var securityException in query.ToArray())
			{
				var constraint = Constraint.Parse(securityException.ConstraintType,
					instance, securityException.PropertyName, securityException.Value, false);

				if (constraint.IsTrue())
					return false;
			}

			return true;
		}

		#endregion
	}
}
