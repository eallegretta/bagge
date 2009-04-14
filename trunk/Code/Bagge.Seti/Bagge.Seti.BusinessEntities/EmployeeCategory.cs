
using Bagge.Seti.Security.BusinessEntities;
using Castle.ActiveRecord;
using System;
namespace Bagge.Seti.BusinessEntities
{
	[ActiveRecord]
	[Serializable]
	public class EmployeeCategory : AuditablePrimaryKeyWithNameAndDescriptionDomainObject<EmployeeCategory, int>
	{
	}
}
