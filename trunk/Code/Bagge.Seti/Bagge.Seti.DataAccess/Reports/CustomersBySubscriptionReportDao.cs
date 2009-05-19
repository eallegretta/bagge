﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities.Reports;
using Bagge.Seti.DataAccess.Contracts.Reports;
using Bagge.Seti.BusinessEntities;
using Eaa.Framework.Data;

namespace Bagge.Seti.DataAccess.Reports
{
	public class CustomersBySubscriptionReportDao: DBRepository<CustomersBySubscriptionReport>, IReportDao
	{
		public CustomersBySubscriptionReportDao(): base(Constants.DEFAULT_CONNECTION_STRING_NAME)
		{
		}

		#region IReportDao<CustomersBySubscriptionReport> Members

		public IList<BaseReport> GetReport(IList<FilterPropertyValue> filters)
		{
			throw new NotImplementedException();
		}

		#endregion

		protected override Eaa.Framework.Collections.Adapters.PropertyMappingCollection Mappings
		{
			get { throw new NotImplementedException(); }
		}
	}
}
