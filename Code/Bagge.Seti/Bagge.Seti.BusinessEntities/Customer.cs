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
		[RegexValidator("Validators.Customer.CUIT.Pattern", typeof(Customer), MessageTemplateResourceName = "Validators.Customer.CUIT", MessageTemplateResourceType = typeof(Customer))]
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
				if (Floor.HasValue)
					address += Floor.Value;
				if (Apartment.HasValue)
					address += Apartment.Value;

				return address;
			}
		}
		
		[Property]
		[StringLengthValidator(50, MessageTemplateResourceName = "Validators.Customer.Address.Length", MessageTemplateResourceType = typeof(Customer))]
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
		[StringLengthValidator(7, MessageTemplateResourceName = "Validators.Customer.ZipCode.Length", MessageTemplateResourceType = typeof(Customer))]
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
