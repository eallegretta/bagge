using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.BusinessLogic.Contracts
{
	public interface ISlicedFindManager<T, PK> where T : PrimaryKeyDomainObject<T, PK>
	{
		T[] SlicedFindAll(int startIndex, int pageSize);
		T[] SlicedFindAllOrdered(int startIndex, int pageSize, string orderBy);
		T[] SlicedFindAllOrdered(int startIndex, int pageSize, string orderBy, bool ascending);
		T[] SlicedFindAllByProperty(int startIndex, int pageSize, string property, object value);
		T[] SlicedFindAllByPropertyOrdered(int startIndex, int pageSize, string property, object value, string orderBy);
		T[] SlicedFindAllByPropertyOrdered(int startIndex, int pageSize, string property, object value, string orderBy, bool ascending);
		int Count();
		int CountByProperty(string property, object value);
	}
}
