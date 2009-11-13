using System;
using System.Collections.Generic;
using Castle.ActiveRecord;
using Bagge.Seti.Security.BusinessEntities;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Bagge.Seti.BusinessEntities.Validators;
using Bagge.Seti.BusinessEntities.Security;

namespace Bagge.Seti.BusinessEntities
{
	[ActiveRecord]
	[Serializable]
	[Securizable("Securizable_Ticket", typeof(Ticket))]
	public partial class Ticket : AuditablePrimaryKeyDomainObject<Ticket, int>
	{
		[NotNullValidator(MessageTemplateResourceName = "Validators_Ticket_Customer", MessageTemplateResourceType = typeof(Ticket))]
		[BelongsTo("CustomerId")]
		[Securizable("Securizable_Ticket_Customer", typeof(Ticket))]
		public Customer Customer
		{
			get;
			set;
		}

		[Property]
		[Securizable("Securizable_Ticket_CreationDate", typeof(Ticket))]
		//[DateTimeValidator(MessageTemplateResourceName = "Validators_Date", MessageTemplateResourceType = typeof(Ticket))]
		public DateTime CreationDate
		{
			get;
			set;
		}

		[Property]
		[Securizable("Securizable_Ticket_ExecutionDate", typeof(Ticket))]
		[DateTimeValidator(MessageTemplateResourceName = "Validators_Date", MessageTemplateResourceType = typeof(Ticket))]
		public DateTime? ExecutionDateTime
		{
			get;
			set;
		}


		[Property]
		[Securizable("Securizable_Ticket_EstimatedDuration", typeof(Ticket))]
		public decimal EstimatedDuration
		{
			get;
			set;
		}

		[Property]
		[Securizable("Securizable_Ticket_RealDuration", typeof(Ticket))]
		public decimal? RealDuration
		{
			get;
			set;
		}

		[RequiredStringValidator(MessageTemplateResourceName = "Validators_Ticket_Description", MessageTemplateResourceType = typeof(Ticket))]
		[Property]
		[Securizable("Securizable_Ticket_Description", typeof(Ticket))]
		public string Description
		{
			get;
			set;
		}

		[Property]
		[Securizable("Securizable_Ticket_Budget", typeof(Ticket))]
		public decimal? Budget
		{
			get;
			set;
		}

		[Securizable("Securizable_Ticket_Employees", typeof(Ticket))]
		[HasAndBelongsToMany(Table = "TicketEmployee", ColumnKey = "TicketId", ColumnRef = "EmployeeId", Lazy = true)]
		public virtual IList<Employee> Employees
		{
			get;
			set;
		}

		[Securizable("Securizable_Ticket_Products", typeof(Ticket))]
		[HasMany(typeof(ProductTicket), Table = "ProductTicket", ColumnKey = "TicketId", Lazy = true, Inverse = false,  Cascade = ManyRelationCascadeEnum.SaveUpdate)]
		public virtual IList<ProductTicket> Products
		{
			get;
			set;
		}

		[Securizable("Securizable_Ticket_Creator", typeof(Ticket))]
		[NotNullValidator(MessageTemplateResourceName = "Validators_Ticket_Creator", MessageTemplateResourceType = typeof(Ticket))]
		[BelongsTo("EmployeeCreatorId", Update = false)]
		public Employee Creator
		{
			get;
			set;
		}

		[Securizable("Securizable_Ticket_Status", typeof(Ticket))]
		[NotNullValidator(MessageTemplateResourceName = "Validators_Ticket_Status", MessageTemplateResourceType = typeof(Ticket))]
		[BelongsTo("TicketStatusId")]
		public TicketStatus Status
		{
			get;
			set;
		}

		[Securizable("Securizable_Ticket_Notes", typeof(Ticket))]
		[Property]
		public string Notes
		{
			get;
			set;
		}

		public bool IsCanceled
		{
			get
			{
				if (Status == null)
					return false;

				return Status.In(TicketStatusEnum.Canceled, TicketStatusEnum.CanceledByCustomer, TicketStatusEnum.CanceledBySystem);
			}
		}

		public bool IsClosed
		{
			get
			{
				if (Status == null)
					return false;

				return Status.Equals(TicketStatusEnum.Closed) || IsCanceled; 
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
