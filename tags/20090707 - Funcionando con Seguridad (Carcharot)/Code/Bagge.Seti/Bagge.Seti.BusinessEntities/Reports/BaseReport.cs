using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Bagge.Seti.BusinessEntities.Reports
{
	public abstract class BaseReport
	{
		public DataTable ReportData { get; set; }
	}
}
