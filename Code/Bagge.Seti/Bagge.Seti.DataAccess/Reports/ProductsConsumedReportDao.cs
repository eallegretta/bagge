using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities.Reports;
using Bagge.Seti.DataAccess.Contracts.Reports;
using Bagge.Seti.BusinessEntities;
using System.Data.SqlTypes;

namespace Bagge.Seti.DataAccess.Reports
{
	public class ProductsConsumedReportDao: BaseReportDao<ProductsConsumedReport>, IReportDao
	{

        #region IReportDao Members

        public BaseReport GetReport(IList<FilterPropertyValue> filters)
        {
            var filterDateFrom = filters.GetFilter("DateFrom");
            var filterDateTo = filters.GetFilter("DateTo");

			if (filterDateTo != null && filterDateTo.Value != null)
				filterDateTo.Value = GetDateToWithMaxTime((DateTime)filterDateTo.Value); 

            SqlDateTime dateFrom = filterDateFrom == null ? SqlDateTime.MinValue : GetSqlDateTime(filterDateFrom.Value as DateTime?, SqlDateTime.MinValue);
            SqlDateTime dateTo = filterDateTo == null ? SqlDateTime.MaxValue : GetSqlDateTime(filterDateTo.Value as DateTime?, SqlDateTime.MaxValue);

			
            return GetReport("ProductsConsumedReport", 
				dateFrom, dateTo,
				filters.GetFilterValue<string>("ProductName") + "%",
				filters.GetFilterValue<int>("EstimatedQuantityFrom", int.MinValue),
				filters.GetFilterValue<int>("EstimatedQuantityTo", int.MaxValue));
        }

        #endregion
    }
}