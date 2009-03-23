using System;
using Bagge.Seti.BusinessEntities;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Scopes;
using NHibernate.Expression;
using Bagge.Seti.DataAccess.Contracts;
using System.Collections.Generic;

namespace Bagge.Seti.DataAccess.ActiveRecord
{
	public class GenericDao<T,PK>: IDao<T,PK> where T: PrimaryKeyDomainObject<T, PK>
	{
		#region IDao<T,PK> Members

		public virtual T[] FindAll()
		{
			return ActiveRecordMediator<T>.FindAll();
		}

		public virtual T[] FindAllOrdered(string orderBy)
		{
			return ActiveRecordMediator<T>.FindAll(new Order[] { new Order(orderBy, true) });
		}

		public virtual T[] FindAllOrdered(string orderBy, bool ascending)
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

		public virtual T Get(PK id)
		{
			try
			{
				return ActiveRecordMediator<T>.FindByPrimaryKey(id);
			}
			catch (NotFoundException)
			{
				throw new BusinessEntities.Exceptions.ObjectNotFoundException();
			}
		}

		public virtual PK Create(T instance)
		{
			ActiveRecordMediator<T>.Create(instance);
			return instance.Id;
		}

		public virtual void Update(T instance)
		{
			/*try
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
			}*/
			ActiveRecordMediator<T>.Update(instance);
		}

		public virtual void Delete(PK id)
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

		#region IFindDao<T,PK> Members

		protected ICriterion[] BuildCriteriaFromFilters(IList<FilterPropertyValue> filters)
		{
			List<ICriterion> list = new List<ICriterion>(filters.Count);
			foreach (FilterPropertyValue filter in filters)
			{
				switch (filter.Type)
				{
					case FilterPropertyValueType.Equals:
						list.Add(Expression.Eq(filter.Property, filter.Value));
						break;
					case FilterPropertyValueType.NotEquals:
						list.Add(Expression.Not(Expression.Eq(filter.Property, filter.Value)));
						break;
					case FilterPropertyValueType.Like:
						if(!string.IsNullOrEmpty(filter.Value.ToString()))
							list.Add(Expression.Like(filter.Property, filter.Value + "%"));
						break;
					case FilterPropertyValueType.NotLike:
						if (!string.IsNullOrEmpty(filter.Value.ToString()))
							list.Add(Expression.Not(Expression.Like(filter.Property, filter.Value + "%")));
						break;
					case FilterPropertyValueType.Greater:
						list.Add(Expression.Gt(filter.Property, filter.Value));
						break;
					case FilterPropertyValueType.Lower:
						list.Add(Expression.Lt(filter.Property, filter.Value));
						break;
				}
			}
			return list.ToArray();
		}

		public virtual T[] FindAllByProperties(System.Collections.Generic.IList<FilterPropertyValue> filter)
		{
			return ActiveRecordMediator<T>.FindAll(BuildCriteriaFromFilters(filter));
		}

		public virtual T[] FindAllByPropertiesOrdered(System.Collections.Generic.IList<FilterPropertyValue> filter, string orderBy)
		{
			return FindAllByPropertiesOrdered(filter, orderBy, true);
		}

		public virtual T[] FindAllByPropertiesOrdered(System.Collections.Generic.IList<FilterPropertyValue> filter, string orderBy, bool ascending)
		{
			return ActiveRecordMediator<T>.FindAll(new Order[] { new Order(orderBy, ascending) }, BuildCriteriaFromFilters(filter));
		}

		#endregion

		#region ISlicedFindDao<T,PK> Members


		public virtual T[] SlicedFindAllByProperties(int startIndex, int pageSize, System.Collections.Generic.IList<FilterPropertyValue> filter)
		{
			return ActiveRecordMediator<T>.SlicedFindAll(startIndex, pageSize, BuildCriteriaFromFilters(filter));
		}

		public virtual T[] SlicedFindAllByPropertiesOrdered(int startIndex, int pageSize, System.Collections.Generic.IList<FilterPropertyValue> filter, string orderBy)
		{
			return SlicedFindAllByPropertiesOrdered(startIndex, pageSize, filter, orderBy, true);
		}

		public virtual T[] SlicedFindAllByPropertiesOrdered(int startIndex, int pageSize, System.Collections.Generic.IList<FilterPropertyValue> filter, string orderBy, bool ascending)
		{
			return ActiveRecordMediator<T>.SlicedFindAll(startIndex, pageSize, new Order[] { new Order(orderBy, ascending) }, BuildCriteriaFromFilters(filter));
		}

		public virtual int CountByProperties(System.Collections.Generic.IList<FilterPropertyValue> filter)
		{
			return ActiveRecordMediator<T>.Count(BuildCriteriaFromFilters(filter));
		}

		#endregion
	}
}
