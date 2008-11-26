using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.Security.BusinessEntities
{
	public class AuditablePrimaryKeyWithNameAndDescriptionDomainObject<T, PK>: PrimaryKeyWithNameAndDescriptionDomainObject<T, PK>, IAuditable
	{
		#region IAuditable Members

		[Property]
		public string AuditUserName
		{
			get;
			set;
		}

		[Property]
		public byte[] AuditTimeStamp
		{
			get;
			set;
		}

		#endregion
	}
}
