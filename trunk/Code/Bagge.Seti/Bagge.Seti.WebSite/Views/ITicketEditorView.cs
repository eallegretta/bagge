using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.WebSite.Views
{
	public interface ITicketEditorView: IEditorView<int>
	{
		Employee[] Employees
		{
			set;
		}

		int SelectedEmployeeId
		{
			get;
		}

		Customer[] Customers
		{
			set;
		}

		int SelectedCustomerId
		{
			get;
		}

		TicketStatus[] TicketStatus
		{
			set;
		}

		int SelectedTicketStatus
		{
			get;
		}

		Product[] Products
		{
			set;
		}

		int SelectedProductId
		{
			get;
		}
	}
}
