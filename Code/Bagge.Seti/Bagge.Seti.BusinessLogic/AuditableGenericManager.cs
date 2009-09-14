﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.BusinessEntities;
using System.Collections;
using Bagge.Seti.DataAccess.Contracts;

namespace Bagge.Seti.BusinessLogic
{
	public class AuditableGenericManager<T, PK>: GenericManager<T, PK>, IAuditableManager<T, PK> where T: PrimaryKeyDomainObject<T, PK>
	{
		protected static readonly FilterPropertyValue DeletedFilter = new FilterPropertyValue
		{
			Property = "Deleted",
			Type = FilterPropertyValueType.Equals,
			Value = true
		};

		protected static readonly FilterPropertyValue NotDeletedFilter = new FilterPropertyValue
		{
			Property = "Deleted",
			Type = FilterPropertyValueType.Equals,
			Value = false
		};

		public AuditableGenericManager(IDao<T, PK> dao)
			: base(dao)
		{
		}

		protected bool IsDelete { get; private set; }
		protected bool IsUndelete { get; private set; }

		protected bool IsDeleteOrUndelete { get { return IsDelete || IsUndelete; } }


		public override void Delete(T instance)
		{
			IAuditable auditable = instance as IAuditable;
			if (auditable == null)
				base.Delete(instance);
			else
			{
				auditable.Deleted = true;
				IsDelete = true;
				Update((T)auditable);
				IsDelete = false;
			}
		}

		public virtual void Undelete(T instance)
		{
			var auditable = instance as IAuditable;
			if (auditable == null)
				return;
			auditable.Deleted = false;
			IsUndelete = true;
			Update((T)auditable);
			IsUndelete = false;
		}

		#region IFindActiveManager<T,PK> Members

		public T[] FindAllActive()
		{
			return FindAllByProperty("Deleted", false);
		}

		public T[] FindAllActiveOrdered(string orderBy)
		{
			return FindAllByPropertyOrdered("Deleted", false, orderBy);
		}

		public T[] FindAllActiveOrdered(string orderBy, bool ascending)
		{
			return FindAllByPropertyOrdered("Deleted", false, orderBy, ascending);
		}

		private static IList<FilterPropertyValue> GetActiveFilter(IList<FilterPropertyValue> customFilters)
		{
			var filters = GetActiveFilter();
			foreach (var filter in customFilters)
			{
				filters.Add(filter);
			}
			return filters;
		}

		private static IList<FilterPropertyValue> GetActiveFilter(string property, object value)
		{
			var filters = GetActiveFilter();
			filters.Add(new FilterPropertyValue { Property = property, Value = value, Type = FilterPropertyValueType.Equals });
			return filters;
		}

		private static IList<FilterPropertyValue> GetActiveFilter()
		{
			var filters = new List<FilterPropertyValue>();
			filters.Add(new FilterPropertyValue { Property = "Deleted", Value = false, Type = FilterPropertyValueType.Equals });
			return filters;
		}

		public T[] FindAllActiveByProperty(string property, object value)
		{
			return FindAllByProperties(GetActiveFilter(property, value));
		}

		public T[] FindAllActiveByPropertyOrdered(string property, object value, string orderBy)
		{
			return FindAllByPropertiesOrdered(GetActiveFilter(property, value), orderBy);
		}

		public T[] FindAllActiveByPropertyOrdered(string property, object value, string orderBy, bool ascending)
		{
			return FindAllByPropertiesOrdered(GetActiveFilter(property, value), orderBy, ascending);
		}

		public T[] FindAllActiveByProperties(IList<Bagge.Seti.BusinessEntities.FilterPropertyValue> filter)
		{
			return FindAllByProperties(GetActiveFilter(filter));
		}

		public T[] FindAllActiveByPropertiesOrdered(IList<Bagge.Seti.BusinessEntities.FilterPropertyValue> filter, string orderBy)
		{
			return FindAllByPropertiesOrdered(GetActiveFilter(filter), orderBy);
		}

		public T[] FindAllActiveByPropertiesOrdered(IList<Bagge.Seti.BusinessEntities.FilterPropertyValue> filter, string orderBy, bool ascending)
		{
			return FindAllByPropertiesOrdered(GetActiveFilter(filter), orderBy, ascending);
		}

		#endregion
	}
}
