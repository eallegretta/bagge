using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.DataAccess.Contracts;
using Bagge.Seti.BusinessEntities;
using Castle.ActiveRecord;

namespace Bagge.Seti.DataAccess.ActiveRecord
{
	public class TicketStatusDao: ITicketStatusDao
	{
		public TicketStatus[] FindAll()
		{
			return ActiveRecordMediator<TicketStatus>.FindAll();
		}

		public TicketStatus Get(int id)
		{
			return ActiveRecordMediator<TicketStatus>.FindByPrimaryKey(id);
		}
	}
}
