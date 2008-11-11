using System;
using System.Collections.Generic;
using System.Text;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.DataAccess;

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
			return Dao.Get(id);
		}
		public virtual T[] FindAll()
		{
			return Dao.FindAll();
		}
		public virtual PK Create(T instance)
		{
			if (instance == null)
				throw new ArgumentNullException("instance");

			Dao.Create(instance);
			return instance.Id;
		}

		public virtual void Update(T instance)
		{
			if (instance == null)
				throw new ArgumentNullException("instance");

			Dao.Update(instance);
		}

		public virtual void Delete(T instance)
		{
			if (instance == null)
				throw new ArgumentNullException("instance");

			Dao.Delete(instance.Id);
		}
		public virtual void Delete(PK id)
		{
			Dao.Delete(id);
		}

		#endregion
	}
}
