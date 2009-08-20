﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.DataAccess.Contracts
{
	public interface ITicketDao: IDao<Ticket, int>
	{
		void DeleteProducts(int ticketId);
		Ticket[] FindAllByProduct(int productId);
		Ticket[] FindAllByProvider(int providerId);
	}
}
