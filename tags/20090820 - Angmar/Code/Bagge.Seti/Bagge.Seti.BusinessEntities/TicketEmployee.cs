
using Castle.ActiveRecord;
using System;
namespace Bagge.Seti.BusinessEntities
{
	[ActiveRecord]
	[Serializable]
	public partial class TicketEmployee : PrimaryKeyDomainObject<TicketEmployee, int>
	{
		[BelongsTo("TicketId")]
		public Ticket Ticket
		{
			get;
			set;
		}

		[BelongsTo("EmployeeId")]
		public Employee Employee
		{
			get;
			set;
		}

	}
}
