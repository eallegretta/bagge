using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.DataAccess.Contracts;
using Bagge.Seti.DesignByContract;
using Bagge.Seti.BusinessEntities.Security;

namespace Bagge.Seti.BusinessLogic
{
	[Securizable("Securizable_TicketManager", typeof(TicketManager))]
	public partial class TicketManager : AuditableGenericManager<Ticket, int>, ITicketManager
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

		[Securizable("Securizable_TicketManager_FindAllByStatus", typeof(TicketManager))]
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

		public int CreateApproved(Ticket instance)
		{
			instance.Status = _ticketStatusManager.Get(TicketStatusEnum.Open);

			return Create(instance);
		}

		public override int Create(Ticket instance)
		{
			CheckRequirements(instance);

			instance.CreationDate = DateTime.Now;
			if (instance.Status != TicketStatusEnum.Open)
				instance.Status = _ticketStatusManager.Get(TicketStatusEnum.PendingAproval);

			AssignTicketToProducts(instance);

			return base.Create(instance);
		}

		private void CheckRequirements(Ticket instance)
		{
			Check.Require(instance.Status != null);
			Check.Require(instance.Creator != null);
			Check.Require(instance.Customer != null);
			Check.Require(instance.Budget.HasValue && instance.Budget.Value >= 0);
		}

		public override void Update(Ticket instance)
		{
			CheckRequirements(instance);

			Ticket instanceFromDb = Get(instance.Id);

			GetDao<ITicketDao>().DeleteProducts(instance.Id);

			instance.CreationDate = instanceFromDb.CreationDate;
			instance.Creator = instanceFromDb.Creator;

			AssignTicketToProducts(instance);

			base.Update(instance);
		}

		private static void AssignTicketToProducts(Ticket instance)
		{
			foreach (var product in instance.Products)
				product.Ticket = instance;
		}

		public override void Delete(int id)
		{
		}

		public override void Undelete(int id)
		{
			
		}



		#region ITicketManager Members

		[Securizable("Securizable_TicketManager_FindAllByProduct", typeof(TicketManager))]
		public Ticket[] FindAllByProduct(int productId)
		{
			return GetDao<ITicketDao>().FindAllByProduct(productId);
		}

		[Securizable("Securizable_TicketManager_FindAllByProvider", typeof(TicketManager))]
		public Ticket[] FindAllByProvider(int providerId)
		{
			return GetDao<ITicketDao>().FindAllByProvider(providerId);
		}

		#endregion
	}
}
