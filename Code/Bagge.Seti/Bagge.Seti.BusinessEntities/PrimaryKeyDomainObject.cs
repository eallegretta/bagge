using System;
using Castle.ActiveRecord;
using Bagge.Seti.Security;
using System.Collections.Generic;
using Bagge.Seti.Security.Constraints;
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.BusinessEntities
{
	[Serializable]
	public class PrimaryKeyDomainObject<T, PK>: IEquatable<PrimaryKeyDomainObject<T, PK>>, ISecurizable
	{
		public PrimaryKeyDomainObject():this(default(PK))
		{
			IsAccessible = true;
		}
		public PrimaryKeyDomainObject(PK id)
		{
			Id = id;
		}

		
		[PrimaryKey]
		public virtual PK Id { get; set; }


		#region IEquatable<PrimaryKeyDomainObject> Members

		public bool Equals(PrimaryKeyDomainObject<T, PK> other)
		{
			return Id.Equals(other.Id);
		}

		#endregion

		#region ISecurizable Members

		public bool IsAccessible
		{
			get;
			set;
		}

		#endregion
	}
}
