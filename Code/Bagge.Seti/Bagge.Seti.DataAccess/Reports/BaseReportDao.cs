using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities.Reports;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.SqlTypes;

namespace Bagge.Seti.DataAccess.Reports
{
	public abstract class BaseReportDao<T> where T: BaseReport
	{
		protected T GetReport(string spName, params object[] parameters)
		{
			T report = Activator.CreateInstance<T>();

			var database = DatabaseFactory.CreateDatabase(Constants.DEFAULT_CONNECTION_STRING_NAME);
			using (DataSet ds = database.ExecuteDataSet(spName, parameters))
			{
				if (ds.Tables.Count > 0)
					report.ReportData = ds.Tables[0].Copy();
			}

			return report;
		}

		protected static DateTime GetDateToWithMaxTime(DateTime dateTo)
		{
			return new DateTime(dateTo.Year, dateTo.Month, dateTo.Day, 23, 59, 59);
		}

		protected static SqlDateTime GetSqlDateTime(DateTime? dateTime, SqlDateTime defaultValue)
		{
			try
			{
				if (dateTime == null)
					return defaultValue;
				return new SqlDateTime(dateTime.Value);
			}
			catch(Exception)
			{
				return defaultValue;
			}
		}
	}
}
