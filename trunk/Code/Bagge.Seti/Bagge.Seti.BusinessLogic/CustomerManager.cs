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
using Bagge.Seti.DesignByContract;
using Bagge.Seti.DataAccess;

namespace Bagge.Seti.BusinessLogic
{
	public class CustomerManager: AuditableGenericManager<Customer, int>, ICustomerManager
	{
		public CustomerManager(ICustomerDao dao)
			: base(dao)
		{
		}

		public virtual Customer GetByCuit(string cuit)
		{
			if (cuit == string.Empty)
				return null;

			Customer[] customers = this.FindAllActiveByProperty("CUIT", cuit);
			if (customers.Length > 0)
				return customers[0];
			return null;


		}

		private bool IsCuitUnique(Customer customer)
		{
			Check.Require(customer != null);

			if (customer.Id == 0)
				return GetByCuit(customer.CUIT) == null;
			else
			{
				Customer customerInDb = GetByCuit(customer.CUIT);
				if (customerInDb == null)
					return true;

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
			if (!IsDelete)
			{
				if (!IsCuitUnique(instance))
					throw new BusinessRuleException(Resources.CUITNotUniqueErrorMessage);
			}
			if (!IsDeleteOrUndelete)
				SessionScopeUtils.FlushSessionScope();

			base.Update(instance);
		}
	}
}
