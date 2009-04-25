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

namespace Bagge.Seti.BusinessLogic
{
	public class EmployeeManager: AuditableGenericManager<Employee, int>, IEmployeeManager
	{
		public EmployeeManager(IEmployeeDao dao)
			: base(dao)
		{
		}

		#region IEmployeeManager Members

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

		public Employee GetByUsername(string username)
		{
			return GetByStringProperty("Username", username, Resources.EmployeeByUsernameNotFoundErrorMessage);
		}

		#endregion

		#region IEmployeeManager Members


		public void RecoverPassword(string email, string baseLinkPath)
		{
			Check.Require(!string.IsNullOrEmpty(email));

			var employee = GetByEmail(email);
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

			SetEnableSsl(client);

			client.Send(msg);
		}

		private static void SetEnableSsl(SmtpClient client)
		{
			if (ConfigurationManager.AppSettings.AllKeys.Contains("EnableSSLForMails"))
				client.EnableSsl = ConfigurationManager.AppSettings["EnableSSLForMails"].ToBoolean();
		}

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

		#endregion
	}
}
