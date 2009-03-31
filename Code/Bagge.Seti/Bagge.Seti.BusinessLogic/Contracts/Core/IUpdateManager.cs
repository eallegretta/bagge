using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.BusinessLogic.Contracts
{
	public interface IUpdateManager<T, PK> where T : PrimaryKeyDomainObject<T, PK>
	{
		void Update(T instance);
	}
}
