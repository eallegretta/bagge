﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.DataAccess.Contracts;
using Bagge.Seti.BusinessEntities.Exceptions;
using Bagge.Seti.BusinessLogic.Properties;
using Bagge.Seti.DesignByContract;
using Bagge.Seti.DataAccess;

namespace Bagge.Seti.BusinessLogic
{
	public class ProductManager : AuditableGenericManager<Product, int>, IProductManager
	{
		IProductProviderDao _productProviderDao;

		public ProductManager(IProductDao dao, IProductProviderDao productProviderDao)
			: base(dao)
		{
			_productProviderDao = productProviderDao;
		}
        

		public Product GetByName(string name)
		{   
			Check.Require(!string.IsNullOrEmpty(name));

			var products = FindAllActiveByProperty("Name", name);
			if (products.Length > 1)
				throw new BusinessRuleException(Resources.MultipleNamesErrorMessage);

			if (products.Length == 1)
				return products[0];

			throw new ObjectNotFoundException(Resources.InstanceNotFound);
		}

		public override int Create(Product instance)
		{
			Check.Require(instance != null);

			foreach (ProductProvider provider in instance.Providers)
			{
				provider.Product = instance;
			}


			return base.Create(instance);
		}

		public override void Update(Product instance)
		{
			Check.Require(instance != null);

			foreach (ProductProvider provider in instance.Providers)
			{
				provider.Product = instance;
			}

			SessionScopeUtils.FlushSessionScope();

			_productProviderDao.DeleteByProduct(instance.Id);

			base.Update(instance);
		}

		protected override Product[] FindAllByProperties(IList<FilterPropertyValue> filter, string orderBy, bool? ascending)
		{
			Check.Require(filter != null);

			ReplaceProvidersFilter(filter);

			return base.FindAllByProperties(filter, orderBy, ascending);
		}

		private void ReplaceProvidersFilter(IList<FilterPropertyValue> filter)
		{
			var productsFilter = (from fil in filter
								  where fil.Property == "Providers"
								  select fil).FirstOrDefault();
			if(productsFilter != null)
				productsFilter.Value = _productProviderDao.FindAllByProduct((int)productsFilter.Value);
		}

		protected override Product[] SlicedFindAllByProperties(int startIndex, int pageSize, IList<FilterPropertyValue> filter, string orderBy, bool? ascending)
		{
			Check.Require(filter != null);

			ReplaceProvidersFilter(filter);

			return base.SlicedFindAllByProperties(startIndex, pageSize, filter, orderBy, ascending);
		}
	}
}
