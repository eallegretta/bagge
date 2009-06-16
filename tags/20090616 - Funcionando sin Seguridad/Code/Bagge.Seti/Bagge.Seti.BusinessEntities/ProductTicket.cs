using Castle.ActiveRecord;
using System;

namespace Bagge.Seti.BusinessEntities
{
	[ActiveRecord]
	[Serializable]
	public class ProductTicket : PrimaryKeyDomainObject<ProductTicket, int>
	{
		[BelongsTo("ProductProviderId", Cascade = CascadeEnum.None)]
		public ProductProvider ProductProvider
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
