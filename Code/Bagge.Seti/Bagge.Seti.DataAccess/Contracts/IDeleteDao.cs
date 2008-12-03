using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.DataAccess.Contracts
{
	public interface IDeleteDao<T, PK> where T : PrimaryKeyDomainObject<T, PK>
	{
		void Delete(PK id);
	}
}
