using System;
using System.Collections.Generic;

namespace Bagge.Seti.BusinessEntities
{
	public class Ticket : PrimaryKeyDomainObject<Ticket, int>
	{
		public Customer Customer
		{
			get;
			set;
		}

		public DateTime CreationDate
		{
			get;
			set;
		}

		public DateTime? ExecutionDate
		{
			get;
			set;
		}

		public DateTime CustomerArrival
		{
			get;
			set;
		}

		public decimal EstimatedDuration
		{
			get;
			set;
		}

		public decimal? RealDuration
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		public decimal? Budget
		{
			get;
			set;
		}


		public virtual IList<TicketEmployee> Employees
		{
			get;
			set;
		}

		public virtual IList<ProductTicket> Products
		{
			get;
			set;
		}

		public TicketStatus Status
		{
			get;
			set;
		}
	}
}
