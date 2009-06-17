using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;
using Castle.ActiveRecord;
using Bagge.Seti.BusinessEntities.Properties;
using Bagge.Seti.BusinessEntities.Validators;

namespace Bagge.Seti.Security.BusinessEntities
{
	[ActiveRecord]
	[Serializable]
	public partial class Role : AuditablePrimaryKeyWithNameAndDescriptionDomainObject<Role, int>
	{
		[RequiredStringValidator(MessageTemplateResourceName = "Validators_Role_Description", MessageTemplateResourceType = typeof(Role))]
		public override string Description
		{
			get
			{
				return base.Description;
			}
			set
			{
				base.Description = value;
			}
		}

		public static int SuperAdministratorId
		{
			get
			{
				return Settings.Default.SuperAdministratorRoleId;
			}
		}

		public bool IsSuperAdministratorRole
		{
			get { return Id == SuperAdministratorId; }
		}

		[HasAndBelongsToMany(Table = "RoleEmployee", ColumnKey = "RoleId", ColumnRef = "EmployeeId", Lazy = true)]
		public virtual IList<Employee> Employees
		{
			get;
			set;
		}

		[HasAndBelongsToMany(Table = "RoleFunction", ColumnKey = "RoleId", ColumnRef = "FunctionId", Lazy = true)]
		public virtual IList<Function> Functions
		{
			get;
			set;
		}
	}
}
