using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities.Reports;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.BusinessLogic.Contracts
{
	public interface IReportManager
	{
		IList<BaseReport> GetReport(string reportName, IList<FilterPropertyValue> filters);
	}
}
