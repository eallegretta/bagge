using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.WebSite.Views
{
	public interface ITicketEditorView: IEditorView<int>
	{
		Employee[] Technicians { set; }

		int[] AssignedTechniciansIds { get; set;  }
		
		Customer[] Customers { set; }
		
		int SelectedCustomerId { get; set; }

		TicketStatus[] TicketStatus { set; }

		TicketStatusEnum SelectedTicketStatus { get; set;  }

		ProductTicket[] Products { set; get; }

		bool ShowApproveButton { set; }
		bool ShowCloseButton { set; }

		bool IsUpdateProgress { get; }

		string EmailUrl { get; }
	}
}
