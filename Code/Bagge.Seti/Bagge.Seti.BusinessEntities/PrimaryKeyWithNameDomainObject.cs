using System;
using Castle.ActiveRecord;
using Castle.Components.Validator;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.BusinessEntities.Validators;

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
		[RequiredStringValidator(Ruleset="Rules", MessageTemplateResourceName="Validators.PrimaryKeyDomainObject.Name", MessageTemplateResourceType = typeof(ISecurizable))]
		public virtual string Name { get ; set; }

		public override string ToString()
		{
			return Name;
		}
	}
}
