using Castle.ActiveRecord;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.BusinessEntities.Validators;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System.Text.RegularExpressions;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace Bagge.Seti.BusinessEntities
{
	[ActiveRecord]
	public class Customer : AuditablePrimaryKeyWithNameDomainObject<Customer, int>
	{
		[Property]
		[ValidatorComposition(CompositionType.Or)]
		[RequiredStringValidator(Negated = true)]
		[RegexValidator("Validators_Customer_CUIT_Pattern", typeof(Customer), MessageTemplateResourceName = "Validators_Customer_CUIT", MessageTemplateResourceType = typeof(Customer))]
		public string CUIT 
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

		[Property]
		public string City
		{
			get;
			set;
		}

		[Property]
		public string Phone
		{
			get;
			set;
		}

		[Property]
		public string MobilePhone
		{
			get;
			set;
		}

		[Property]
		[ValidatorComposition(CompositionType.Or)]
		[EmailValidator(Required = false, MessageTemplateResourceName = "Validators_Customer_Email", MessageTemplateResourceType = typeof(Customer))]
		public string Email
		{
			get;
			set;
		}

		[Property]
		public bool Subscription
		{
			get;
			set;
		}
	}
}
