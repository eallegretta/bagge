using System.Collections.Generic;
using Castle.ActiveRecord;

namespace Bagge.Seti.BusinessEntities
{
	[ActiveRecord]
	public class Provider : PrimaryKeyDomainObject<Provider, int>
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

		[Property]
		public string Address
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
		public string PrimayPhone
		{
			get;
			set;
		}

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

		[Property]
		public string Email
		{
			get;
			set;
		}

		[Property]
		public string WebSite
		{
			get;
			set;
		}

		[Property]
		public string ContactName
		{
			get;
			set;
		}

		[HasAndBelongsToMany(typeof(ProductProvider), ColumnKey = "ProviderId", ColumnRef = "ProductId", Lazy = true)]
		public virtual IList<ProductProvider> Products
		{
			get; set;
		}


	}
}
