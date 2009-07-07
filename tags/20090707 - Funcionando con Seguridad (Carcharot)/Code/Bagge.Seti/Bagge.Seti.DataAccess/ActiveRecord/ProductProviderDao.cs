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

		#region IProductProviderDao Members

		public ProductProvider[] FindProductsInIdsByAggregation(int[] ids, string aggregation)
		{
			string hql = string.Format("select pp.Product.Id as Id, {0}(pp.Price) from ProductProvider pp where pp.Product.Id in (:ids) group by pp.Product.Id order by pp.Product.Id", aggregation);
			var query = new SimpleQuery<ProductProvider>(hql);
			query.SetParameterList("ids", ids);

			var productProviders = query.Execute();


			hql = "from Product p where p.Product.Id in (:ids) order by p.Product.Id";
			var productQuery = new SimpleQuery<Product>(hql);
			productQuery.SetParameterList("ids", ids);

			int index = 0;
			foreach (var product in productQuery.Execute())
			{
				var productProvider = productProviders[index++];

				productProvider.Product = product;
				productProvider.Id = 0;
			}

			return productProviders;
		}

		#endregion
	}
}
