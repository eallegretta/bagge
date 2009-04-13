
using System.Collections.Generic;
using Castle.ActiveRecord;
using Bagge.Seti.Security.BusinessEntities;
using System;
namespace Bagge.Seti.BusinessEntities
{
	[ActiveRecord]
	[Serializable]
	public class Product : AuditablePrimaryKeyWithNameAndDescriptionDomainObject<Product, int>
	{
		[HasAndBelongsToMany(typeof(ProductProvider), Table = "ProductProvider", ColumnKey = "ProductId", ColumnRef = "ProviderId", Lazy = true)]
		public virtual IList<ProductProvider> Providers
		{
			get;
			set;
		}


		[HasAndBelongsToMany(typeof(ProductTicket), Table = "ProductTicket", ColumnKey = "ProductId", ColumnRef = "TicketId", Lazy = true)]
		public virtual IList<ProductTicket> Tickets
		{
			get;
			set;
		}
	}
}
