using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.BusinessLogic.Contracts
{
	public interface IByNameSelectableManager<T, PK> where T:PrimaryKeyWithNameDomainObject<T, PK>
	{
		T GetByName(string name);
		T[] FindAllByName(string productName);
		T[] FindAllByName(string productName, int maxRecords);
	}
}
