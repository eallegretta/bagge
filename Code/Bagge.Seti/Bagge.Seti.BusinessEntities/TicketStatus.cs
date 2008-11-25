
using Castle.ActiveRecord;
namespace Bagge.Seti.BusinessEntities
{
	[ActiveRecord]
	public class TicketStatus : PrimaryKeyWithNameAndDescriptionDomainObject<TicketStatus, int>
	{
	}
}
