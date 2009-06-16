using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.DataAccess.Contracts
{
	public interface ISimpleFindGetDao<T, PK>: IGetDao<T, PK> where T: PrimaryKeyDomainObject<T, PK>
	{
		T[] FindAll();
	}
}
