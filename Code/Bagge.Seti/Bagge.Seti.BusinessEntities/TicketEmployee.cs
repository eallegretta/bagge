
namespace Bagge.Seti.BusinessEntities
{
	public class TicketEmployee : PrimaryKeyDomainObject<TicketEmployee, int>
	{
		public Ticket Ticket
		{
			get;
			set;
		}

		public Employee Employee
		{
			get;
			set;
		}

	}
}
