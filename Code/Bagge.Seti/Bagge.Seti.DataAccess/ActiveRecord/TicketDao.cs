using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.DataAccess.Contracts;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;

namespace Bagge.Seti.DataAccess.ActiveRecord
{
	public class TicketDao : GenericDao<Ticket, int>, ITicketDao
	{
		public override void Update(Ticket instance)
		{
			SessionScopeUtils.FlushSessionScope();

			base.Update(instance);
		}

		#region ITicketDao Members

		public void DeleteProducts(int ticketId)
		{
			ActiveRecordMediator<ProductTicket>.DeleteAll("TicketId = " + ticketId);
		}

		public Ticket[] FindAllByProduct(int productId)
		{
			string hql = "from Ticket ticket inner join fetch ticket.Products prod inner join fetch prod.ProductProvider pp where pp.Product.Id = ?";
			var query = new SimpleQuery<Ticket>(hql, productId);
			return query.Execute();
		}

		public Ticket[] FindAllByProvider(int providerId)
		{
			string hql = "from Ticket ticket inner join fetch ticket.Products prod inner join fetch prod.ProductProvider pp where pp.Provider.Id = ?";
			var query = new SimpleQuery<Ticket>(hql, providerId);
			return query.Execute();
		}

		#endregion
	}
}
