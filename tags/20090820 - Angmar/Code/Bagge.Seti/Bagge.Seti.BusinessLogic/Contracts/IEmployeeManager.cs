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
		Employee GetByEmail(string email);
		void RecoverPassword(string email, string baseLinkPath);
		void RegeneratePassword(string encodedKey);
		Employee[] FindAllActiveTechnicians();
		void UpdateProfile(Employee instance);
	}
}
