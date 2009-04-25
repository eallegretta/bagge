using System;
using System.Collections.Generic;
using System.Text;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.DataAccess;
using Bagge.Seti.DesignByContract;
using Bagge.Seti.BusinessLogic.Properties;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.DataAccess.Contracts;

namespace Bagge.Seti.BusinessLogic
{
	public class GenericManager<T, PK>: IManager<T, PK>  where T: PrimaryKeyDomainObject<T, PK>
	{
		protected IDao<T, PK> Dao
		{
			get;
			private set;
		}

		protected D GetDao<D>() where D : class
		{
			return Dao as D;
		}




		public GenericManager(IDao<T, PK> dao)
		{
			Dao = dao;
		}

		#region IManager<T> Members

		public T Get(PK id)
		{
			Check.Require(!id.Equals(default(PK)), string.Format(Resources.IdCannotBeDefault, default(PK)));

			T instance = Dao.Get(id);

			Check.Ensure(instance != null, string.Format(Resources.InstanceNotFound, id, typeof(T)));

			return instance;
		}
		
		public virtual PK Create(T instance)
		{
			Check.Require(instance != null, string.Format(Resources.InstanceCannotBeNull, typeof(T)));

			Dao.Create(instance);

			Check.Ensure(!instance.Id.Equals(default(PK)), string.Format(Resources.InstanceIdCannotBeDefault, default(PK), typeof(T)));

			return instance.Id;
		}

		public virtual void Update(T instance)
		{
			Check.Require(instance != null, string.Format(Resources.InstanceCannotBeNull, typeof(T)));

			Dao.Update(instance);
		}

		public virtual void Delete(T instance)
		{
			Check.Require(instance != null, string.Format(Resources.InstanceCannotBeNull, typeof(T)));
			Check.Require(!instance.Id.Equals(default(PK)), string.Format(Resources.IdCannotBeDefault, default(PK)));

			Delete(instance.Id);
		}
		public virtual void Delete(PK id)
		{
			Check.Require(!id.Equals(default(PK)), string.Format(Resources.IdCannotBeDefault, default(PK)));

			Dao.Delete(id);
		}


		public T[] FindAll()
		{
			return FindAll(null, null);
		}

		public T[] FindAllOrdered(string orderBy)
		{
			return FindAll(orderBy, null);
		}

		public T[] FindAllOrdered(string orderBy, bool ascending)
		{
			return FindAll(orderBy, ascending);
		}

		protected virtual T[] FindAll(string orderBy, bool? ascending)
		{
			return Dao.FindAll(orderBy, ascending);
		}


		public T[] FindAllByProperty(string property, object value)
		{
			return FindAllByProperty(property, value, null, null);
		}

		public T[] FindAllByPropertyOrdered(string property, object value, string orderBy)
		{
			return FindAllByProperty(property, value, orderBy, null);
		}

		public T[] FindAllByPropertyOrdered(string property, object value, string orderBy, bool ascending)
		{
			return FindAllByProperty(property, value, orderBy, ascending);
		}

		protected virtual T[] FindAllByProperty(string property, object value, string orderBy, bool? ascending)
		{
			return Dao.FindAllByProperty(property, value, orderBy, ascending);
		}

		public T[] SlicedFindAll(int pageIndex, int pageSize)
		{
			return SlicedFindAll(pageIndex, pageSize, null, null);
		}

		public T[] SlicedFindAllOrdered(int pageIndex, int pageSize, string orderBy)
		{
			return SlicedFindAll(pageIndex, pageSize, orderBy, null);
		}

		public T[] SlicedFindAllOrdered(int pageIndex, int pageSize, string orderBy, bool ascending)
		{
			return SlicedFindAll(pageIndex, pageSize, orderBy, ascending);
		}

		protected virtual T[] SlicedFindAll(int pageIndex, int pageSize, string orderBy, bool? ascending)
		{
			return Dao.SlicedFindAll(pageIndex, pageSize, orderBy, ascending);
		}


		public T[] SlicedFindAllByProperty(int pageIndex, int pageSize, string property, object value)
		{
			return SlicedFindAllByProperty(pageIndex, pageSize, property, value, null, null);
		}

		public T[] SlicedFindAllByPropertyOrdered(int pageIndex, int pageSize, string property, object value, string orderBy)
		{
			return SlicedFindAllByProperty(pageIndex, pageSize, property, value, orderBy, null);
		}

		public T[] SlicedFindAllByPropertyOrdered(int pageIndex, int pageSize, string property, object value, string orderBy, bool ascending)
		{
			return SlicedFindAllByProperty(pageIndex, pageSize, property, value, orderBy, ascending);
		}

		protected virtual T[] SlicedFindAllByProperty(int pageIndex, int pageSize, string property, object value, string orderBy, bool? ascending)
		{
			return Dao.SlicedFindAllByProperty(pageIndex, pageSize, property, value, orderBy, ascending);
		}

		public virtual int Count()
		{
			return Dao.Count();
		}

		public virtual int CountByProperty(string property, object value)
		{
			return Dao.CountByProperty(property, value);
		}

		#endregion

		#region IFindManager<T,PK> Members


		public T[] FindAllByProperties(IList<FilterPropertyValue> filter)
		{
			return FindAllByProperties(filter);
		}

		public T[] FindAllByPropertiesOrdered(IList<FilterPropertyValue> filter, string orderBy)
		{
			return FindAllByPropertiesOrdered(filter, orderBy);
		}

		public T[] FindAllByPropertiesOrdered(IList<FilterPropertyValue> filter, string orderBy, bool ascending)
		{
			return FindAllByPropertiesOrdered(filter, orderBy, ascending);
		}

		protected virtual T[] FindAllByProperties(IList<FilterPropertyValue> filter, string orderBy, bool ascending)
		{
			return Dao.FindAllByProperties(filter, orderBy, ascending);
		}

		#endregion

		#region ISlicedFindManager<T,PK> Members


		public T[] SlicedFindAllByProperties(int startIndex, int pageSize, IList<FilterPropertyValue> filter)
		{
			return SlicedFindAllByProperties(startIndex, pageSize, filter, null, null);
		}

		public T[] SlicedFindAllByPropertiesOrdered(int startIndex, int pageSize, IList<FilterPropertyValue> filter, string orderBy)
		{
			return SlicedFindAllByProperties(startIndex, pageSize, filter, orderBy, null);
		}

		public T[] SlicedFindAllByPropertiesOrdered(int startIndex, int pageSize, IList<FilterPropertyValue> filter, string orderBy, bool ascending)
		{
			return SlicedFindAllByProperties(startIndex, pageSize, filter, orderBy, ascending);
		}

		protected virtual T[] SlicedFindAllByProperties(int startIndex, int pageSize, IList<FilterPropertyValue> filter, string orderBy, bool? ascending)
		{
			return Dao.SlicedFindAllByProperties(startIndex, pageSize, filter, orderBy, ascending);
		}

		public virtual int CountByProperties(IList<FilterPropertyValue> filter)
		{
			return Dao.CountByProperties(filter);
		}

		#endregion
	}
}
