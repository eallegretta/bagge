
using System.Collections.Generic;
using Castle.ActiveRecord;
namespace Bagge.Seti.BusinessEntities
{
	[ActiveRecord]
	public class Product : PrimaryKeyWithNameAndDescriptionDomainObject<Product, int>
	{
		[HasAndBelongsToMany(typeof(ProductProvider), ColumnKey = "ProductId", ColumnRef = "ProviderId", Lazy = true)]
		public virtual IList<ProductProvider> Providers
		{
			get;
			set;
		}


		[HasAndBelongsToMany(typeof(ProductTicket), ColumnKey = "ProductId", ColumnRef = "TicketId", Lazy = true)]
		public virtual IList<ProductTicket> Tickets
		{
			get;
			set;
		}
	}
}
