using System;
using System.Collections.Generic;
using Castle.ActiveRecord;
using Bagge.Seti.Security.BusinessEntities;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Bagge.Seti.BusinessEntities.Validators;

namespace Bagge.Seti.BusinessEntities
{
	[ActiveRecord]
	[Serializable]
	public class Ticket : AuditablePrimaryKeyDomainObject<Ticket, int>
	{
		[NotNullValidator(MessageTemplateResourceName = "Validators_Ticket_Customer", MessageTemplateResourceType = typeof(Ticket))]
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

		[Property("CustomerETA")]
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

		[RequiredStringValidator(MessageTemplateResourceName = "Validators_Ticket_Description", MessageTemplateResourceType = typeof(Ticket))]
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


		[HasAndBelongsToMany(Table = "TicketEmployee", ColumnKey = "TicketId", ColumnRef = "EmployeeId", Lazy = true)]
		public virtual IList<Employee> Employees
		{
			get;
			set;
		}

		[HasMany(typeof(ProductTicket), Table = "ProductTicket", ColumnKey = "TicketId", Lazy = true, Inverse = false,  Cascade = ManyRelationCascadeEnum.SaveUpdate)]
		public virtual IList<ProductTicket> Products
		{
			get;
			set;
		}

		[NotNullValidator(MessageTemplateResourceName = "Validators_Ticket_Creator", MessageTemplateResourceType = typeof(Ticket))]
		[BelongsTo("EmployeeCreatorId", Update = false)]
		public Employee Creator
		{
			get;
			set;
		}

		[NotNullValidator(MessageTemplateResourceName = "Validators_Ticket_Status", MessageTemplateResourceType = typeof(Ticket))]
		[BelongsTo("TicketStatusId")]
		public TicketStatus Status
		{
			get;
			set;
		}

		public bool IsClosed
		{
			get
			{
				if (Status == null)
					return false;

				return Status.Equals(TicketStatusEnum.Closed) || Status.Equals(TicketStatusEnum.Canceled);
			}
		}

		public static bool CheckTicketsAllClosed(IList<Ticket> tickets)
		{
			foreach (var ticket in tickets)
			{
				if (!ticket.IsClosed)
					return false;
			}
			return true;
		}
	}
}
