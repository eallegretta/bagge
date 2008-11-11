using System;
using System.Collections.Generic;
using System.Text;
using Bagge.Seti.BusinessEntities;


namespace Bagge.Seti.BusinessLogic
{
	public interface IManager<T, PK> where T: PrimaryKeyDomainObject<T,PK>
	{
		T[] FindAll();
		T Get(PK id);
		PK Create(T instance);
		void Update(T instance);
		void Delete(T instance);
		void Delete(PK id);
	}
}
