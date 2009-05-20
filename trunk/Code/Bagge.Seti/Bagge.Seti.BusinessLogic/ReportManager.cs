using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.BusinessEntities.Reports;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.DataAccess.Contracts.Reports;
using System.Collections.Specialized;

namespace Bagge.Seti.BusinessLogic
{
	public class ReportManager: IReportManager
	{
		IDictionary<string, IReportDao> _reportDaos;

		public ReportManager(IDictionary<string, IReportDao> reportDaos)
		{
			_reportDaos = reportDaos;
		}

		public IList<BaseReport> GetReport(string reportName, IList<FilterPropertyValue> filters)
		{
			if (_reportDaos.ContainsKey(reportName))
				return _reportDaos[reportName].GetReport(filters);
			return null;
		}

	}
}
