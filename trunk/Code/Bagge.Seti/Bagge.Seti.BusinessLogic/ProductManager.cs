using System;
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
		ITicketManager _ticketManager;

		public ProductManager(IProductDao dao, IProductProviderDao productProviderDao, ITicketManager ticketManager)
			: base(dao)
		{
			_productProviderDao = productProviderDao;
			_ticketManager = ticketManager;
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

			if (!IsDeleteOrUndelete)
			{
				SessionScopeUtils.FlushSessionScope();
				_productProviderDao.DeleteByProduct(instance.Id);
			}

			base.Update(instance);
		}


		protected override void ReplaceFilters(IList<FilterPropertyValue> filters)
		{
			var providersFilter = (from fil in filters
								  where fil.Property == "Providers" && fil.Value is int
								  select fil).FirstOrDefault();

			filters.Remove(providersFilter);

			if (providersFilter != null)
			{
				foreach (var product in _productProviderDao.FindAllByProvider((int)providersFilter.Value))
					filters.Add(new FilterPropertyValue { Property = providersFilter.Property, Type = providersFilter.Type, Value = product });
			}
		}

		private void CheckTicketRelationship(Product instance)
		{
			if (_ticketManager.FindAllByProduct(instance.Id).Length > 0)
			{
				instance.Deleted = false;
				throw new CantDeleteException(Resources.ProductTicketRelatedErrorMessage);
			}
		}
	}
}
