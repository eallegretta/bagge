﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.BusinessLogic.Contracts
{
	public interface ITicketManager: IManager<Ticket, int>
	{
		Ticket[] FindAllByStatus(TicketStatusEnum status);
		Ticket[] FindAllByProduct(int productId);
		Ticket[] FindAllByProvider(int providerId);
		int CreateApproved(Ticket ticket);
		void UpdateProgress(int ticketId, decimal realDuration, string notes);
		void Close(int ticketId);

        Ticket[] FindAllByExecutionWeek(DateTime weekStartDate, DateTime weekEndDate);
        Ticket[] FindAllByExecutionWeekAndTechnician(DateTime weekStartDate, DateTime weekEndDate, int technicianId);
		string EmailUrl { set; get; }
	}
}
