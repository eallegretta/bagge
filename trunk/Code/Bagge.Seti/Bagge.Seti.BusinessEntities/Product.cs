
using System.Collections.Generic;
namespace Bagge.Seti.BusinessEntities
{
	public class Product : PrimaryKeyWithNameAndDescriptionDomainObject<Product, int>
	{
		public virtual IList<ProductProvider> Providers
		{
			get;
			set;
		}

		public virtual IList<ProductTicket> Tickets
		{
			get;
			set;
		}
	}
}
