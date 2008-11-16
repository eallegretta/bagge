using System;
using System.Collections.Generic;
using System.Text;
using Bagge.Seti.BusinessEntities;


namespace Bagge.Seti.BusinessLogic
{
	public interface IManager<T, PK> where T: PrimaryKeyDomainObject<T,PK>
	{
		T[] FindAll();
		T[] FindAllOrdered(string orderBy);
		T[] FindAllOrdered(string orderBy, bool ascending);
		T[] FindAllByProperty(string property, object value);
		T[] FindAllByPropertyOrdered(string property, object value, string orderBy);
		T[] FindAllByPropertyOrdered(string property, object value, string orderBy, bool ascencing);
		T[] SlicedFindAll(int startIndex, int pageSize);
		T[] SlicedFindAllOrdered(int startIndex, int pageSize, string orderBy);
		T[] SlicedFindAllOrdered(int startIndex, int pageSize, string orderBy, bool ascending);
		T[] SlicedFindAllByProperty(int startIndex, int pageSize, string property, object value);
		T[] SlicedFindAllByPropertyOrdered(int startIndex, int pageSize, string property, object value, string orderBy);
		T[] SlicedFindAllByPropertyOrdered(int startIndex, int pageSize, string property, object value, string orderBy, bool ascending);
		int Count();
		int CountByProperty(string property, object value);
		T Get(PK id);
		PK Create(T instance);
		void Update(T instance);
		void Delete(PK id);
	}
}
