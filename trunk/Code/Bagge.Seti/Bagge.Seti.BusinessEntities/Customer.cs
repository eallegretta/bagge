

using Castle.ActiveRecord;
namespace Bagge.Seti.BusinessEntities
{
	[ActiveRecord]
	public class Customer : PrimaryKeyWithNameDomainObject<Customer, int>
	{
		[Property]
		public string CUIT 
		{ 
			get; 
			set; 
		}

		[Property]
		public string Address
		{
			get;
			set;
		}

		[Property]
		public string Floor
		{
			get;
			set;
		}

		[Property("Departament")]
		public string Apartment
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
		public string Email
		{
			get;
			set;
		}

		[Property(NotNull = false)]
		public decimal? Subscription
		{
			get;
			set;
		}
	}
}
