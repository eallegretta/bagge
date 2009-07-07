using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.DataAccess.Contracts;
using Bagge.Seti.BusinessEntities;
using Castle.ActiveRecord;
using NHibernate.Expression;
using Bagge.Seti.Extensions;


namespace Bagge.Seti.DataAccess.ActiveRecord
{
	public class SimpleFindGetDao<T, PK>: ISimpleFindGetDao<T, PK> where T: PrimaryKeyDomainObject<T, PK>
	{
		#region ISimpleFindGetDao<T,PK> Members

		public T[] FindAll()
		{
			if (typeof(T).IsOfType(typeof(PrimaryKeyWithNameDomainObject<T, PK>)))
				return ActiveRecordMediator<T>.FindAll(new Order[] { new Order("Name", true) });
			return ActiveRecordMediator<T>.FindAll();
		}

		#endregion

		#region IGetDao<T,PK> Members

		public T Get(PK id)
		{
			return ActiveRecordMediator<T>.FindByPrimaryKey(id, false);
		}

		#endregion
	}
}
