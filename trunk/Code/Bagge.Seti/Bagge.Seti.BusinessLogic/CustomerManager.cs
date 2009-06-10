﻿using System;
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
		ITicketManager _ticketManager;

		public CustomerManager(ICustomerDao dao, ITicketManager ticketManager)
			: base(dao)
		{
			_ticketManager = ticketManager;
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

			if (IsDeleteOrUndelete)
				CheckTicketRelationship(instance);

			base.Update(instance);
		}

		private void CheckTicketRelationship(Customer instance)
		{
			IList<FilterPropertyValue> filters = new List<FilterPropertyValue>();
			filters.Add("Customer", instance);

			if (_ticketManager.FindAllByProperties(filters).Length > 0)
			{
				instance.Deleted = false;
				throw new CantDeleteException(Resources.CustomerTicketRelatedErrorMessage);
			}
		}
	}
}
