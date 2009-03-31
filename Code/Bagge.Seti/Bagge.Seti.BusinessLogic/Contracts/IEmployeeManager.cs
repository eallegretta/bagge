using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.BusinessLogic.Contracts
{
	public interface IEmployeeManager: IAuditableManager<Employee, int>
	{
		bool Authenticate(string username, string password);
		Employee GetByUsername(string username);
	}
}
