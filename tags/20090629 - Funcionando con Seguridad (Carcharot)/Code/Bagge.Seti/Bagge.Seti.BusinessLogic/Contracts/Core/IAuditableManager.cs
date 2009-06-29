using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.BusinessLogic.Contracts
{
	public interface IAuditableManager<T, PK>: IManager<T, PK>, IFindActiveManager<T, PK>, IUndeleteManager<T, PK> where T: PrimaryKeyDomainObject<T,PK>
	{
		
	}
}
