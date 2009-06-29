
using Castle.ActiveRecord;
using System;
using Bagge.Seti.BusinessEntities.Security;
namespace Bagge.Seti.BusinessEntities
{
	[Serializable]
	[ActiveRecord]
	[Securizable("Securizable_ProviderCalification", typeof(ProviderCalification))]
	public partial class ProviderCalification : PrimaryKeyWithNameAndDescriptionDomainObject<ProviderCalification, int>
	{
	}
}
