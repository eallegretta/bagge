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
	public class GenericDao<T, PK> : IDao<T, PK> where T : PrimaryKeyDomainObject<T, PK>
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
						if (!string.IsNullOrEmpty(filter.Value.ToString()))
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
			/*if (!string.IsNullOrEmpty(orderBy))
				return ActiveRecordMediator<T>.FindAll(new Order[] { 
					new Order(orderBy, ascending.HasValue ? ascending.Value : true) }, BuildCriteriaFromFilters(filter));

			return ActiveRecordMediator<T>.FindAll(BuildCriteriaFromFilters(filter));*/

			return GetFilteredRecords(null, null, orderBy, ascending.HasValue ? ascending.Value : true, filter);
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

		private class FilterCriteria : FilterPropertyValue
		{
			public string VariableName { get; set; }
		}

		private IList<FilterCriteria> GetAndFilterCriterias(IList<FilterPropertyValue> filters)
		{
			List<FilterCriteria> criterias = new List<FilterCriteria>();

			var andProperties = (from filter in filters
								 group filter by filter.Property
									 into gr
									 where gr.Count() == 1
									 select gr.Key).ToArray();

			var andFilters = (from filter in filters
							 where andProperties.Contains(filter.Property)
							 select filter).ToArray();

			foreach (var filter in andFilters)
			{
				FilterCriteria criteria = new FilterCriteria();
				criteria.Property = filter.Property;
				criteria.Type = filter.Type;
				criteria.Value = filter.Value;
				criteria.VariableName = filter.Property.ToLower();
				criterias.Add(criteria);
			}

			return criterias;
		}

		private IList<FilterCriteria> GetOrFilterCriterias(IList<FilterPropertyValue> filters)
		{
			List<FilterCriteria> criterias = new List<FilterCriteria>();

			var orProperties = (from filter in filters
								 group filter by filter.Property
									 into gr
									 where gr.Count() > 1
									 select gr.Key).ToArray();

			var orFilters = (from filter in filters
							 where orProperties.Contains(filter.Property)
							 orderby filter.Property
							 select filter).ToArray();

			string lastProperty = string.Empty;
			int index = 0;

			foreach (var filter in orFilters)
			{
				FilterCriteria criteria = new FilterCriteria();
				criteria.Property = filter.Property;
				criteria.Type = filter.Type;
				criteria.Value = filter.Value;

				if (lastProperty != filter.Property)
				{
					index = 0;
					lastProperty = filter.Property;
				}
				criteria.VariableName = filter.Property.ToLower() + new String('_', index++);
				
				criterias.Add(criteria);
			}

			return criterias;
		}

		protected T[] GetFilteredRecords(int? firstResult, int? maxResults, string orderBy, bool ascending, IList<FilterPropertyValue> filters)
		{
			StringBuilder hql = new StringBuilder();
			hql.AppendFormat("from {0} el", typeof(T).Name);

			StringBuilder where = new StringBuilder();

			IList<FilterCriteria> andFilterCriterias = GetAndFilterCriterias(filters);
			IList<FilterCriteria> orFilterCriterias = GetOrFilterCriterias(filters);

			int countAnd = andFilterCriterias.Count;
			int countOr = orFilterCriterias.Count;

			GetFilteredRecordsAppendAndCondition(where, andFilterCriterias, countAnd, countOr);

			GetFilteredRecordsAppendOrCondition(where, orFilterCriterias, countOr);

			if(where.Length > 0)
				hql.AppendFormat(" where {0}", where);

			if (!string.IsNullOrEmpty(orderBy))
				hql.AppendFormat(" orderby {0} {1}", orderBy, (ascending) ? "asc" : "desc");



			SimpleQuery<T> query = new SimpleQuery<T>(hql.ToString());

			GetFilteredRecordsSetQueryValues(andFilterCriterias, query);
			GetFilteredRecordsSetQueryValues(orFilterCriterias, query);

			if (firstResult.HasValue && maxResults.HasValue)
				query.SetQueryRange(firstResult.Value, maxResults.Value);

			return query.Execute();
		}

		private static void GetFilteredRecordsSetQueryValues(IList<FilterCriteria> filters, SimpleQuery<T> query)
		{
			foreach (var filter in filters)
			{
				if (query.Query.Contains(":" + filter.VariableName))
				{
					if (filter.Type.In(FilterPropertyValueType.Like, FilterPropertyValueType.NotLike))
						query.SetParameter(filter.VariableName, filter.Value + "%");
					else if (filter.Type.In(FilterPropertyValueType.Contains, FilterPropertyValueType.NotContains))
						query.SetParameter(filter.VariableName, "%" + filter.Value + "%");
					else
						query.SetParameter(filter.VariableName, filter.Value);
				}
			}
		}

		private void GetFilteredRecordsAppendOrCondition(StringBuilder where, IList<FilterCriteria> orFilterCriterias, int countOr)
		{
			string lastOrProperty = string.Empty;
			for (int index = 0; index < countOr; index++)
			{
				FilterCriteria criteria = orFilterCriterias[index];
				if (lastOrProperty != criteria.Property)
				{
					if (lastOrProperty == string.Empty)
						where.Append("(");
					else
					{
						where.Remove(where.Length - 5, 4);
						where.Append(") and (");
					}
					lastOrProperty = criteria.Property;
				}
				if (index < countOr - 1)
					where.Append(GetWhereClauseBasedOnFilter(orFilterCriterias[index], "or"));
				else
					where.Append(GetWhereClauseBasedOnFilter(orFilterCriterias[index], string.Empty));
			}

			if (countOr > 0)
			{
				if(where.ToString().EndsWith("or "))
					where.Remove(where.Length - 4, 3);
				where.Append(")");
			}
		}

		private void GetFilteredRecordsAppendAndCondition(StringBuilder where, IList<FilterCriteria> andFilterCriterias, int countAnd, int countOr)
		{
			for (int index = 0; index < countAnd; index++)
			{
				if (index < countAnd - 1)
					where.Append(GetWhereClauseBasedOnFilter(andFilterCriterias[index], "and"));
				else
				{
					if (countOr == 0)
						where.Append(GetWhereClauseBasedOnFilter(andFilterCriterias[index], string.Empty));
					else
						where.Append(GetWhereClauseBasedOnFilter(andFilterCriterias[index], "and"));
				}
			}

			if (countAnd > 0 && countOr == 0)
			{
				if (where.ToString().EndsWith("and "))
					where.Remove(where.Length - 5, 4);
			}
		}

		private string GetWhereClauseBasedOnFilter(FilterCriteria filter, string condition)
		{
			if (!string.IsNullOrEmpty(condition))
				condition = " " + condition + " ";

			switch (filter.Type)
			{
				case FilterPropertyValueType.Equals:
					return string.Format("el.{0} = :{1}{2}", filter.Property, filter.VariableName, condition);
				case FilterPropertyValueType.NotEquals:
					return string.Format("el.{0} <> :{1}{2}", filter.Property, filter.VariableName, condition);
				case FilterPropertyValueType.Like:
				case FilterPropertyValueType.Contains:
					if (!string.IsNullOrEmpty(filter.Value.ToString()))
						return string.Format("el.{0} like :{1}{2}", filter.Property, filter.VariableName, condition);
					return string.Empty;
				case FilterPropertyValueType.NotLike:
				case FilterPropertyValueType.NotContains:
					if (!string.IsNullOrEmpty(filter.Value.ToString()))
						return string.Format("el.{0} not like :{1}{2}", filter.Property, filter.VariableName, condition);
					return string.Empty;
				case FilterPropertyValueType.Greater:
					return string.Format("el.{0} > :{1}{2}", filter.Property, filter.VariableName, condition);
				case FilterPropertyValueType.GreaterEquals:
					return string.Format("el.{0} >= :{1}{2}", filter.Property, filter.VariableName, condition);
				case FilterPropertyValueType.Lower:
					return string.Format("el.{0} < :{1}{2}", filter.Property, filter.VariableName, condition);
				case FilterPropertyValueType.LowerEquals:
					return string.Format("el.{0} <= :{1}{2}", filter.Property, filter.VariableName, condition);
				case FilterPropertyValueType.In:
					return string.Format(":{1} in elements(el.{0}){2}", filter.Property, filter.VariableName, condition);
				case FilterPropertyValueType.NotIn:
					return string.Format(":{1} not in elements(el.{0}){2}", filter.Property, filter.VariableName, condition);
			}
			return string.Empty;
		}

	}
}
