using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.BusinessEntities.Reports;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.DataAccess.Contracts.Reports;
using System.Collections.Specialized;
using Bagge.Seti.DesignByContract;

namespace Bagge.Seti.BusinessLogic
{
	public partial class ReportManager : IReportManager
	{
		IDictionary<Type, IReportDao> _reportDaos;

		public ReportManager(IDictionary<Type, IReportDao> reportDaos)
		{
			_reportDaos = reportDaos;
		}

		public BaseReport GetReport<T>(IList<FilterPropertyValue> filters)
		{
			Check.Require(typeof(BaseReport).IsAssignableFrom(typeof(T)));

			Type type = typeof(T);
			if (_reportDaos.ContainsKey(type))
				return _reportDaos[type].GetReport(filters);
			return null;
		}

	}
}
