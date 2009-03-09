using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.BusinessLogic.Properties;
using Bagge.Seti.DesignByContract;
using Bagge.Seti.DataAccess.Contracts;

namespace Bagge.Seti.BusinessLogic
{
	public class AuditableGenericManager<T, PK>: GenericManager<T, PK> where T: AuditablePrimaryKeyDomainObject<T, PK>
	{
		public AuditableGenericManager(IDao<T, PK> dao)
			: base(dao)
		{
		}

		public override void Delete(PK id)
		{
			Check.Require(!id.Equals(default(PK)), string.Format(Resources.IdCannotBeDefault, default(PK)));

			T instance = Get(id);

			instance.Deleted = true;

			Update(instance);
		}
	}
}
