using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;

namespace Bagge.Seti.BusinessEntities
{
	[ActiveRecord]
	public class TicketHistory: PrimaryKeyDomainObject<TicketHistory, int>
	{
		public TicketHistory()
		{

		}
		public TicketHistory(Ticket ticket, Employee employee)
		{
			Ticket = ticket;
			Status = ticket.Status;
			DateTime = DateTime.Now;
			Employee = employee;
			Notes = ticket.Notes;
		}

		[BelongsTo("TicketId")]
		public virtual Ticket Ticket
		{
			get;
			set;
		}

		[BelongsTo("TicketStatusId")]
		public virtual TicketStatus Status
		{
			get;
			set;
		}

		[Property]
		public virtual DateTime DateTime
		{
			get;
			set;
		}

		[BelongsTo("EmployeeId")]
		public virtual Employee Employee
		{
			get;
			set;
		}

		[Property]
		public virtual string Notes
		{
			get;
			set;
		}
	}
}
