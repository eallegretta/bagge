using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.DataAccess.Contracts;
using Bagge.Seti.BusinessEntities.Security;
using Castle.ActiveRecord.Queries;
using Castle.ActiveRecord;
using NHibernate.Expression;

namespace Bagge.Seti.DataAccess.ActiveRecord
{
	public class SecurityExceptionDao: ISecurityExceptionDao
	{
		public SecurityException[] FindAll(int roleId, int functionId)
		{
			string hql = "from SecurityException e where e.Role.Id = ? and e.SecureEntity.Function.Id = ?";
			return new SimpleQuery<SecurityException>(hql, roleId, functionId).Execute();
		}

		public void DeleteAll(int roleId, int functionId)
		{
			var query = new ScalarQuery<long>(typeof(long), "delete from SecurityException e where e.Role.Id = ?  and e.SecureEntity.Function.Id = ?");
			query.Execute();
		}

		public void Save(SecurityException securityException)
		{
			if(securityException.Id > 0)
				SessionScopeUtils.FlushSessionScope();
			ActiveRecordMediator<SecurityException>.Save(securityException);
		}

		#region ISecurityExceptionDao Members


		public SecurityException Get(int id)
		{
			return ActiveRecordMediator<SecurityException>.FindByPrimaryKey(id, false);
		}

		public void Delete(int id)
		{
			ActiveRecordMediator<SecurityException>.Delete(Get(id));
		}

		#endregion
	}
}
