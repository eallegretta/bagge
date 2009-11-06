using System.Collections.Generic;
using Castle.ActiveRecord;
using Bagge.Seti.Security.BusinessEntities;
using System;
using Bagge.Seti.BusinessEntities.Validators;
using Bagge.Seti.BusinessEntities.Security;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace Bagge.Seti.BusinessEntities
{
	[ActiveRecord]
	[Serializable]
	[Securizable("Securizable_Provider", typeof(Provider))]
	public partial class Provider : AuditablePrimaryKeyWithNameDomainObject<Provider, int>
	{

		public string NameAndCUIT
		{
			get
			{
				string nameCuit = Name;
				if (!string.IsNullOrEmpty(CUIT))
					nameCuit += " (" + CUIT + ")";
				return nameCuit;
			}
		}

		[BelongsTo("CalificationId")]
		[Securizable("Securizable_Provider_Calification", typeof(Provider))]
		public ProviderCalification Calification
		{
			get;
			set;
		}

		[BelongsTo("DistrictId")]
		[Securizable("Securizable_Provider_District", typeof(Provider))]
		public District District
		{
			get;
			set;
		}


		[Property]
		[Securizable("Securizable_Provider_CUIT", typeof(Provider))]
		[ValidatorComposition(CompositionType.Or)]
		[RequiredStringValidator(Negated = true)]
		[RegexValidator("Validators_Customer_CUIT_Pattern", typeof(Provider), MessageTemplateResourceName = "Validators_Customer_CUIT", MessageTemplateResourceType = typeof(Provider))]
		public string CUIT
		{
			get;
			set;
		}

		[Property]
		[Securizable("Securizable_Provider_Company", typeof(Provider))]
		public string Company
		{
			get;
			set;
		}

		[Securizable("Securizable_Provider_FullAddress", typeof(Provider))]
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
		[Securizable("Securizable_Provider_Floor", typeof(Provider))]
		public char? Floor
		{
			get;
			set;
		}

		[Property("Departament")]
		[Securizable("Securizable_Provider_Apartment", typeof(Provider))]
		public char? Apartment
		{
			get;
			set;
		}

		[Property]
		[Securizable("Securizable_Provider_ZipCode", typeof(Provider))]
		public string ZipCode
		{
			get;
			set;
		}


		[RequiredStringValidator(MessageTemplateResourceName = "Validators_Provider_PrimaryPhone_Required", MessageTemplateResourceType = typeof(Provider))]
		[Property("Phone")]
		[Securizable("Securizable_Provider_PrimaryPhone", typeof(Provider))]
		public string PrimaryPhone
		{
			get;
			set;
		}

		[RequiredStringValidator(MessageTemplateResourceName = "Validators_Provider_SecondaryPhone_Required", MessageTemplateResourceType = typeof(Provider))]
		[Property]
		[Securizable("Securizable_Provider_SecondaryPhone", typeof(Provider))]
		public string SecondaryPhone
		{
			get;
			set;
		}

		[Property]
		[Securizable("Securizable_Provider_FaxPhone", typeof(Provider))]
		public string FaxPhone
		{
			get;
			set;
		}

		[EmailValidator(Required = false, MessageTemplateResourceName = "Validators_Provider_Email", MessageTemplateResourceType = typeof(Provider))]
		[Property]
		[Securizable("Securizable_Provider_Email", typeof(Provider))]
		public string Email
		{
			get;
			set;
		}

		[UrlValidator(MessageTemplateResourceName = "Validators_Provider_Website", MessageTemplateResourceType = typeof(Provider))]
		[Property]
		[Securizable("Securizable_Provider_WebSite", typeof(Provider))]
		public string WebSite
		{
			get;
			set;
		}

		[RequiredStringValidator(MessageTemplateResourceName = "Validators_Provider_ContactName_Required", MessageTemplateResourceType = typeof(Provider))]
		[Property]
		[Securizable("Securizable_Provider_ContactName", typeof(Provider))]
		public string ContactName
		{
			get;
			set;
		}

		[HasMany(typeof(ProductProvider), Table= "ProductProvider", ColumnKey = "ProviderId", Lazy = true, 
			Cascade = ManyRelationCascadeEnum.SaveUpdate, Inverse = true)]
		public virtual IList<ProductProvider> Products
		{
			get; set;
		}


	}
}
