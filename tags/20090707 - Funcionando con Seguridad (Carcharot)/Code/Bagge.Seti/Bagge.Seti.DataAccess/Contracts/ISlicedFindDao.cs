using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.DataAccess.Contracts
{
	public interface ISlicedFindDao<T, PK> where T : PrimaryKeyDomainObject<T, PK>
	{
		T[] SlicedFindAll(int startIndex, int pageSize, string orderBy, bool? ascending);
		T[] SlicedFindAllByProperty(
			int startIndex, int pageSize, string property, object value
			, string orderBy, bool? ascending);
		T[] SlicedFindAllByProperties(int startIndex, int pageSize, 
			IList<FilterPropertyValue> filter, string orderBy, bool? ascending);
		int Count();
		int CountByProperty(string property, object value);
		int CountByProperties(IList<FilterPropertyValue> filter);
	}
}
