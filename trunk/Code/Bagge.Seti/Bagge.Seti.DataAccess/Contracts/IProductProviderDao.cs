using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.DataAccess.Contracts
{
	public interface IProductProviderDao: IDao<ProductProvider, int>
	{
		void DeleteByProduct(int productId);
		void DeleteByProvider(int providerId);
	}
}
