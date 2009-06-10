using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Bagge.Seti.DataAccess.Contracts;
using Bagge.Seti.DataAccess.ActiveRecord;

namespace Bagge.Seti.DataAccess.Tests
{
	[TestFixture]
	public class TicketDaoTests: BaseTestFixture
	{
		[Test]
		public void Test_should_find_all_tickets_by_product()
		{
			ITicketDao dao = new TicketDao();

			Assert.IsTrue(dao.FindAllByProduct(1).Length > 0);
			Assert.IsTrue(dao.FindAllByProduct(3).Length > 0);
			Assert.IsTrue(dao.FindAllByProduct(56).Length == 0);
		}

		[Test]
		public void Test_should_find_all_tickets_by_provider()
		{
			ITicketDao dao = new TicketDao();

			Assert.IsTrue(dao.FindAllByProvider(1).Length > 0);
			Assert.IsTrue(dao.FindAllByProvider(3).Length > 0);
			Assert.IsTrue(dao.FindAllByProvider(56).Length == 0);
		}
	}
}
