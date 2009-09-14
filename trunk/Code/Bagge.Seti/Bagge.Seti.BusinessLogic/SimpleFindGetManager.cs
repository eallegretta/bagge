using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.DataAccess.Contracts;
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.BusinessLogic
{
	[Securizable("Securizable_SimpleFindGetManager", typeof(RandomPassword))]
	public class SimpleFindGetManager<T, PK>: ISimpleFindGetManager<T, PK> where T: PrimaryKeyDomainObject<T, PK>
	{
		ISimpleFindGetDao<T, PK> _dao;

		public SimpleFindGetManager(ISimpleFindGetDao<T, PK> dao)
		{
			_dao = dao;
		}

		#region ISimpleFindGetManager<T,PK> Members

		[SecurizableCrud("Securizable_SimpleFindGetManager_FindAll", typeof(RandomPassword), FunctionAction.List)]
		public T[] FindAll()
		{
			return _dao.FindAll();
		}

		#endregion

		#region IGetManager<T,PK> Members

		[SecurizableCrud("Securizable_SimpleFindGetManager_Get", typeof(RandomPassword), FunctionAction.List)]
		public T Get(PK id)
		{
			return _dao.Get(id);
		}

		#endregion
	}
}
