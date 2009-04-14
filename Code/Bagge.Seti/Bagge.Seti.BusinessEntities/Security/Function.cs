using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;
using Castle.ActiveRecord;
using System.Reflection;

namespace Bagge.Seti.Security.BusinessEntities
{
	[ActiveRecord("[Function]")]
	[Serializable]
	public class Function : AuditablePrimaryKeyWithNameDomainObject<Function, int>
	{
		public Type TargetType
		{
			get
			{
				return Type.GetType(Assembly.CreateQualifiedName(AssemblyName, ClassFullQualifiedName));
			}
		}

		[Property("Assembly")]
		public string AssemblyName
		{
			get;
			set;
		}

		[Property]
		public string ClassFullQualifiedName
		{
			get;
			set;
		}

		[Property]
		public string MemberName
		{
			get;
			set;
		}

		[Property]
		public char MemberType
		{
			get;
			set;
		}

		[BelongsTo("AccessibilityTypeId")]
		public AccessibilityType Accessibility
		{
			get;
			set;
		}

		[Property]
		public char ConstraintType
		{
			get;
			set;
		}

		[Property]
		public string Value
		{
			get;
			set;
		}

		[HasAndBelongsToMany(Table="RoleFunction", ColumnKey="FunctionId", ColumnRef="RoleId")]
		public virtual IList<Role> Roles
		{
			get; set;
		}
	}
}
