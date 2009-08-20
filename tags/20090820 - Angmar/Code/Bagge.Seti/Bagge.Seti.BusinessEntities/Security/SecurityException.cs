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

		[BelongsTo("RoleId")]
		public Role Role
		{
			get;
			set;
		}

		[BelongsTo("SecureEntityId")]
		public SecureEntity SecureEntity
		{
			get;
			set;
		}

		[Property]
		public string PropertyName
		{
			get;
			set;
		}

	}
}
