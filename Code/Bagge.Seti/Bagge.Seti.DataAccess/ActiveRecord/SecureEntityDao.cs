using System;
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.DataAccess.Contracts;
using Castle.ActiveRecord.Queries;
namespace Bagge.Seti.DataAccess.ActiveRecord
{
	public class SecureEntityDao : ISecureEntityDao
	{
		#region ISecureEntityDao Members

		public SecureEntity[] FindAll(int functionId)
		{
			string hql = "from SecureEntity se where se.Function.Id = ?";
			return new SimpleQuery<SecureEntity>(hql, functionId).Execute();
		}

		public SecureEntity Get(int functionId, string classFullQualifiedName)
		{
			string hql = "from SecureEntity se where se.Function.Id = ? and ClassFullQualifiedName = ?";
			var entities = new SimpleQuery<SecureEntity>(hql, functionId, classFullQualifiedName).Execute();
			if (entities.Length == 1)
				return entities[0];
			return null;
		}

		#endregion
	}
}
