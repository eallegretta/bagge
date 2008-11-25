using Castle.ActiveRecord;

namespace Bagge.Seti.BusinessEntities
{
	[ActiveRecord]
	public class ProductTicket : PrimaryKeyDomainObject<ProductTicket, int>
	{
		[BelongsTo("ProductId")]
		public Product Product
		{
			get;
			set;
		}

		[BelongsTo("TicketId")]
		public Ticket Ticket
		{
			get;
			set;
		}

		[Property]
		public decimal? EstimatedQuantity
		{
			get;
			set;
		}
	}
}
