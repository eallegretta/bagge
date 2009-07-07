using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.DataAccess.Contracts
{
	public interface IUpdateDao<T, PK> where T : PrimaryKeyDomainObject<T, PK>
	{
		void Update(T instance);
	}
}
