using Castle.ActiveRecord;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.BusinessEntities.Validators;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System.Text.RegularExpressions;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System;
using Bagge.Seti.BusinessEntities.Security;

namespace Bagge.Seti.BusinessEntities
{
	[ActiveRecord]
	[Serializable]
	[Securizable("Securizable_Customer", typeof(Customer))]
	public partial class Customer : AuditablePrimaryKeyWithNameDomainObject<Customer, int>
	{
		[Property]
		[Securizable("Securizable_Customer_CUIT", typeof(Customer))]
		[ValidatorComposition(CompositionType.Or)]
		[RequiredStringValidator(Negated = true)]
		[RegexValidator("Validators_Customer_CUIT_Pattern", typeof(Customer), MessageTemplateResourceName = "Validators_Customer_CUIT", MessageTemplateResourceType = typeof(Customer))]
		public string CUIT 
		{ 
			get; 
			set; 
		}

		[Securizable("Securizable_Customer_District", typeof(Customer))]
		[BelongsTo("DistrictId")]
		public District District
		{
			get;
			set;
		}

		[Securizable("Securizable_Customer_FullAddress", typeof(Customer))]
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

		[Securizable("Securizable_Customer_Address", typeof(Customer))]
		[Property]
		public string Address
		{
			get;
			set;
		}

		[Securizable("Securizable_Customer_Floor", typeof(Customer))]
		[Property]
		public char? Floor
		{
			get;
			set;
		}

		[Securizable("Securizable_Customer_Apartment", typeof(Customer))]
		[Property("Departament")]
		public char? Apartment
		{
			get;
			set;
		}

		[Securizable("Securizable_Customer_ZipCode", typeof(Customer))]
		[Property]
		public string ZipCode
		{
			get;
			set;
		}

		[Securizable("Securizable_Customer_City", typeof(Customer))]
		[Property]
		public string City
		{
			get;
			set;
		}

		[Securizable("Securizable_Customer_Phone", typeof(Customer))]
		[Property]
		public string Phone
		{
			get;
			set;
		}


		[Securizable("Securizable_Customer_MobilePhone", typeof(Customer))]
		[Property]
		public string MobilePhone
		{
			get;
			set;
		}

		[Securizable("Securizable_Customer_Email", typeof(Customer))]
		[Property]
		[ValidatorComposition(CompositionType.Or)]
		[EmailValidator(Required = false, MessageTemplateResourceName = "Validators_Customer_Email", MessageTemplateResourceType = typeof(Customer))]
		public string Email
		{
			get;
			set;
		}

		[Securizable("Securizable_Customer_Subscription", typeof(Customer))]
		[Property]
		public bool Subscription
		{
			get;
			set;
		}
	}
}
