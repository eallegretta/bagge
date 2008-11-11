using System;
using Bagge.Seti.BusinessEntities;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Scopes;

namespace Bagge.Seti.DataAccess.ActiveRecord
{
	public class GenericDao<T,PK>: IDao<T,PK> where T: PrimaryKeyDomainObject<T, PK>
	{
		#region IDao<T,PK> Members

		public T[] FindAll()
		{
			return ActiveRecordMediator<T>.FindAll();
		}

		public T Get(PK id)
		{
			return ActiveRecordMediator<T>.FindByPrimaryKey(id);
		}

		public PK Create(T instance)
		{
			ActiveRecordMediator<T>.Create(instance);
			return instance.Id;
		}

		public void Update(T instance)
		{
			try
			{
				ISessionScope scope = ThreadScopeAccessor.Instance.GetRegisteredScope();
				if (scope != null)
				{
					scope.Flush();
					scope.Dispose();
				}
				ActiveRecordMediator<T>.Update(instance);
			}
			catch (ScopeMachineryException)
			{
			}
		}

		public void Delete(PK id)
		{
			ActiveRecordMediator<T>.Delete(Get(id));	
		}

		#endregion
	}
}
