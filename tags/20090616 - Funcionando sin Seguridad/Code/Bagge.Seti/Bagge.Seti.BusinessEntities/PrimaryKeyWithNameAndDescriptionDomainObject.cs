using System;
using Castle.ActiveRecord;

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
		public virtual string Description { get; set;  }
	}
}
