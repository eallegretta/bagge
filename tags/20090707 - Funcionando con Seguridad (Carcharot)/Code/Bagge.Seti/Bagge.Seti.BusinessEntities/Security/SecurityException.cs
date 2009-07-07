using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
using Bagge.Seti.Security.BusinessEntities;
using System.Reflection;

namespace Bagge.Seti.BusinessEntities.Security
{
	[ActiveRecord]
	public class SecurityException: PrimaryKeyDomainObject<SecurityException, int>
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

		[BelongsTo("FunctionId")]
		public Function Function
		{
			get;
			set;
		}
	}
}
