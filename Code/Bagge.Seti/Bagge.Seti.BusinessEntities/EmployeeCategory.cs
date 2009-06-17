
using Bagge.Seti.Security.BusinessEntities;
using Castle.ActiveRecord;
using System;
using Bagge.Seti.BusinessEntities.Properties;
using Bagge.Seti.BusinessEntities.Security;
namespace Bagge.Seti.BusinessEntities
{
	[ActiveRecord]
	[Serializable]
	[Securizable("Securizable_EmployeeCategory", typeof(EmployeeCategory))]
	public partial class EmployeeCategory : AuditablePrimaryKeyWithNameAndDescriptionDomainObject<EmployeeCategory, int>
	{
		public static readonly int TechnicianId = Settings.Default.TechnicianCategoryId;
	}
}
