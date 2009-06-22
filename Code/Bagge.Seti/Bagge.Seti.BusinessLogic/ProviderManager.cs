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
	public partial class ProviderManager : AuditableGenericManager<Provider, int>, IProviderManager
	{
		IProductProviderDao _productProviderDao;
		ITicketManager _ticketManager;

		public ProviderManager(IProviderDao dao, IProductProviderDao productProviderDao, ITicketManager ticketManager)
			: base(dao)
		{
			_productProviderDao = productProviderDao;
			_ticketManager = ticketManager;
		}

		public virtual Provider GetByCuit(string cuit)
		{
			if (string.IsNullOrEmpty(cuit))
				return null;

			Provider[] providers = this.FindAllActiveByProperty("CUIT", cuit);
			if (providers.Length > 0)
				return providers[0];

			return null;
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
				if (providerInDb == null)
					return true;

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

			if (IsDeleteOrUndelete)
				CheckTicketRelationship(instance);


			foreach (ProductProvider product in instance.Products)
			{
				product.Provider = instance;
			}

			if (!IsCuitUnique(instance))
				throw new BusinessRuleException(Resources.CUITNotUniqueErrorMessage);

			if (!IsDeleteOrUndelete)
			{
				SessionScopeUtils.FlushSessionScope();
				_productProviderDao.DeleteByProvider(instance.Id);
			}
				

			base.Update(instance);
		}

		protected override void ReplaceFilters(IList<FilterPropertyValue> filters)
		{
			var productsFilter = (from fil in filters
								  where fil.Property == "Products" && fil.Value is int
								  select fil).FirstOrDefault();

			filters.Remove(productsFilter);

			if (productsFilter != null)
			{
				foreach(var productProvider in _productProviderDao.FindAllByProduct((int)productsFilter.Value))
					filters.Add(new FilterPropertyValue { Property = productsFilter.Property, Type = productsFilter.Type, Value = productProvider }); 
			}
				
		}


		private void CheckTicketRelationship(Provider instance)
		{
			if (Ticket.CheckTicketsAllClosed(_ticketManager.FindAllByProvider(instance.Id)))
			{
				instance.Deleted = false;
				throw new CantDeleteException(Resources.ProviderTicketRelatedErrorMessage);
			}
		}
	}
}
