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
		protected IDao<T,PK> Dao;


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
			return Dao.FindAll();
		}

		public T[] FindAllOrdered(string orderBy)
		{
			return Dao.FindAllOrdered(orderBy);
		}

		public T[] FindAllOrdered(string orderBy, bool ascending)
		{
			return Dao.FindAllOrdered(orderBy, ascending);
		}

		public T[] FindAllByProperty(string property, object value)
		{
			return Dao.FindAllByProperty(property, value);
		}

		public T[] FindAllByPropertyOrdered(string property, object value, string orderBy)
		{
			return Dao.FindAllByPropertyOrdered(property, value, orderBy);
		}

		public T[] FindAllByPropertyOrdered(string property, object value, string orderBy, bool ascending)
		{
			return Dao.FindAllByPropertyOrdered(property, value, orderBy, ascending);
		}

		public T[] SlicedFindAll(int pageIndex, int pageSize)
		{
			return Dao.SlicedFindAll(pageIndex, pageSize);
		}

		public T[] SlicedFindAllOrdered(int pageIndex, int pageSize, string orderBy)
		{
			return Dao.SlicedFindAllOrdered(pageIndex, pageSize, orderBy);
		}

		public T[] SlicedFindAllOrdered(int pageIndex, int pageSize, string orderBy, bool ascending)
		{
			return Dao.SlicedFindAllOrdered(pageIndex, pageSize, orderBy, ascending);
		}

		public T[] SlicedFindAllByProperty(int pageIndex, int pageSize, string property, object value)
		{
			return Dao.SlicedFindAllByProperty(pageIndex, pageSize, property, value);
		}

		public T[] SlicedFindAllByPropertyOrdered(int pageIndex, int pageSize, string property, object value, string orderBy)
		{
			return Dao.SlicedFindAllByPropertyOrdered(pageIndex, pageSize, property, value, orderBy);
		}

		public T[] SlicedFindAllByPropertyOrdered(int pageIndex, int pageSize, string property, object value, string orderBy, bool ascending)
		{
			return Dao.SlicedFindAllByPropertyOrdered(pageIndex, pageSize, property, value, orderBy, ascending);
		}

		public int Count()
		{
			return Dao.Count();
		}

		public int CountByProperty(string property, object value)
		{
			return Dao.CountByProperty(property, value);
		}

		#endregion
	}
}
