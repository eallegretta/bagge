using System;
using Castle.ActiveRecord;
using Castle.Components.Validator;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.BusinessEntities.Validators;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

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
		[StringLengthValidator(1, 50, MessageTemplateResourceName = "Validators_PrimaryKeyDomainObject_Name_Length", MessageTemplateResourceType = typeof(ISecurizable))]
		[RequiredStringValidator(MessageTemplateResourceName = "Validators_PrimaryKeyDomainObject_Name_Required", MessageTemplateResourceType = typeof(ISecurizable))]
		public virtual string Name { get ; set; }

		public override string ToString()
		{
			return Name;
		}
	}
}
