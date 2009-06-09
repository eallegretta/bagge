using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.DataAccess.Contracts;
using Bagge.Seti.DesignByContract;

namespace Bagge.Seti.BusinessLogic
{
	public class TicketManager : AuditableGenericManager<Ticket, int>, ITicketManager
	{
		ITicketStatusManager _ticketStatusManager;
		ITicketEmployeeDao _ticketEmployeeDao;
		ICustomerDao _customerDao;

		public TicketManager(ITicketDao dao, ITicketStatusManager ticketStatusManager, 
			ITicketEmployeeDao ticketEmployeeDao, ICustomerDao customerDao)
			: base(dao)
		{
			_ticketStatusManager = ticketStatusManager;
			_ticketEmployeeDao = ticketEmployeeDao;
			_customerDao = customerDao;
		}

		#region ITicketManager Members

		public Ticket[] FindAllByStatus(TicketStatusEnum status)
		{
			return FindAllActiveByProperty("Status",
				_ticketStatusManager.Get(status));
		}

		#endregion


		protected override void ReplaceFilters(IList<FilterPropertyValue> filter)
		{
			ReplaceStatusFilter(filter);

			ReplaceCustomerFilter(filter);

			ReplaceEmployeesFilter(filter);
		}

		private void ReplaceStatusFilter(IList<FilterPropertyValue> filter)
		{
			var statusFilter = filter.GetFilter("Status");

			if (statusFilter != null && statusFilter.Value is int)
				statusFilter.Value = _ticketStatusManager.Get((int)statusFilter.Value);
		}

		private void ReplaceCustomerFilter(IList<FilterPropertyValue> filter)
		{
			var customerFilter = filter.GetFilter("Customer");

			if (customerFilter != null && customerFilter.Value is int)
				customerFilter.Value = _customerDao.Get((int)customerFilter.Value);
		}

		private void ReplaceEmployeesFilter(IList<FilterPropertyValue> filter)
		{
			var employeesFilter = (from fil in filter
								   where fil.Property == "Employees" && fil.Value is int
								   select fil).FirstOrDefault();

			filter.Remove(employeesFilter);

			if (employeesFilter != null)
			{
				foreach (var productProvider in _ticketEmployeeDao.FindAllByEmployee((int)employeesFilter.Value))
					filter.Add(new FilterPropertyValue { Property = employeesFilter.Property, Type = employeesFilter.Type, Value = productProvider });
			}
		}

		public override int Create(Ticket instance)
		{
			instance.CreationDate = DateTime.Now;

			AssignTicketToProducts(instance);

			return base.Create(instance);
		}

		public override void Update(Ticket instance)
		{
			AssignTicketToProducts(instance);

			base.Update(instance);
		}

		private static void AssignTicketToProducts(Ticket instance)
		{
			foreach (var product in instance.Products)
				product.Ticket = instance;
		}


	}
}
