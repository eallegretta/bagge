using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.DataAccess.Contracts;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.DataAccess.ActiveRecord;
using Bagge.Seti.BusinessEntities.Exceptions;
using Bagge.Seti.BusinessLogic.Properties;

namespace Bagge.Seti.BusinessLogic
{
	public class CustomerManager: GenericManager<Customer, int>, ICustomerManager
	{
		public CustomerManager(ICustomerDao dao)
			: base(dao)
		{
		}

		public virtual Customer GetByCuit(string cuit)
		{
			Customer[] customers = this.FindAllByProperty("CUIT", cuit);
			if (customers.Length > 0)
				return customers[0];
			return null;
		}

		private bool IsCuitUnique(Customer customer)
		{
			if (customer.Id == 0)
				return GetByCuit(customer.CUIT) == null;
			else
			{
				Customer customerInDb = GetByCuit(customer.CUIT);
				return customerInDb.Equals(customer);
			}
		}

		public override int Create(Customer instance)
		{
			if (!IsCuitUnique(instance))
				throw new BusinessRuleException(Resources.CUITNotUniqueErrorMessage);

			return base.Create(instance);
		}

		public override void Update(Customer instance)
		{
			if (!IsCuitUnique(instance))
				throw new BusinessRuleException(Resources.CUITNotUniqueErrorMessage);

			base.Update(instance);
		}
	}
}
