using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.BusinessLogic.Contracts
{
	public interface IFindManager<T, PK> where T : PrimaryKeyDomainObject<T, PK>
	{
		T[] FindAll();
		T[] FindAllOrdered(string orderBy);
		T[] FindAllOrdered(string orderBy, bool ascending);
		T[] FindAllByProperty(string property, object value);
		T[] FindAllByPropertyOrdered(string property, object value, string orderBy);
		T[] FindAllByPropertyOrdered(string property, object value, string orderBy, bool ascencing);
		T[] FindAllByProperties(IList<FilterPropertyValue> filter);
		T[] FindAllByPropertiesOrdered(IList<FilterPropertyValue> filter, string orderBy);
		T[] FindAllByPropertiesOrdered(IList<FilterPropertyValue> filter, string orderBy, bool ascending);
	}
}
