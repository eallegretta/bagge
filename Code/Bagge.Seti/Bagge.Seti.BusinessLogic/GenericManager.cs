using System;
using System.Collections.Generic;
using System.Text;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.DataAccess;
using Bagge.Seti.DesignByContract;
using Bagge.Seti.BusinessLogic.Properties;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.DataAccess.Contracts;
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.Security.BusinessEntities;

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

		[SecurizableCrud("Securizable_GenericManager_Get", typeof(RandomPassword), FunctionAction.List)]
		public T Get(PK id)
		{
			Check.Require(!id.Equals(default(PK)), string.Format(Resources.IdCannotBeDefault, default(PK)));

			T instance = Dao.Get(id);

			Check.Ensure(instance != null, string.Format(Resources.InstanceNotFound, id, typeof(T)));

			return instance;
		}

		[SecurizableCrud("Securizable_GenericManager_Create", typeof(RandomPassword), FunctionAction.Create), ]
		public virtual PK Create(T instance)
		{
			Check.Require(instance != null, string.Format(Resources.InstanceCannotBeNull, typeof(T)));

			Dao.Create(instance);

			Check.Ensure(!instance.Id.Equals(default(PK)), string.Format(Resources.InstanceIdCannotBeDefault, default(PK), typeof(T)));

			return instance.Id;
		}

		[SecurizableCrud("Securizable_GenericManager_Update", typeof(RandomPassword), FunctionAction.Update)]
		public virtual void Update(T instance)
		{
			Check.Require(instance != null, string.Format(Resources.InstanceCannotBeNull, typeof(T)));

			Dao.Update(instance);
		}

		[SecurizableCrud("Securizable_GenericManager_Delete", typeof(RandomPassword), FunctionAction.Delete)]
		public virtual void Delete(T instance)
		{
			Check.Require(instance != null, string.Format(Resources.InstanceCannotBeNull, typeof(T)));
			Check.Require(!instance.Id.Equals(default(PK)), string.Format(Resources.IdCannotBeDefault, default(PK)));

			Delete(instance.Id);
		}

		
		private void Delete(PK id)
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

		[SecurizableCrud("Securizable_GenericManager_FindAll", typeof(RandomPassword), FunctionAction.List)]
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

		[SecurizableCrud("Securizable_GenericManager_FindAllByProperty", typeof(RandomPassword), FunctionAction.List)]
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

		[SecurizableCrud("Securizable_GenericManager_SlicedFindAll", typeof(RandomPassword), FunctionAction.List)]
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

		[SecurizableCrud("Securizable_GenericManager_SlicedFindAllByProperty", typeof(RandomPassword), FunctionAction.List)]
		protected virtual T[] SlicedFindAllByProperty(int pageIndex, int pageSize, string property, object value, string orderBy, bool? ascending)
		{
			return Dao.SlicedFindAllByProperty(pageIndex, pageSize, property, value, orderBy, ascending);
		}

		[Securizable("Securizable_GenericManager_Count", typeof(RandomPassword))]
		public virtual int Count()
		{
			return Dao.Count();
		}

		[Securizable("Securizable_GenericManager_CountByProperty", typeof(RandomPassword))]
		public virtual int CountByProperty(string property, object value)
		{

			return Dao.CountByProperty(property, value);
		}

		#endregion

		protected virtual void ReplaceFilters(IList<FilterPropertyValue> filters) { }

		#region IFindManager<T,PK> Members


		public T[] FindAllByProperties(IList<FilterPropertyValue> filter)
		{
			return FindAllByProperties(filter, null, null);
		}

		public T[] FindAllByPropertiesOrdered(IList<FilterPropertyValue> filter, string orderBy)
		{
			return FindAllByProperties(filter, orderBy, null);
		}

		public T[] FindAllByPropertiesOrdered(IList<FilterPropertyValue> filter, string orderBy, bool ascending)
		{
			return FindAllByProperties(filter, orderBy, ascending);
		}

		[SecurizableCrud("Securizable_GenericManager_FindAllByProperties", typeof(RandomPassword), FunctionAction.List)]
		protected virtual T[] FindAllByProperties(IList<FilterPropertyValue> filter, string orderBy, bool? ascending)
		{
			ReplaceFilters(filter);

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

		[SecurizableCrud("Securizable_GenericManager_SlicedFindAllByProperties", typeof(RandomPassword), FunctionAction.List)]
		protected virtual T[] SlicedFindAllByProperties(int startIndex, int pageSize, IList<FilterPropertyValue> filter, string orderBy, bool? ascending)
		{
			ReplaceFilters(filter);

			return Dao.SlicedFindAllByProperties(startIndex, pageSize, filter, orderBy, ascending);
		}

		[SecurizableCrud("Securizable_GenericManager_CountByProperties", typeof(RandomPassword), FunctionAction.List)]
		public virtual int CountByProperties(IList<FilterPropertyValue> filter)
		{
			ReplaceFilters(filter);

			return Dao.CountByProperties(filter);
		}

		#endregion
	}
}
