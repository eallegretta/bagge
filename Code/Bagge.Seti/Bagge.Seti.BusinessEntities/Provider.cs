


using System.Collections.Generic;
namespace Bagge.Seti.BusinessEntities
{
	public class Provider : PrimaryKeyDomainObject<Provider, int>
	{
		public ProviderCalification Calification
		{
			get;
			set;
		}

		public string CUIT
		{
			get;
			set;
		}

		public string Company
		{
			get;
			set;
		}

		public string Address
		{
			get;
			set;
		}

		public string Apartment
		{
			get;
			set;
		}

		public string ZipCode
		{
			get;
			set;
		}

		public string City
		{
			get;
			set;
		}

		public string PrimayPhone
		{
			get;
			set;
		}

		public string SecondaryPhone
		{
			get;
			set;
		}

		public string FaxPhone
		{
			get;
			set;
		}

		public string Email
		{
			get;
			set;
		}

		public string WebSite
		{
			get;
			set;
		}

		public string ContactName
		{
			get;
			set;
		}

		public virtual IList<ProductProvider> Products
		{
			get; set;
		}


	}
}
