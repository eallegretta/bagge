
using Bagge.Seti.Security.BusinessEntities;
using Castle.ActiveRecord;
namespace Bagge.Seti.BusinessEntities
{
	[ActiveRecord]
	public class EmployeeCategory : AuditablePrimaryKeyWithNameAndDescriptionDomainObject<EmployeeCategory, int>
	{
	}
}
