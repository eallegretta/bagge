using System;
using System.Collections.Generic;
using Castle.ActiveRecord;
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.BusinessEntities
{
	[ActiveRecord]
	public class Ticket : AuditablePrimaryKeyDomainObject<Ticket, int>
	{
		[BelongsTo("CustomerId")]
		public Customer Customer
		{
			get;
			set;
		}

		[Property]
		public DateTime CreationDate
		{
			get;
			set;
		}

		[Property]
		public DateTime? ExecutionDate
		{
			get;
			set;
		}

		[Property]
		public DateTime CustomerArrival
		{
			get;
			set;
		}

		[Property]
		public decimal EstimatedDuration
		{
			get;
			set;
		}

		[Property]
		public decimal? RealDuration
		{
			get;
			set;
		}

		[Property]
		public string Description
		{
			get;
			set;
		}

		[Property]
		public decimal? Budget
		{
			get;
			set;
		}


		[HasAndBelongsToMany(typeof(TicketEmployee), ColumnKey = "TicketId", ColumnRef = "EmployeeId", Lazy = true)]
		public virtual IList<TicketEmployee> Employees
		{
			get;
			set;
		}

		[HasAndBelongsToMany(typeof(ProductTicket), ColumnKey = "TicketId", ColumnRef = "ProductId", Lazy = true)]
		public virtual IList<ProductTicket> Products
		{
			get;
			set;
		}

		[BelongsTo("EmployeeCreatorId")]
		public Employee Creator
		{
			get;
			set;
		}

		[BelongsTo("TicketStatusId")]
		public TicketStatus Status
		{
			get;
			set;
		}
	}
}
