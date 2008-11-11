
namespace Bagge.Seti.BusinessEntities
{
	public class ProductTicket : PrimaryKeyDomainObject<ProductTicket, int>
	{
		public Product Product
		{
			get;
			set;
		}

		public Ticket Ticket
		{
			get;
			set;
		}

		public decimal? EstimatedQuantity
		{
			get;
			set;
		}
	}
}
