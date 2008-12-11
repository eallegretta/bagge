using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.DataAccess.Contracts;

namespace Bagge.Seti.BusinessLogic
{
	public class ProductManager: GenericManager<Product, int>, IProductManager
	{
		public ProductManager(IProductDao dao)
			: base(dao)
		{
		}
	}
}
