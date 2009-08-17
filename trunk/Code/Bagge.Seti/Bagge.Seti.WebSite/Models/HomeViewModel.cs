using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.WebSite.Models
{
	public class HomeViewModel
	{
		public HomeViewModel(Ticket ticket)
		{
			Ticket = ticket;
		}

		public string CustomerName
		{
			get { return Ticket.Customer.Name; }
		}

		public string CustomerAddress
		{
			get
			{
				if (Ticket.Customer.District.CountryState.Name.Equals("Capital Federal", StringComparison.InvariantCultureIgnoreCase))
					return string.Format("{0}, {1}", Ticket.Customer.Address, Ticket.Customer.District.CountryState);
				else
					return string.Format("{0}, {1}, {2}", Ticket.Customer.Address, Ticket.Customer.District, Ticket.Customer.District.CountryState);
			}
		}

		public string TicketExecutionDateTime
		{
			get
			{
				return string.Format("{0:d} {0:HH:mm}", Ticket.ExecutionDateTime);
			}
		}

		public string MapDestination
		{
			get
			{
				return CustomerAddress + ", Argentina";
			}
		}

		public string TicketStatus
		{
			get
			{
				return ((TicketStatusEnum)Ticket.Status.Id).ToString();
			}
		}

		public int TicketId
		{
			get { return Ticket.Id; }
		}

		public Ticket Ticket { get; private set; }
	}
}
