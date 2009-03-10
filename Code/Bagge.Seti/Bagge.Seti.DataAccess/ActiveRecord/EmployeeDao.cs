using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.DataAccess.Contracts;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.DataAccess.ActiveRecord
{
	public class EmployeeDao : AuditableGenericDao<Employee, int>, IEmployeeDao
	{
	}
}
