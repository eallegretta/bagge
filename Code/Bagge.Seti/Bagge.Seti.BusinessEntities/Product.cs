
using System.Collections.Generic;
using Castle.ActiveRecord;
using Bagge.Seti.Security.BusinessEntities;
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Bagge.Seti.BusinessEntities.Validators;
using Bagge.Seti.BusinessEntities.Security;
namespace Bagge.Seti.BusinessEntities
{
	[ActiveRecord]
	[Serializable]
	[Securizable("Securizable_Product", typeof(Product))]
	public partial class Product : AuditablePrimaryKeyWithNameAndDescriptionDomainObject<Product, int>
	{
		[StringLengthValidator(1, 50, MessageTemplateResourceName = "Validators_Product_Name_Length", MessageTemplateResourceType = typeof(Product))]
		[RequiredStringValidator(MessageTemplateResourceName = "Validators_Product_Name_Required", MessageTemplateResourceType = typeof(Product))]
		[Securizable("Securizable_Product_Name", typeof(Product))]
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

		[Securizable("Securizable_Product_NameAndDescription", typeof(Product))]
		public string NameAndDescription
		{
			get
			{
				return Name + (string.IsNullOrEmpty(Description) ? " - " + Description : string.Empty);
			}
		}

		[Securizable("Securizable_Product_Providers", typeof(Product))]
		[HasMany(typeof(ProductProvider), Table = "ProductProvider", ColumnKey = "ProductId", Lazy = true,
			Cascade = ManyRelationCascadeEnum.SaveUpdate, Inverse = true)]
		public virtual IList<ProductProvider> Providers
		{
			get;
			set;
		}


		[Securizable("Securizable_Product_Tickets", typeof(Product))]
		[HasMany(typeof(ProductTicket), Table = "ProductTicket", ColumnKey = "ProductId", Lazy = true, 
			Inverse = true, Cascade = ManyRelationCascadeEnum.None)]
		public virtual IList<ProductTicket> Tickets
		{
			get;
			set;
		}
	}
}
