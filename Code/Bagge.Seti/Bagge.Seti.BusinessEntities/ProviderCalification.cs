
using Castle.ActiveRecord;
using System;
namespace Bagge.Seti.BusinessEntities
{
	[Serializable]
	[ActiveRecord]
	public class ProviderCalification : PrimaryKeyWithNameAndDescriptionDomainObject<ProviderCalification, int>
	{
	}
}
