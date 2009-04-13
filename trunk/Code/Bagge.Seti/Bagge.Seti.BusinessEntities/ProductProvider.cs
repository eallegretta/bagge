
using Castle.ActiveRecord;
using System;

namespace Bagge.Seti.BusinessEntities
{
	[ActiveRecord]
	[Serializable]
	public class ProductProvider : PrimaryKeyDomainObject<ProductProvider, int>
	{
		[BelongsTo("ProductId")]
		public Product Product
		{
			get;
			set;
		}
		

		[BelongsTo("ProviderId")]
		public Provider Provider
		{
			get;
			set;
		}

		[Property]
		public decimal? Price
		{
			get;
			set;
		}

	}
}
