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
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.BusinessEntities.Validators;

namespace Bagge.Seti.BusinessLogic
{
	[Securizable("Securizable_ProductManager", typeof(ProductManager))]
	public partial class ProductManager : AuditableGenericManager<Product, int>, IProductManager
	{
		IProductProviderDao _productProviderDao;
		ITicketManager _ticketManager;

		public ProductManager(IProductDao dao, IProductProviderDao productProviderDao, ITicketManager ticketManager)
			: base(dao)
		{
			_productProviderDao = productProviderDao;
			_ticketManager = ticketManager;
		}


		[SecurizableCrud("Securizable_ProductManager_GetByName", typeof(ProductManager), FunctionAction.Retrieve)]
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

		[AvoidValidation]
		public bool IsNameUnique(Product product)
		{
			Check.Require(product != null);
			Check.Require(!string.IsNullOrEmpty(product.Name));

			try
			{
				var prodFromDb = GetByName(product.Name);
				return product.Id == prodFromDb.Id;
			}
			catch (ObjectNotFoundException)
			{
				return true;
			}
			catch (BusinessRuleException)
			{
				return false;
			}
			
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

			if (IsDeleteOrUndelete)
				CheckTicketRelationship(instance);

			foreach (ProductProvider provider in instance.Providers)
			{
				provider.Product = instance;
			}

			if (!IsDeleteOrUndelete)
			{
				SessionScopeUtils.FlushSessionScope();

				UpdateProductProviders(instance);
			}

			base.Update(instance);
		}

		private void UpdateProductProviders(Product instance)
		{
			IList<ProductProvider> providers = new List<ProductProvider>(instance.Providers);

			var productProvidersFromDb = _productProviderDao.FindAllByProduct(instance.Id);
			foreach (var productProviderFromDb in productProvidersFromDb)
			{
				var query = from pp in providers
							where pp.Provider.Id == productProviderFromDb.Provider.Id
							select pp;
				var productProvider = query.FirstOrDefault();
				if (productProvider == null)
					_productProviderDao.Delete(productProviderFromDb.Id);
				else
				{
					productProviderFromDb.Price = productProvider.Price;
					_productProviderDao.Update(productProviderFromDb);
					providers.Remove(productProvider);
				}
			}

			instance.Providers = providers;
		}


		protected override void ReplaceFilters(IList<FilterPropertyValue> filters)
		{
			var providersFilter = (from fil in filters
								  where fil.Property == "Providers" && fil.Value is int
								  select fil).FirstOrDefault();

			filters.Remove(providersFilter);

			if (providersFilter != null)
			{
				bool filterReplaced = false;
				foreach (var product in _productProviderDao.FindAllByProvider((int)providersFilter.Value))
				{
					filterReplaced = true;
					filters.Add(new FilterPropertyValue { Property = providersFilter.Property, Type = providersFilter.Type, Value = product });
				}

				if (!filterReplaced)
					filters.Add(
						new FilterPropertyValue
						{
							Property = "Id",
							Type = FilterPropertyValueType.Equals,
							Value = -1
						});
			}
		}

		private void CheckTicketRelationship(Product instance)
		{
			if (!Ticket.CheckTicketsAllClosed(_ticketManager.FindAllByProduct(instance.Id)))
			{
				instance.Deleted = false;
				throw new CantDeleteException(Resources.ProductTicketRelatedErrorMessage);
			}
		}
	}
}
