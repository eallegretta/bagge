using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.DataAccess.Contracts;

namespace Bagge.Seti.DataAccess.ActiveRecord
{
	public class ProviderDao: AuditableGenericDao<Provider, int>, IProviderDao
	{

		public override Provider[] FindAllByProperties(IList<FilterPropertyValue> filter)
		{
			return base.FindAllByProperties(filter);
		}

		public override Provider[] FindAllByPropertiesOrdered(IList<FilterPropertyValue> filter, string orderBy, bool ascending)
		{
			return base.FindAllByPropertiesOrdered(filter, orderBy, ascending);
		}

		public override Provider[] SlicedFindAllByProperties(int startIndex, int pageSize, IList<FilterPropertyValue> filter)
		{
			return base.SlicedFindAllByProperties(startIndex, pageSize, filter);
		}

		public override Provider[] SlicedFindAllByPropertiesOrdered(int startIndex, int pageSize, IList<FilterPropertyValue> filter, string orderBy, bool ascending)
		{
			return base.SlicedFindAllByPropertiesOrdered(startIndex, pageSize, filter, orderBy, ascending);
		}

	}
}
