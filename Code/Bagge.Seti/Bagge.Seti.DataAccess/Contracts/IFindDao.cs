using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.DataAccess.Contracts
{
	public interface IFindDao<T, PK> where T : PrimaryKeyDomainObject<T, PK>
	{
		T[] FindAll(string orderBy, bool? ascending);
		T[] FindAllByProperty(string property, object value, string orderBy, bool? ascending);
		T[] FindAllByProperties(IList<FilterPropertyValue> filter, string orderBy, bool? ascending);
	}
}
