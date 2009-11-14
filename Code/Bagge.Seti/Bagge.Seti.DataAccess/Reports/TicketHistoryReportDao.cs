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
	public class TicketHistoryReportDao : BaseReportDao<TicketHistoryReport>, IReportDao
	{
		#region IReportDao Members

		public BaseReport GetReport(IList<FilterPropertyValue> filters)
		{
			var filterDateFrom = filters.GetFilter("DateFrom");
			var filterDateTo = filters.GetFilter("DateTo");
			var filterTicket = filters.GetFilter("Ticket");
			var filterStatus = filters.GetFilter("Status");

			if (filterDateTo != null && filterDateTo.Value != null)
				filterDateTo.Value = GetDateToWithMaxTime((DateTime)filterDateTo.Value); 

			SqlDateTime dateFrom = filterDateFrom == null ? SqlDateTime.MinValue : GetSqlDateTime(filterDateFrom.Value as DateTime?, SqlDateTime.MinValue);
			SqlDateTime dateTo = filterDateTo == null ? SqlDateTime.MaxValue : GetSqlDateTime(filterDateTo.Value as DateTime?, SqlDateTime.MaxValue);
			int ticketIdFrom = filterTicket == null ? int.MinValue : (int)filterTicket.Value;
			int ticketIdTo = filterTicket == null ? int.MaxValue : (int)filterTicket.Value;
			int statusIdFrom = filterStatus == null ? int.MinValue : (int)filterStatus.Value;
			int statusIdTo = filterStatus == null ? int.MaxValue : (int)filterStatus.Value;

			return GetReport("TicketHistoryReport", 
				ticketIdFrom, ticketIdTo,
				statusIdFrom, statusIdTo,
				dateFrom, dateTo);
		}

		#endregion
	}
}
