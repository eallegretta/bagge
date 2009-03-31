using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.BusinessLogic.Contracts
{
	public interface IUndeleteManager<T,PK> where T: PrimaryKeyDomainObject<T, PK>
	{
		void Undelete(PK id);
	}
}
