using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.DataAccess.Contracts;

namespace Bagge.Seti.BusinessLogic
{
	public class EmployeeManager: GenericManager<Employee, int>, IEmployeeManager
	{
		public EmployeeManager(IEmployeeDao dao)
			: base(dao)
		{
		}
	}
}
