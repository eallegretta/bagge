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
using System.Diagnostics;
using Castle.Components.Validator;
using System.Net.Mail;
using System.Configuration;
using Bagge.Seti.DataAccess;
using Bagge.Seti.BusinessEntities.Security;

namespace Bagge.Seti.BusinessLogic
{
	[Securizable("Securizable_EmployeeManager", typeof(EmployeeManager))]
	public partial class EmployeeManager : AuditableGenericManager<Employee, int>, IEmployeeManager
	{
		ISimpleFindGetDao<EmployeeCategory, int> _employeeCategoryDao;
		ITicketManager _ticketManager;

		public EmployeeManager(IEmployeeDao dao, ISimpleFindGetDao<EmployeeCategory, int> employeeCategoryDao, ITicketManager ticketManager)
			: base(dao)
		{
			_employeeCategoryDao = employeeCategoryDao;
			_ticketManager = ticketManager;
		}

		#region IEmployeeManager Members

		[Securizable("Securizable_EmployeeManager_Authenticate", typeof(EmployeeManager))]
		public bool Authenticate(string username, string password)
		{

			Check.Require(!username.IsNullOrEmpty());
			Check.Require(!password.IsNullOrEmpty());

			try
			{
				Employee employee = GetByUsername(username);
				return (employee.Password == password.ToMD5());
			}
			catch (ObjectNotFoundException)
			{
				return false;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			
		}

		[Securizable("Securizable_EmployeeManager_GetByUsername", typeof(EmployeeManager))]
		public Employee GetByUsername(string username)
		{
			return GetByStringProperty("Username", username, Resources.EmployeeByUsernameNotFoundErrorMessage);
		}

		#endregion

		#region IEmployeeManager Members

		[Securizable("Securizable_EmployeeManager_RecoverPassword", typeof(EmployeeManager))]
		public void RecoverPassword(string email, string baseLinkPath)
		{
			Check.Require(!string.IsNullOrEmpty(email));

			var employee = GetByEmail(email);

			if (employee == null)
				throw new BusinessRuleException(Resources.EmployeeByEmailNotFoundErrorMessage);

			employee.RecoverPasswordKey = Guid.NewGuid().ToString();

			Update(employee);

			string link = string.Format("{0},{1}", email, employee.RecoverPasswordKey).ToBase64();

			SendEmail(email, Resources.RecoverPasswordEmailSubject,
				string.Format(Resources.RecoverPasswordEmailBodyRegenerate, employee.Fullname, baseLinkPath + link));
		}

		private static void SendEmail(string to, string subject, string body)
		{
			SmtpClient client = new SmtpClient();
			MailMessage msg = new MailMessage();
			msg.To.Add(to);

			msg.Subject = subject;
			msg.Body = body;

			client.EnableSsl = Settings.Default.EnableMailSsl;

			client.Send(msg);
		}

		[Securizable("Securizable_EmployeeManager_RegeneratePassword", typeof(EmployeeManager))]
		public void RegeneratePassword(string encodedKey)
		{
			Check.Require(!string.IsNullOrEmpty(encodedKey));

			string[] values = encodedKey.FromBase64().Split(',');
			string email = values[0];
			string recoverPasswordKey = values[1];

			var employee = GetByEmail(email);
			if (employee.RecoverPasswordKey != recoverPasswordKey)
				throw new BusinessRuleException(Resources.EmployeeRecoverPasswordKeyNotMatch);

			employee.RecoverPasswordKey = null;
			string password = RandomPassword.Generate();
			employee.Password = password.ToMD5();

			SendEmail(employee.Email, Resources.RecoverPasswordEmailSubject,
				string.Format(Resources.RecoverPasswordEmailBodyNewPassword, employee.Fullname, password));
		}

		[Securizable("Securizable_EmployeeManager_GetByEmail", typeof(EmployeeManager))]
		public Employee GetByEmail(string email)
		{
			return GetByStringProperty("Email", email, string.Empty);
		}


		private Employee GetByStringProperty(string propertyName, string propertyValue, string errorMessage)
		{
			Check.Require(!propertyValue.IsNullOrEmpty());

			Employee[] employees = Dao.FindAllByProperty(propertyName, propertyValue, null, null);
			if (employees.Length > 1)
				throw new BusinessRuleException(Resources.MultipleUsernamesErrorMessage);
			else if (employees.Length == 1)
				return employees[0];
			else
				throw new ObjectNotFoundException(string.Format(errorMessage, propertyValue));
		}


		protected override void ReplaceFilters(IList<FilterPropertyValue> filters)
		{
			var employeeCategoryFilter = (from filter in filters
										 where filter.Property == "Category" && filter.Value is int
										 select filter).FirstOrDefault();

			if (employeeCategoryFilter != null)
				employeeCategoryFilter.Value = _employeeCategoryDao.Get(employeeCategoryFilter.Value.ToString().ToInt32());
		}

		public override int Create(Employee instance)
		{
			Check.Require(instance != null);

			if (string.IsNullOrEmpty(instance.Password))
				throw new BusinessRuleException(Resources.EmployeePasswordRequiredErrorMessage);

			instance.Password = instance.Password.ToMD5();

			return base.Create(instance);
		}

		public override void Update(Employee instance)
		{
			Check.Require(instance != null);

			if (!IsDeleteOrUndelete)
			{
				SessionScopeUtils.FlushSessionScope();

				if (!string.IsNullOrEmpty(instance.Password))
					instance.Password = instance.Password.ToMD5();
				else
				{
					Employee userFromDb = Get(instance.Id);
					instance.Password = userFromDb.Password;
				}
			}
			else
			{
				CheckTicketRelationship(instance);
			}


			base.Update(instance);
		}

		

		private void CheckTicketRelationship(Employee instance)
		{
			IList<FilterPropertyValue> filters = new List<FilterPropertyValue>();
			filters.Add("Creator", instance);

			if (!Ticket.CheckTicketsAllClosed(_ticketManager.FindAllByProperties(filters)))
			{
				instance.Deleted = false;
				throw new CantDeleteException(Resources.EmployeeTicketRelatedErrorMessage);
			}
			filters.Clear();
			filters.Add("Employees", FilterPropertyValueType.In, instance);

			if (!Ticket.CheckTicketsAllClosed(_ticketManager.FindAllByProperties(filters)))
			{
				instance.Deleted = false;
				throw new CantDeleteException(Resources.EmployeeTicketRelatedErrorMessage);
			}
		}

		[Securizable("Securizable_EmployeeManager_FindAllActiveTechnicians", typeof(EmployeeManager))]
		public Employee[] FindAllActiveTechnicians()
		{
			var category = _employeeCategoryDao.Get(EmployeeCategory.TechnicianId);
			return FindAllActiveByProperty("Category", category);
		}

		#endregion

		
	}
}
