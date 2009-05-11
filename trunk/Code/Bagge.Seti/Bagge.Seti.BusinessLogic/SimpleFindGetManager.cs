using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.DataAccess.Contracts;

namespace Bagge.Seti.BusinessLogic
{
	public class SimpleFindGetManager<T, PK>: ISimpleFindGetManager<T, PK> where T: PrimaryKeyDomainObject<T, PK>
	{
		ISimpleFindGetDao<T, PK> _dao;

		public SimpleFindGetManager(ISimpleFindGetDao<T, PK> dao)
		{
			_dao = dao;
		}

		#region ISimpleFindGetManager<T,PK> Members

		public T[] FindAll()
		{
			return _dao.FindAll();
		}

		#endregion

		#region IGetManager<T,PK> Members

		public T Get(PK id)
		{
			return _dao.Get(id);
		}

		#endregion
	}
}
