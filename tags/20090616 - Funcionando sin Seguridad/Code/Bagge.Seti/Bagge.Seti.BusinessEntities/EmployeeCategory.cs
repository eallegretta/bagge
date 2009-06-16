
using Bagge.Seti.Security.BusinessEntities;
using Castle.ActiveRecord;
using System;
using Bagge.Seti.BusinessEntities.Properties;
namespace Bagge.Seti.BusinessEntities
{
	[ActiveRecord]
	[Serializable]
	public class EmployeeCategory : AuditablePrimaryKeyWithNameAndDescriptionDomainObject<EmployeeCategory, int>
	{
		public static readonly int TechnicianId = Settings.Default.TechnicianCategoryId;
	}
}
