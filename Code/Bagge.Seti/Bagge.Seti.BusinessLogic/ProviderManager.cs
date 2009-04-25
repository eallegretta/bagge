using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.DataAccess.Contracts;
using Bagge.Seti.DesignByContract;
using Bagge.Seti.BusinessLogic.Properties;
using Bagge.Seti.BusinessEntities.Exceptions;
using Bagge.Seti.DataAccess;

namespace Bagge.Seti.BusinessLogic
{
	public class ProviderManager : AuditableGenericManager<Provider, int>, IProviderManager
	{
		IProductProviderDao _productProviderDao;

		public ProviderManager(IProviderDao dao, IProductProviderDao productProviderDao)
			: base(dao)
		{
			_productProviderDao = productProviderDao;
		}

		public virtual Provider GetByCuit(string cuit)
		{
			Check.Require(!string.IsNullOrEmpty(cuit));

			Provider[] providers = this.FindAllActiveByProperty("CUIT", cuit);
			if (providers.Length > 0)
				return providers[0];
			return null;
		}


		public Provider[] FindAllByName(string productName, int maxRecords)
		{
			Check.Require(!string.IsNullOrEmpty(productName));

			FilterPropertyValue filter = new FilterPropertyValue();
			filter.Property = "Name";
			filter.Type = FilterPropertyValueType.Like;
			filter.Value = productName;

			return Dao.SlicedFindAllByProperties(
				0,
				maxRecords,
				new List<FilterPropertyValue> { filter },
				"Name", null);
		}

		public Provider[] FindAllByName(string productName)
		{
			Check.Require(!string.IsNullOrEmpty(productName));

			FilterPropertyValue filter = new FilterPropertyValue();
			filter.Property = "Name";
			filter.Type = FilterPropertyValueType.Like;
			filter.Value = productName;

			return Dao.FindAllByProperties(
				new List<FilterPropertyValue> { filter }, 
				"Name", null);
		}

		public Provider GetByName(string name)
		{
			Check.Require(!string.IsNullOrEmpty(name));

			var products = Dao.FindAllByProperty("Name", name, null, null);
			if (products.Length > 1)
				throw new BusinessRuleException(Resources.MultipleNamesErrorMessage);

			if (products.Length == 1)
				return products[0];

			throw new ObjectNotFoundException(Resources.InstanceNotFound);
		}

		private bool IsCuitUnique(Provider provider)
		{
			Check.Require(provider != null);

			if (provider.Id == 0)
				return GetByCuit(provider.CUIT) == null;
			else
			{
				Provider providerInDb = GetByCuit(provider.CUIT);
				return providerInDb.Equals(provider);
			}
		}

		public override int Create(Provider instance)
		{
			Check.Require(instance != null);

			foreach (ProductProvider product in instance.Products)
			{
				product.Provider = instance;
			}

			if (!IsCuitUnique(instance))
				throw new BusinessRuleException(Resources.CUITNotUniqueErrorMessage);

			return base.Create(instance);
		}

		public override void Update(Provider instance)
		{
			Check.Require(instance != null);


			foreach (ProductProvider product in instance.Products)
			{
				product.Provider = instance;
			}

			if (!IsCuitUnique(instance))
				throw new BusinessRuleException(Resources.CUITNotUniqueErrorMessage);

			SessionScopeUtils.FlushSessionScope();

			_productProviderDao.DeleteByProvider(instance.Id);

			base.Update(instance);
		}

		protected override Provider[] FindAllByProperties(IList<FilterPropertyValue> filter, string orderBy, bool ascending)
		{
			ReplaceProductsFilter(filter);

			return base.FindAllByProperties(filter, orderBy, ascending);
		}

		private void ReplaceProductsFilter(IList<FilterPropertyValue> filter)
		{
			var productsFilter = (from fil in filter
								  where fil.Property == "Products"
								  select fil).FirstOrDefault();
			if(productsFilter != null)
				productsFilter.Value = _productProviderDao.FindAllByProduct((int)productsFilter.Value);
		}

		protected override Provider[] SlicedFindAllByProperties(int startIndex, int pageSize, IList<FilterPropertyValue> filter, string orderBy, bool? ascending)
		{
			ReplaceProductsFilter(filter);

			return base.SlicedFindAllByProperties(startIndex, pageSize, filter, orderBy, ascending);
		}

	}
}
