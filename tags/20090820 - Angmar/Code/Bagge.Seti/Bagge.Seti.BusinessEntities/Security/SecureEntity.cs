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
	public class SecureEntity: PrimaryKeyDomainObject<SecureEntity, int>
	{
		[BelongsTo("FunctionId")]
		public Function Function
		{
			get;
			set;
		}

		[Property]
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

		public Type TargetType
		{
			get
			{
				return Type.GetType(Assembly.CreateQualifiedName(AssemblyName, ClassFullQualifiedName));
			}
		}
	}
}
