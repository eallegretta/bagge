using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.DataAccess.Contracts
{
	public interface IFindDao<T, PK> where T : PrimaryKeyDomainObject<T, PK>
	{
		T[] FindAll();
		T[] FindAllOrdered(string orderBy);
		T[] FindAllOrdered(string orderBy, bool ascending);
		T[] FindAllByProperty(string property, object value);
		T[] FindAllByPropertyOrdered(string property, object value, string orderBy);
		T[] FindAllByPropertyOrdered(string property, object value, string orderBy, bool ascencing);
	}
}
