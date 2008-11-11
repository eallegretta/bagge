using System;
using Castle.ActiveRecord;

namespace Bagge.Seti.BusinessEntities
{
	[Serializable]
	public class PrimaryKeyWithNameDomainObject<T, PK>: PrimaryKeyDomainObject<T, PK>
	{
		public PrimaryKeyWithNameDomainObject(): base(default(PK))
		{ 
		}
		public PrimaryKeyWithNameDomainObject(PK id, string name)
		{
			Id = id;
			Name = name;
		}

		[Property]
		public virtual string Name { get ; set; }

		public override string ToString()
		{
			return Name;
		}
	}
}
