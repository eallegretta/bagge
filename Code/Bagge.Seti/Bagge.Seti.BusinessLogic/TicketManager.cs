using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.DataAccess.Contracts;
using Bagge.Seti.DesignByContract;
using Bagge.Seti.BusinessEntities.Security;
using System.Net.Mail;
using Bagge.Seti.BusinessLogic.Properties;
using Bagge.Seti.DataAccess;
using Bagge.Seti.BusinessEntities.Exceptions;

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

		public string EmailUrl
		{
			get;
			set;
		}

		private static void SendUpdatedTicketEmail(Ticket ticket, string url)
		{

			using (SessionScopeUtils.NewSessionScope())
			{
				SmtpClient client = new SmtpClient();
				MailMessage msg = new MailMessage();


				if (!string.IsNullOrEmpty(ticket.Creator.Email))
					msg.To.Add(ticket.Creator.Email);
				foreach (var employee in ticket.Employees)
				{
					if (!string.IsNullOrEmpty(employee.Email))
						msg.To.Add(employee.Email);
				}

				msg.Subject = Resources.UpdateTicketEmailSubject;
				msg.Body = string.Format(Resources.UpdateTicketEmailBody, ticket.Id, url);
				msg.IsBodyHtml = true;


				client.EnableSsl = Settings.Default.EnableMailSsl;

				client.Send(msg);
			}
		}


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

		public void Close(int ticketId)
		{
			var ticket = Get(ticketId);

			if (ticket.Customer.Subscription)
				ticket.Status = _ticketStatusManager.Get(TicketStatusEnum.Closed);
			else
				ticket.Status = _ticketStatusManager.Get(TicketStatusEnum.PendingPayment);

			base.Update(ticket);
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

			SendUpdatedTicketEmail(instance, EmailUrl);
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

		#region ITicketManager Members


		public void UpdateProgress(int ticketId, decimal realDuration, string notes)
		{
			var ticket = Get(ticketId);

			if (ticket.Status == TicketStatusEnum.Closed)
				throw new BusinessRuleException(Resources.CannotUpdateStatusClosedTicketErrorMessage);

			if(realDuration > decimal.MinValue)
				ticket.RealDuration = realDuration;
			ticket.Notes = notes;

			base.Update(ticket);

			SendUpdatedTicketEmail(ticket, EmailUrl);
		}

		#endregion
	}
}
