﻿
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
		#region IEquatable<TicketStatusEnum> Members

		public bool Equals(TicketStatusEnum other)
		{
			return Id.Equals((int)other);
		}

		#endregion
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