
using Castle.ActiveRecord;
using System;
namespace Bagge.Seti.BusinessEntities
{
	[ActiveRecord]
	[Serializable]
	public class TicketStatus : PrimaryKeyWithNameAndDescriptionDomainObject<TicketStatus, int>
	{
	}
}
