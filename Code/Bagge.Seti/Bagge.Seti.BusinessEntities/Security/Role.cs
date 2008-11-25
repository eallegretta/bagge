using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;
using Castle.ActiveRecord;

namespace Bagge.Seti.Security.BusinessEntities
{
	[ActiveRecord]
	public class Role : PrimaryKeyWithNameDomainObject<Role, int>
	{
		[HasAndBelongsToMany(Table= "RoleEmployee", ColumnKey="RoleId", ColumnRef="EmployeeId", Lazy=true)]
		public virtual IList<Employee> Employees
		{
			get;
			set;
		}

		[HasAndBelongsToMany(Table = "RoleFunction", ColumnKey="RoleId", ColumnRef="FunctionId", Lazy=true)]
		public virtual IList<Function> Functions
		{
			get;
			set;
		}
	}
}
