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
using Bagge.Seti.Security.BusinessEntities;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;

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

		public override Employee Get(int id)
		{
			var employee = base.Get(id);
			if (employee.Deleted)
				throw new ObjectNotFoundException();
			return employee;
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

		[SecurizableCrud("Securizable_EmployeeManager_GetByUsername", typeof(EmployeeManager), FunctionAction.List)]
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

			

			string link = string.Format("{0},{1}", email, employee.RecoverPasswordKey).ToBase64();

			try
			{

				SendEmail(email, Resources.RecoverPasswordEmailSubject,
					string.Format(Resources.RecoverPasswordEmailBodyRegenerate, employee.Fullname, baseLinkPath + link));

			}
			catch (Exception ex)
			{
				throw new BusinessRuleException(Resources.EmailSendFailErrorMessage, ex);
			}
			Update(employee);
		}

		private static void SendEmail(string to, string subject, string body)
		{
			SmtpClient client = new SmtpClient();
			MailMessage msg = new MailMessage();
			msg.To.Add(to);

			msg.Subject = subject;
			msg.Body = body;
			msg.IsBodyHtml = true;


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

		[SecurizableCrud("Securizable_EmployeeManager_GetByEmail", typeof(EmployeeManager), FunctionAction.List)]
		public Employee GetByEmail(string email)
		{
			return GetByStringProperty("Email", email, Resources.EmployeeByEmailNotFoundErrorMessage);
		}


		private Employee GetByStringProperty(string propertyName, string propertyValue, string errorMessage)
		{
			Check.Require(!propertyValue.IsNullOrEmpty());

			var properties = new List<FilterPropertyValue>();
			properties.Add(propertyName, propertyValue);
			properties.Add(NotDeletedFilter);

			Employee[] employees = Dao.FindAllByProperties(properties, null, null);
			if (employees.Length > 1)
				throw new BusinessRuleException(Resources.MultipleUsernamesErrorMessage);
			else if (employees.Length == 1)
			{
				var employee = employees[0];
				if (employee.Deleted)
					throw new ObjectNotFoundException(string.Format(errorMessage, propertyValue));
				return employee;
			}
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

		[SecurizableCrud("Securizable_EmployeeManager_UpdateProfile", typeof(EmployeeManager), FunctionAction.Update)]
		public virtual void UpdateProfile(Employee instance)
		{
			Check.Require(instance != null);

			var employeeFromDb = Get(instance.Id);
			employeeFromDb.Firstname = instance.Firstname;
			employeeFromDb.Lastname = instance.Lastname;
			employeeFromDb.Email = instance.Email;
			employeeFromDb.Phone = instance.Phone;
			employeeFromDb.EmergencyPhone = instance.EmergencyPhone;
			if(!string.IsNullOrEmpty(instance.Password))
				employeeFromDb.Password = instance.Password.ToMD5();

			base.Update(employeeFromDb);
		}

		public override void Update(Employee instance)
		{
			Check.Require(instance != null);

			var employeeFromDb = Get(instance.Id);

			if (!IsDeleteOrUndelete)
			{
				SessionScopeUtils.FlushSessionScope();
				
				employeeFromDb.Firstname = instance.Firstname;
				employeeFromDb.Lastname = instance.Lastname;
				employeeFromDb.Email = instance.Email;
				employeeFromDb.Phone = instance.Phone;
				employeeFromDb.EmergencyPhone = instance.EmergencyPhone;
				employeeFromDb.Category = instance.Category;
				employeeFromDb.Roles = instance.Roles;

				if (!string.IsNullOrEmpty(instance.Password))
					employeeFromDb.Password = instance.Password.ToMD5();
				
			}
			else
			{
				CheckTicketRelationship(employeeFromDb);
			}


			base.Update(employeeFromDb);
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

		[SecurizableCrud("Securizable_EmployeeManager_FindAllActiveTechnicians", typeof(EmployeeManager), FunctionAction.List)]
		public Employee[] FindAllActiveTechnicians()
		{
			var category = _employeeCategoryDao.Get(EmployeeCategory.TechnicianId);
			return FindAllActiveByProperty("Category", category);
		}

		#endregion



		#region IEmployeeManager Members

		private static readonly Employee _notAuthorizedUser = new Employee { 
			Id = -1,
			Username = "NOT_AUTHORIZED",
			Roles = new List<Role>(), 
			IsAuthenticated = false };

		public Employee GetNotAuthorizedUser()
		{
			return _notAuthorizedUser;
		}

		#endregion

		#region IEmployeeManager Members


		public Employee GetFromActiveDirectory(string server, string user, string password, string usernameToLook)
		{
			Check.Require(!string.IsNullOrEmpty(usernameToLook));
			Check.Require(usernameToLook.Contains('\\'), "UsernameToLook must be in the form domain\\username");

			var context = new PrincipalContext(ContextType.Domain, server, user, password);
			var userPrincipal = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, usernameToLook.Split('\\')[1]);

			if (userPrincipal == null)
				return null;

			var directoryEntry = userPrincipal.GetUnderlyingObject() as DirectoryEntry;
			
			string otherTelephone = null;

			if(directoryEntry.Properties.Contains("otherTelephone"))
				otherTelephone = directoryEntry.Properties["otherTelephone"].Value.ToString();

			return new Employee
			{
				Firstname = userPrincipal.GivenName,
				Lastname = userPrincipal.Surname,
				Email = userPrincipal.EmailAddress,
				Phone = userPrincipal.VoiceTelephoneNumber,
				EmergencyPhone = otherTelephone,
				Roles = new List<Role>(),
				Username = usernameToLook
			};
		}

		#endregion
	}
}
