using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;
using Castle.ActiveRecord;

namespace Bagge.Seti.Security.BusinessEntities
{
	[ActiveRecord]
	public class Function : PrimaryKeyWithNameDomainObject<Function, int>
	{
		[HasAndBelongsToMany(Table="RoleFunction", ColumnKey="FunctionId", ColumnRef="RoleId")]
		public virtual IList<Role> Roles
		{
			get; set;
		}
	}
}
