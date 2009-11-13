using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.WebSite.Helpers;
using Bagge.Seti.Common;
using Bagge.Seti.BusinessEntities.Reports;

namespace Bagge.Seti.WebSite.Reports
{
	[SecurizableCrud("Securizable_TicketHistory", typeof(TicketHistory), FunctionAction.List)]
	public partial class TicketHistory : FilteredReportPage<TicketHistoryReport>
	{
		protected override void OnLoad(EventArgs e)
		{
			if (!IsPostBack)
			{
				int? ticketId = Request.QueryString["TicketId"].ToInt32Nullable();

				if (ticketId.HasValue)
					_ticketId.Text = ticketId.ToString();

				_status.DataSource = IoCContainer.TicketStatusManager.FindAll();
				_status.DataBind();
			}
			base.OnLoad(e);


		}

		protected override Button FilterButton
		{
			get { return _filter; }
		}

		public override string GetFormattedColumnValue(int columnIndex, string value)
		{
			if (string.IsNullOrEmpty(value))
				return null;

			switch (columnIndex)
			{
				case 1:
					return string.Format("{0:d}", DateTime.Parse(value));
				default:
					return null;
			}
		}

		public override IList<Bagge.Seti.BusinessEntities.FilterPropertyValue> Filters
		{
			get
			{
				var filters = new List<FilterPropertyValue>();
				FilterHelper.AddCalendarFilterValue(_dateFrom, "DateFrom", FilterPropertyValueType.Equals, filters);
				FilterHelper.AddCalendarFilterValue(_dateTo, "DateTo", FilterPropertyValueType.Equals, filters);
				FilterHelper.AddTextBoxFilterValue<int>(_ticketId, "Ticket", FilterPropertyValueType.Equals, filters);
				FilterHelper.AddDropDownFilterValue<int>(_status, "Status", FilterPropertyValueType.Equals, filters);

				return filters;
			}
		}
	}
}
