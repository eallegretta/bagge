using System;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.DataAccess
{
	public interface IDao<T, PK> where T: PrimaryKeyDomainObject<T, PK>
	{
		T[] FindAll();
		T Get(PK id);
		PK Create(T instance);
		void Update(T instance);
		void Delete(PK id);
	}
}
