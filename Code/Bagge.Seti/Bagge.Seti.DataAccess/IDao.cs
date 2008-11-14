using System;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.DataAccess
{
	public interface IDao<T, PK> where T: PrimaryKeyDomainObject<T, PK>
	{
		T[] FindAll();
		T[] FindAllOrdered(string orderBy);
		T[] FindAllOrdered(string orderBy, bool ascending);
		T[] FindAllByProperty(string property, object value);
		T[] FindAllByPropertyOrdered(string property, object value, string orderBy);
		T[] FindAllByPropertyOrdered(string property, object value, string orderBy, bool ascencing);
		T[] SlicedFindAll(int pageIndex, int pageSize);
		T[] SlicedFindAllOrdered(int pageIndex, int pageSize, string orderBy);
		T[] SlicedFindAllOrdered(int pageIndex, int pageSize, string orderBy, bool ascending);
		T[] SlicedFindAllByProperty(int pageIndex, int pageSize, string property, object value);
		T[] SlicedFindAllByPropertyOrdered(int pageIndex, int pageSize, string property, object value, string orderBy);
		T[] SlicedFindAllByPropertyOrdered(int pageIndex, int pageSize, string property, object value, string orderBy, bool ascending);
		int Count();
		int CountByProperty(string property, object value);
		T Get(PK id);
		PK Create(T instance);
		void Update(T instance);
		void Delete(PK id);
		
	}
}
