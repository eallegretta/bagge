using System;
using Castle.ActiveRecord;

namespace Bagge.Seti.BusinessEntities
{
	[Serializable]
	public class PrimaryKeyDomainObject<T, PK>: IEquatable<PrimaryKeyDomainObject<T, PK>>
	{
		public PrimaryKeyDomainObject():this(default(PK))
		{
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
	}
}
