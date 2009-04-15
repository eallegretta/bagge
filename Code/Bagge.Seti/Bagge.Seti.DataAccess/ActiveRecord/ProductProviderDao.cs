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
//			ActiveRecordMediator<ProductProvider>.DeleteAll("

			string hql = "delete ProductProvider pp where pp.Product.Id = ?";
			ScalarQuery<ProductProvider> query = new ScalarQuery<ProductProvider>(
				typeof(ProductProvider), hql, productId);
			query.Execute();
		}

		public void DeleteByProvider(int providerId)
		{
			string hql = "delete ProductProvider pp where pp.Provider.Id = ?";
			ScalarQuery<ProductProvider> query = new ScalarQuery<ProductProvider>(
				typeof(ProductProvider), hql, providerId);
			query.Execute();
		}

		#endregion
	}
}
