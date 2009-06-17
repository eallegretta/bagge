using System;
using Castle.ActiveRecord;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.BusinessEntities.Security;

namespace Bagge.Seti.BusinessEntities
{
	[Serializable]
	public class PrimaryKeyWithNameAndDescriptionDomainObject<T, PK>: PrimaryKeyWithNameDomainObject<T, PK>
	{
		public PrimaryKeyWithNameAndDescriptionDomainObject(): base(default(PK), string.Empty)
		{ 
		}
		public PrimaryKeyWithNameAndDescriptionDomainObject(PK id, string name, string description)
		{
			Id = id;
			Name = name;
			Description = description;
		}

		[Property]
		[Securizable("Securizable_PrimaryKeyWithNameAndDescriptionDomainObject_Description", typeof(ISecurizable))]
		public virtual string Description { get; set;  }
	}
}
