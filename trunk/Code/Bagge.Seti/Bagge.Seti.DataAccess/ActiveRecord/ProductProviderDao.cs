using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.DataAccess.Contracts;
using Castle.ActiveRecord.Queries;
using Castle.ActiveRecord;

namespace Bagge.Seti.DataAccess.ActiveRecord
{
	public class ProductProviderDao: GenericDao<ProductProvider, int>, IProductProviderDao
	{
		#region IProductProviderDao Members

		public void DeleteByProduct(int productId)
		{
			ActiveRecordMediator<ProductProvider>.DeleteAll("ProductId = " + productId);
		}

		public void DeleteByProvider(int providerId)
		{
			ActiveRecordMediator<ProductProvider>.DeleteAll("ProviderId = " + providerId);
		}


		public ProductProvider[] FindAllByProduct(int productId)
		{
			string hql = "from ProductProvider p where p.Product.Id = ?";
			var query = new SimpleQuery<ProductProvider>(hql, productId);

			return query.Execute();
		}

		public ProductProvider[] FindAllByProvider(int providerId)
		{
			string hql = "from ProductProvider p where p.Provider.Id = ?";
			var query = new SimpleQuery<ProductProvider>(hql, providerId);

			return query.Execute();
		}

		#endregion
	}
}
