using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities.Reports;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.DataAccess.Contracts.Reports
{
	public interface IReportDao
	{
		IList<BaseReport> GetReport(IList<FilterPropertyValue> filters);
	}
}
