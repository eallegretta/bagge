using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.DataAccess.Contracts;
using Bagge.Seti.BusinessEntities;
using Castle.ActiveRecord;

namespace Bagge.Seti.DataAccess.ActiveRecord
{
	public class TicketHistoryDao: ITicketHistoryDao
	{
		#region ICreateDao<TicketHistory,int> Members

		public int Create(TicketHistory instance)
		{
			ActiveRecordMediator<TicketHistory>.Create(instance);
			return instance.Id;
		}

		#endregion
	}
}
