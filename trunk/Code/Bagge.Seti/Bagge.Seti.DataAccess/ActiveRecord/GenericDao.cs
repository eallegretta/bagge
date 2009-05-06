using System;
using System.Linq;
using Bagge.Seti.BusinessEntities;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Scopes;
using NHibernate.Expression;
using Bagge.Seti.DataAccess.Contracts;
using System.Collections.Generic;
using NHibernate.SqlCommand;
using System.Text;
using Castle.ActiveRecord.Queries;

namespace Bagge.Seti.DataAccess.ActiveRecord
{
	public class GenericDao<T,PK>: IDao<T,PK> where T: PrimaryKeyDomainObject<T, PK>
	{
		#region IDao<T,PK> Members

		public virtual T[] FindAll(string orderBy, bool? ascending)
		{
			if (!string.IsNullOrEmpty(orderBy))
				return ActiveRecordMediator<T>.FindAll(new Order[] { new Order(orderBy, (ascending.HasValue) ? ascending.Value : true) });
	
			return ActiveRecordMediator<T>.FindAll();
		}


		public virtual T[] FindAllByProperty(string property, object value, string orderBy, bool? ascending)
		{
			if (!string.IsNullOrEmpty(orderBy))
				return ActiveRecordMediator<T>.FindAll(new Order[] { new Order(orderBy, ascending.HasValue ? ascending.Value : true) },
				Expression.Eq(property, value));

			return ActiveRecordMediator<T>.FindAll(Expression.Eq(property, value));
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
			ActiveRecordMediator<T>.Update(instance);
		}

		public virtual void Delete(PK id)
		{
			ActiveRecordMediator<T>.Delete(Get(id));	
		}

		public virtual T[] SlicedFindAll(int pageIndex, int pageSize, string orderBy, bool? ascending)
		{
			if (!string.IsNullOrEmpty(orderBy))
				return ActiveRecordMediator<T>.SlicedFindAll(pageIndex, pageSize,
					new Order[] { new Order(orderBy, ascending.HasValue ? ascending.Value : true) });
			
			return ActiveRecordMediator<T>.SlicedFindAll(pageIndex, pageSize,
				new Order[] { });
		}


		public virtual T[] SlicedFindAllByProperty(int pageIndex, int pageSize, string property, 
			object value, string orderBy, bool? ascending)
		{
			if (!string.IsNullOrEmpty(orderBy))
				return ActiveRecordMediator<T>.SlicedFindAll(pageIndex, pageSize,
					new Order[] { new Order(orderBy, ascending.HasValue ? ascending.Value : true) },
					Expression.Eq(property, value));

			return ActiveRecordMediator<T>.SlicedFindAll(pageIndex, pageSize,
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
					case FilterPropertyValueType.GreaterEquals:
						list.Add(Expression.Ge(filter.Property, filter.Value));
						break;
					case FilterPropertyValueType.Lower:
						list.Add(Expression.Lt(filter.Property, filter.Value));
						break;
					case FilterPropertyValueType.LowerEquals:
						list.Add(Expression.Le(filter.Property, filter.Value));
						break;
				}
			}
			return list.ToArray();
		}

		public virtual T[] FindAllByProperties(IList<FilterPropertyValue> filter, string orderBy, bool? ascending)
		{
			if(!string.IsNullOrEmpty(orderBy))
				return ActiveRecordMediator<T>.FindAll(new Order[] { 
					new Order(orderBy, ascending.HasValue ? ascending.Value : true) }, BuildCriteriaFromFilters(filter));

			return ActiveRecordMediator<T>.FindAll(BuildCriteriaFromFilters(filter));
		}

		public virtual T[] SlicedFindAllByProperties(int startIndex, int pageSize, IList<FilterPropertyValue> filter, 
			string orderBy, bool? ascending)
		{

			return GetFilteredRecords(startIndex, pageSize, orderBy, (ascending.HasValue) ? ascending.Value : true, filter);
		}


		public virtual int CountByProperties(System.Collections.Generic.IList<FilterPropertyValue> filter)
		{
			return ActiveRecordMediator<T>.Count(BuildCriteriaFromFilters(filter));
		}

