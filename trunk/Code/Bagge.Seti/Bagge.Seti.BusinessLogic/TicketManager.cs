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
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.BusinessLogic
{
	[Securizable("Securizable_TicketManager", typeof(TicketManager))]
	public partial class TicketManager : AuditableGenericManager<Ticket, int>, ITicketManager
	{
		ITicketStatusManager _ticketStatusManager;
		//ITicketEmployeeDao _ticketEmployeeDao;
		IEmployeeDao _employeeDao;
		ICustomerDao _customerDao;

		public TicketManager(ITicketDao dao, ITicketStatusManager ticketStatusManager,
			/*ITicketEmployeeDao ticketEmployeeDao*/ IEmployeeDao employeeDao, ICustomerDao customerDao)
			: base(dao)
		{
			_ticketStatusManager = ticketStatusManager;
			//_ticketEmployeeDao = ticketEmployeeDao;
			_employeeDao = employeeDao;
			_customerDao = customerDao;
			SendEmails = Settings.Default.EnableMail;
		}

		#region ITicketManager Members

		public string EmailUrl
		{
			get;
			set;
		}

		public bool SendEmails
		{
			get;
			set;
		}

		private void SendUpdatedTicketEmail(Ticket ticket, string url)
		{
			if (SendEmails)
			{
				IDisposable session = null;

				if (!SessionScopeUtils.InSessionScope)
					session = SessionScopeUtils.NewSessionScope();

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

				if (session != null)
					session.Dispose();
			}
		}


		[SecurizableCrud("Securizable_TicketManager_FindAllByStatus", typeof(TicketManager), FunctionAction.List)]
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
				/*foreach (var ticketEmployee in _ticketEmployeeDao.FindAllByEmployee((int)employeesFilter.Value))
					filter.Add(new FilterPropertyValue { Property = employeesFilter.Property, Type = employeesFilter.Type, Value = ticketEmployee });*/
				filter.Add(
					new FilterPropertyValue
					{
						Property = employeesFilter.Property,
						Type = employeesFilter.Type,
						Value = _employeeDao.Get((int)employeesFilter.Value)
					});
			}
		}

		[SecurizableCrud("Securizable_TicketManager_CreateApproved", typeof(TicketManager), FunctionAction.Create)]
		public int CreateApproved(Ticket instance)
		{
			if (!instance.Customer.Subscription && !instance.Budget.HasValue)
				throw new BusinessRuleException(Resources.TicketBudgetRequiredErrorMessage);


			instance.Status = _ticketStatusManager.Get(TicketStatusEnum.Open);

			return Create(instance);
		}

		[SecurizableCrud("Securizable_TicketManager_Close", typeof(TicketManager), FunctionAction.Update)]
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

		private void AssignTicketToProducts(Ticket instance)
		{
			foreach (var product in instance.Products)
				product.Ticket = instance;
		}

		public override void Delete(Ticket instance)
		{
		}

		public override void Undelete(Ticket instance)
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

		[SecurizableCrud("Securizable_TicketManager_UpdateProgress", typeof(TicketManager), FunctionAction.Update)]
		public void UpdateProgress(int ticketId, decimal realDuration, string notes)
		{
			var ticket = Get(ticketId);

			//if (ticket.Status == TicketStatusEnum.Closed)
			if(ticket.IsClosed)
				throw new BusinessRuleException(Resources.CannotUpdateStatusClosedTicketErrorMessage);

			if (realDuration > decimal.MinValue)
				ticket.RealDuration = realDuration;
			ticket.Notes = notes;

			base.Update(ticket);

			SendUpdatedTicketEmail(ticket, EmailUrl);
		}

		#endregion

		#region ITicketManager Members


		public Ticket[] FindAllByExecutionWeek(DateTime weekStartDate, DateTime weekEndDate)
		{
			var filters = new List<FilterPropertyValue>();
			weekStartDate = new DateTime(weekStartDate.Year, weekStartDate.Month, weekStartDate.Day, 0, 0, 0);
			weekEndDate = new DateTime(weekEndDate.Year, weekEndDate.Month, weekEndDate.Day, 23, 59, 59);
			filters.AddBetween("ExecutionDateTime", weekStartDate, weekEndDate);
			return FindAllActiveByPropertiesOrdered(filters, "ExecutionDateTime");
		}

		public Ticket[] FindAllByExecutionWeekAndTechnician(DateTime weekStartDate, DateTime weekEndDate, int technicianId)
		{
			var filters = new List<FilterPropertyValue>();
			weekStartDate = new DateTime(weekStartDate.Year, weekStartDate.Month, weekStartDate.Day, 0, 0, 0);
			weekEndDate = new DateTime(weekEndDate.Year, weekEndDate.Month, weekEndDate.Day, 23, 59, 59);
			filters.AddBetween("ExecutionDateTime", weekStartDate, weekEndDate);
			var employee = _employeeDao.Get(technicianId);
			filters.Add("Employees", FilterPropertyValueType.In, employee);
			return FindAllActiveByPropertiesOrdered(filters, "ExecutionDateTime");
		}


		#endregion
	}
}
