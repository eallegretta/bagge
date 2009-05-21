using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Bagge.Seti.DataAccess.Reports;

namespace Bagge.Seti.DataAccess.Tests
{
	[TestFixture]
	public class ReportsTestFixture: BaseTestFixture
	{
		[Test]
		public void Test_should_get_customers_by_subscription_report()
		{
			var dao = new CustomersBySubscriptionReportDao();
			var data = dao.GetReport(null);
			Assert.IsNotNull(data);
			Assert.IsTrue(data.Count >= 0);
		}
	}
}
