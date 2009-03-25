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

		private bool IsCuitUnique(Customer customer)
		{
			if (customer.Id == 0)
				return this.FindAllByProperty("CUIT", customer.CUIT).Length == 0;
			else
			{
				Customer[] customers = this.FindAllByProperty("CUIT", customer);
				if (customers.Length == 0)
					return true;
				return customers[0].Equals(customer);
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