		private class FilterCriteria: FilterPropertyValue 
		{
			public string VariableName { get; set; }
			public string Condition { get; set; }
		}

		private IList<FilterCriteria> GetFilterCriterias(IList<FilterPropertyValue> filters)
		{
			List<FilterCriteria> criterias = new List<GenericDao<T, PK>.FilterCriteria>();
			foreach (var filter in filters)
			{
			}

			return null;
		}

		protected T[] GetFilteredRecords(int? firstResult, int? maxResults, string orderBy, bool ascending, IList<FilterPropertyValue> filters)
		{
			StringBuilder hql = new StringBuilder();
			hql.AppendFormat("from {0} el", typeof(T).Name);

			StringBuilder where = new StringBuilder();
			for (int index = 0; index < filters.Count; index++)
			{
				if (index < filters.Count - 1)
					where.Append(GetWhereClauseBasedOnFilter(filters[index], "and"));
				else
					where.Append(GetWhereClauseBasedOnFilter(filters[index], string.Empty));
			}

			if (where.Length > 0)
			{
				if (where.ToString().EndsWith("and "))
					where = where.Remove(where.Length - 5, 4);

				hql.AppendFormat(" where {0}", where.ToString());
			}

			if(!string.IsNullOrEmpty(orderBy))
				hql.AppendFormat(" orderby {0} {1}", orderBy, (ascending) ? "asc" : "desc");
	
			

			SimpleQuery<T> query = new SimpleQuery<T>(hql.ToString());

			foreach (FilterPropertyValue filter in filters)
			{
				if (query.Query.Contains(":" + filter.Property.ToLower()))
				{
					if (filter.Type == FilterPropertyValueType.Like || filter.Type == FilterPropertyValueType.NotLike)
						query.SetParameter(filter.Property.ToLower(), filter.Value + "%");
					else
						query.SetParameter(filter.Property.ToLower(), filter.Value);
				}
			}
			if (firstResult.HasValue && maxResults.HasValue)
				query.SetQueryRange(firstResult.Value, maxResults.Value);

			return query.Execute();
		}

		private string GetWhereClauseBasedOnFilter(FilterPropertyValue filter, string condition)
		{
			if (!string.IsNullOrEmpty(condition))
				condition = " " + condition + " ";

			switch (filter.Type)
			{
				case FilterPropertyValueType.Equals:
					return string.Format("el.{0} = :{1}{2}", filter.Property, filter.Property.ToLower(), condition);
				case FilterPropertyValueType.NotEquals:
					return string.Format("el.{0} <> :{1}{2}", filter.Property, filter.Property.ToLower(), condition);
				case FilterPropertyValueType.Like:
					if (!string.IsNullOrEmpty(filter.Value.ToString()))
						return string.Format("el.{0} like :{1}{2}", filter.Property, filter.Property.ToLower(), condition);
					return string.Empty;
				case FilterPropertyValueType.NotLike:
					if (!string.IsNullOrEmpty(filter.Value.ToString()))
						return string.Format("el.{0} not like :{1}{2}", filter.Property, filter.Property.ToLower(), condition);
					return string.Empty;
				case FilterPropertyValueType.Greater:
					return string.Format("el.{0} > :{1}{2}", filter.Property, filter.Property.ToLower(), condition);
				case FilterPropertyValueType.GreaterEquals:
					return string.Format("el.{0} >= :{1}{2}", filter.Property, filter.Property.ToLower(), condition);
				case FilterPropertyValueType.Lower:
					return string.Format("el.{0} < :{1}{2}", filter.Property, filter.Property.ToLower(), condition);
				case FilterPropertyValueType.LowerEquals:
					return string.Format("el.{0} <= :{1}{2}", filter.Property, filter.Property.ToLower(), condition);
				case FilterPropertyValueType.In:
					return string.Format(":{1} in elements(el.{0}){2}", filter.Property, filter.Property.ToLower(), condition);
				case FilterPropertyValueType.NotIn:
					return string.Format(":{1} not in elements(el.{0}){2}", filter.Property, filter.Property.ToLower(), condition);
			}
			return string.Empty;
		}

	}
}
