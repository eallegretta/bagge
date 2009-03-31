using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.BusinessLogic.Contracts
{
	public interface IFindActiveManager<T, PK> where T : PrimaryKeyDomainObject<T, PK>
	{
		T[] FindAllActive();
		T[] FindAllActiveOrdered(string orderBy);
		T[] FindAllActiveOrdered(string orderBy, bool ascending);
		T[] FindAllActiveByProperty(string property, object value);
		T[] FindAllActiveByPropertyOrdered(string property, object value, string orderBy);
		T[] FindAllActiveByPropertyOrdered(string property, object value, string orderBy, bool ascencing);
		T[] FindAllActiveByProperties(IList<FilterPropertyValue> filter);
		T[] FindAllActiveByPropertiesOrdered(IList<FilterPropertyValue> filter, string orderBy);
		T[] FindAllActiveByPropertiesOrdered(IList<FilterPropertyValue> filter, string orderBy, bool ascending);
	}
}
