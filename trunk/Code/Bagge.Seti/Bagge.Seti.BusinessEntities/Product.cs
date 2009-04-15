
using System.Collections.Generic;
using Castle.ActiveRecord;
using Bagge.Seti.Security.BusinessEntities;
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Bagge.Seti.BusinessEntities.Validators;
namespace Bagge.Seti.BusinessEntities
{
	[ActiveRecord]
	[Serializable]
	public class Product : AuditablePrimaryKeyWithNameAndDescriptionDomainObject<Product, int>
	{
		[StringLengthValidator(1, 50, MessageTemplateResourceName = "Validators_Product_Name_Length", MessageTemplateResourceType = typeof(Product))]
		[RequiredStringValidator(MessageTemplateResourceName = "Validators_Product_Name_Required", MessageTemplateResourceType = typeof(Product))]
		public override string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}

		public string NameAndDescription
		{
			get
			{
				return Name + (string.IsNullOrEmpty(Description) ? " - " + Description : string.Empty);
			}
		}

		[HasMany(typeof(ProductProvider), Table = "ProductProvider", ColumnKey = "ProductId", Lazy = true,
			Cascade = ManyRelationCascadeEnum.SaveUpdate, Inverse = true)]
		public virtual IList<ProductProvider> Providers
		{
			get;
			set;
		}


		[HasMany(typeof(ProductTicket), Table = "ProductTicket", ColumnKey = "ProductId", Lazy = true, 
			Inverse = true, Cascade = ManyRelationCascadeEnum.None)]
		public virtual IList<ProductTicket> Tickets
		{
			get;
			set;
		}
	}
}
