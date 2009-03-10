using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.Security.BusinessEntities;
using Castle.ActiveRecord;
using NHibernate.Expression;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.DataAccess.ActiveRecord
{
	public class AuditableGenericDao<T, PK>: GenericDao<T, PK> where T: PrimaryKeyDomainObject<T, PK>, IAuditable
	{
		private readonly ICriterion NotDeleted = Expression.Eq("Deleted", false);
		

		public override T[] FindAll()
		{
			return ActiveRecordMediator<T>.FindAll(NotDeleted);
		}

		public override T[] FindAllByProperty(string property, object value)
		{
			return ActiveRecordMediator<T>.FindAll(Expression.Eq(property, value), NotDeleted);
		}
		public override T[] FindAllByPropertyOrdered(string property, object value, string orderBy)
		{
			return FindAllByPropertyOrdered(property, value, orderBy, true);
		}
		public override T[] FindAllByPropertyOrdered(string property, object value, string orderBy, bool ascending)
		{
			return ActiveRecordMediator<T>.FindAll(new Order[] { new Order(orderBy, ascending) }, Expression.Eq(property, value), NotDeleted);
		}
		public override T[] FindAllOrdered(string orderBy)
		{
			return FindAllOrdered(orderBy, true);
		}

		public override T[] FindAllOrdered(string orderBy, bool ascending)
		{
			return ActiveRecordMediator<T>.FindAll(new Order[] { new Order(orderBy, ascending) }, NotDeleted);
		}

		public override T Get(PK id)
		{
			return ActiveRecordMediator<T>.FindFirst(Expression.Eq("Id", id), NotDeleted);
		}


		public override T[] SlicedFindAll(int pageIndex, int pageSize)
		{
			return ActiveRecordMediator<T>.SlicedFindAll(pageIndex, pageSize, NotDeleted);
		}

		public override T[] SlicedFindAllByProperty(int pageIndex, int pageSize, string property, object value)
		{
			return ActiveRecordMediator<T>.SlicedFindAll(pageIndex, pageSize, Expression.Eq(property, value), NotDeleted);
		}

		public override T[] SlicedFindAllByPropertyOrdered(int pageIndex, int pageSize, string property, object value, string orderBy)
		{
			return SlicedFindAllByPropertyOrdered(pageIndex, pageSize, property, value, orderBy, true);
		}

		public override T[] SlicedFindAllByPropertyOrdered(int pageIndex, int pageSize, string property, object value, string orderBy, bool ascending)
		{
			return ActiveRecordMediator<T>.SlicedFindAll(pageIndex, pageSize, new Order[] { new Order(orderBy, ascending) }, Expression.Eq(property, value), NotDeleted);
		}

		public override T[] SlicedFindAllOrdered(int pageIndex, int pageSize, string orderBy)
		{
			return SlicedFindAllOrdered(pageIndex, pageSize, orderBy, true);
		}

		public override T[] SlicedFindAllOrdered(int pageIndex, int pageSize, string orderBy, bool ascending)
		{
			return ActiveRecordMediator<T>.SlicedFindAll(pageIndex, pageSize, new Order[] { new Order(orderBy, ascending) }, NotDeleted);
		}

		public override int Count()
		{
			return ActiveRecordMediator<T>.Count(NotDeleted);
		}

		public override int CountByProperty(string property, object value)
		{
			return ActiveRecordMediator<T>.Count(Expression.Eq(property, value), NotDeleted);
		}

		public override void Delete(PK id)
		{
			T instance = Get(id);
			instance.Deleted = true;
			Update(instance);
		}
	}
}
