
using Castle.ActiveRecord;
namespace Bagge.Seti.BusinessEntities
{
	[ActiveRecord]
	public class TicketEmployee : PrimaryKeyDomainObject<TicketEmployee, int>
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
