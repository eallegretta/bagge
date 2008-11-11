
namespace Bagge.Seti.BusinessEntities
{
	public class ProductProvider : PrimaryKeyDomainObject<ProductProvider, int>
	{
		public Product Product
		{
			get;
			set;
		}
		
		public Provider Provider
		{
			get;
			set;
		}

		public decimal? Price
		{
			get;
			set;
		}

	}
}
