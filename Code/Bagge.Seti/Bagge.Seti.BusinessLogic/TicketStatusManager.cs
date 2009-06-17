using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.DataAccess.Contracts;

namespace Bagge.Seti.BusinessLogic
{
	public partial class TicketStatusManager : ITicketStatusManager
	{

		private ITicketStatusDao _dao;

		public TicketStatusManager(ITicketStatusDao dao)
		{
			_dao = dao;
		}

		#region ITicketStatusManager Members

		public TicketStatus Get(TicketStatusEnum status)
		{
			return Get((int)status);
		}

		public TicketStatus[] FindAll()
		{
			return _dao.FindAll();
		}

		#endregion

		#region IGetManager<TicketStatus,int> Members

		public TicketStatus Get(int id)
		{
			return _dao.Get(id);
		}

		#endregion
	}
}
