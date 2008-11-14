using System;
using Bagge.Seti.BusinessEntities;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Scopes;
using NHibernate.Expression;

namespace Bagge.Seti.DataAccess.ActiveRecord
{
	public class GenericDao<T,PK>: IDao<T,PK> where T: PrimaryKeyDomainObject<T, PK>
	{
		#region IDao<T,PK> Members

		public T[] FindAll()
		{
			return ActiveRecordMediator<T>.FindAll();
		}

		public T[] FindAllOrdered(string orderBy)
		{
			return ActiveRecordMediator<T>.FindAll(new Order[] { new Order(orderBy, true) });
		}

		public T[] FindAllOrdered(string orderBy, bool ascending)
		{
			return ActiveRecordMediator<T>.FindAll(new Order[] { new Order(orderBy, ascending) });
		}


		public virtual T[] FindAllByProperty(string property, object value)
		{
			return ActiveRecordMediator<T>.FindAll(Expression.Eq(property, value));
		}
		public virtual T[] FindAllByPropertyOrdered(string property, object value, string orderBy)
		{
			return FindAllByPropertyOrdered(property, value, orderBy, true);
		}

		public virtual T[] FindAllByPropertyOrdered(string property, object value, string orderBy, bool ascending)
		{
			return ActiveRecordMediator<T>.FindAll(new Order[] { new Order(orderBy, ascending) },
				Expression.Eq(property, value));
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

		public virtual T[] SlicedFindAll(int pageIndex, int pageSize)
		{
			return ActiveRecordMediator<T>.SlicedFindAll(pageIndex, pageSize,
				new Order[] { });
		}

		public virtual T[] SlicedFindAllOrdered(int pageIndex, int pageSize, string orderBy)
		{
			return SlicedFindAllOrdered(pageIndex, pageSize, orderBy, true);
		}
		public virtual T[] SlicedFindAllOrdered(int pageIndex, int pageSize, string orderBy, bool ascending)
		{
			return ActiveRecordMediator<T>.SlicedFindAll(pageIndex, pageSize,
				new Order[] { new Order(orderBy, ascending) });
		}


		public virtual T[] SlicedFindAllByProperty(int pageIndex, int pageSize, string property, object value)
		{
			return ActiveRecordMediator<T>.SlicedFindAll(pageIndex, pageSize,
				Expression.Eq(property, value));
		}

		public virtual T[] SlicedFindAllByPropertyOrdered(int pageIndex, int pageSize, string property, object value, string orderBy)
		{
			return SlicedFindAllByPropertyOrdered(pageIndex, pageSize, property, value, orderBy, true);
		}
		public virtual T[] SlicedFindAllByPropertyOrdered(int pageIndex, int pageSize, string property, object value, string orderBy, bool ascending)
		{
			return ActiveRecordMediator<T>.SlicedFindAll(pageIndex, pageSize,
				new Order[] { new Order(orderBy, ascending) },
				Expression.Eq(property, value));
		}

		public virtual int Count()
		{
			return ActiveRecordMediator<T>.Count();
		}

		public virtual int CountByProperty(string property, object value)
		{
			return ActiveRecordMediator<T>.Count(Expression.Eq(property, value));
		}

		#endregion
	}
}
