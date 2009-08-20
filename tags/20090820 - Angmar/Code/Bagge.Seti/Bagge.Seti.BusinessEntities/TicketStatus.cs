
using Castle.ActiveRecord;
using System;
using Bagge.Seti.BusinessEntities.Security;
namespace Bagge.Seti.BusinessEntities
{
	[ActiveRecord]
	[Serializable]
	[Securizable("Securizable_TicketStatus", typeof(TicketStatus))]
	public partial class TicketStatus : PrimaryKeyWithNameAndDescriptionDomainObject<TicketStatus, int>, IEquatable<TicketStatusEnum>
	{
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (obj is TicketStatusEnum)
				return Equals((TicketStatusEnum)obj);

			return base.Equals(obj);
		}

		#region IEquatable<TicketStatusEnum> Members

		public bool Equals(TicketStatusEnum other)
		{
			return Id.Equals((int)other);
		}

		#endregion

		public static bool operator ==(TicketStatus status, TicketStatusEnum statusEnum)
		{
			if (status == null)
				return false;

			return status.Equals(statusEnum);
		}

		public static bool operator !=(TicketStatus status, TicketStatusEnum statusEnum)
		{
			if (status == null)
				return true;

			return !status.Equals(statusEnum);
		}
	}

	public enum TicketStatusEnum
	{
		Initial = 1,
		Open = 2,
		PendingAproval = 3,
		PendingPayment = 4,
		Expired = 5,
		Closed = 6,
		Canceled = 7
	}
}
