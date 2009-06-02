using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.WebSite.Views
{
	public interface ITicketEditorView: IEditorView<int>
	{
		Employee[] Employees { set; }

		int[] AssignedEmployeeIds { get; }
		
		Customer[] Customers { set; }
		
		int SelectedCustomerId { get; set; }

		TicketStatus[] TicketStatus { set; }

		TicketStatusEnum SelectedTicketStatus { get; set;  }

		ProductTicket[] Products { set; }

		int[] AssignedProductIds { get; }
	}
}
