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
			Check.Require(!username.IsNullOrEmpty());

			Employee[] employees = Dao.FindAllByProperty("Username", username);
			if (employees.Length > 1)
				throw new BusinessRuleException(Resources.MultipleUsernamesErrorMessage);
			else if (employees.Length == 1)
				return employees[0];
			else
				throw new ObjectNotFoundException(string.Format(Resources.EmployeeByUsernameNotFoundErrorMessage, username));
		
		}

		#endregion
	}
}
