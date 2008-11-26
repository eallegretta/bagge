
using System.Collections.Generic;
using Castle.ActiveRecord;
using Bagge.Seti.Security.BusinessEntities;
namespace Bagge.Seti.BusinessEntities
{
	[ActiveRecord]
	public class Product : AuditablePrimaryKeyWithNameAndDescriptionDomainObject<Product, int>
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
