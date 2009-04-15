using System.Collections.Generic;
using Castle.ActiveRecord;
using Bagge.Seti.Security.BusinessEntities;
using System;
using Bagge.Seti.BusinessEntities.Validators;

namespace Bagge.Seti.BusinessEntities
{
	[ActiveRecord]
	[Serializable]
	public class Provider : AuditablePrimaryKeyWithNameDomainObject<Provider, int>
	{
		[BelongsTo("CalificationId")]
		public ProviderCalification Calification
		{
			get;
			set;
		}

		[BelongsTo("DistrictId")]
		public District District
		{
			get;
			set;
		}


		[Property]
		public string CUIT
		{
			get;
			set;
		}

		[Property]
		public string Company
		{
			get;
			set;
		}

		public string FullAddress
		{
			get
			{
				string address = "";
				if (!string.IsNullOrEmpty(Address))
					address += Address + " ";
				if (Floor.HasValue && Floor.Value != '\0')
					address += Floor.Value;
				if (Apartment.HasValue && Apartment.Value != '\0')
					address += Apartment.Value;

				return address;
			}
		}

		[Property]
		public string Address
		{
			get;
			set;
		}

		[Property]
		public char? Floor
		{
			get;
			set;
		}

		[Property("Departament")]
		public char? Apartment
		{
			get;
			set;
		}

		[Property]
		public string ZipCode
		{
			get;
			set;
		}


		[RequiredStringValidator(MessageTemplateResourceName = "Validators_Provider_PrimaryPhone_Required", MessageTemplateResourceType = typeof(Provider))]
		[Property("Phone")]
		public string PrimaryPhone
		{
			get;
			set;
		}

		[RequiredStringValidator(MessageTemplateResourceName = "Validators_Provider_SecondaryPhone_Required", MessageTemplateResourceType = typeof(Provider))]
		[Property]
		public string SecondaryPhone
		{
			get;
			set;
		}

		[Property]
		public string FaxPhone
		{
			get;
			set;
		}

		[EmailValidator(Required = false, MessageTemplateResourceName = "Validators_Provider_Email", MessageTemplateResourceType = typeof(Provider))]
		[Property]
		public string Email
		{
			get;
			set;
		}

		[UrlValidator(MessageTemplateResourceName = "Validators_Provider_Website", MessageTemplateResourceType = typeof(Provider))]
		[Property]
		public string WebSite
		{
			get;
			set;
		}

		[RequiredStringValidator(MessageTemplateResourceName = "Validators_Provider_ContactName_Required", MessageTemplateResourceType = typeof(Provider))]
		[Property]
		public string ContactName
		{
			get;
			set;
		}

		[HasAndBelongsToMany(typeof(ProductProvider), Table= "ProductProvider", ColumnKey = "ProviderId", ColumnRef = "ProductId", 
			Lazy = true, Cascade = ManyRelationCascadeEnum.AllDeleteOrphan, Inverse = true)]
		public virtual IList<ProductProvider> Products
		{
			get; set;
		}


	}
}
