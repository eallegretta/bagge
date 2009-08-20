using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.DataAccess.Contracts;
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.BusinessLogic
{
	[Securizable("Securizable_TicketStatusManager", typeof(TicketStatusManager))]
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

		[SecurizableCrud("Securizable_TicketStatusManager_FindAll", typeof(TicketStatusManager), FunctionAction.Retrieve)]
		public TicketStatus[] FindAll()
		{
			return _dao.FindAll();
		}

		#endregion

		#region IGetManager<TicketStatus,int> Members

		[SecurizableCrud("Securizable_TicketStatusManager_Get", typeof(TicketStatusManager), FunctionAction.Retrieve)]
		public TicketStatus Get(int id)
		{
			return _dao.Get(id);
		}

		#endregion
	}
}
