using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.BusinessEntities.CustomTypes;

namespace Bagge.Seti.Security.BusinessEntities
{
	[Serializable]
	public class AuditablePrimaryKeyWithNameAndDescriptionDomainObject<T, PK>: PrimaryKeyWithNameAndDescriptionDomainObject<T, PK>, IAuditable
	{
		#region IAuditable Members

		[Property]
		public string AuditUserName
		{
			get;
			set;
		}

		[Property(Insert = false, Update = false)]
		public byte[] AuditTimeStamp
		{
			get;
			set;
		}

		[Property]
		public bool Deleted
		{
			get;
			set;
		}

		#endregion
	}
}
