using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Bagge.Seti.DataAccess.Reports;
using Bagge.Seti.BusinessEntities;

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
			Assert.IsNotNull(data.ReportData);
			Assert.IsTrue(data.ReportData.Rows.Count >= 0);
		}

		[Test]
		public void Test_should_get_customers_by_ticket_report_without_dates()
		{
			var dao = new CustomersByTicketReportDao();
			var data = dao.GetReport(null);
			Assert.IsNotNull(data);
			Assert.IsNotNull(data.ReportData);
			Assert.IsTrue(data.ReportData.Rows.Count >= 0);
		}

		[Test]
		public void Test_should_get_customers_by_ticket_report_with_dates()
		{
			var dao = new CustomersByTicketReportDao();
			var filters = new List<FilterPropertyValue>();
			filters.Add("DateFrom", new DateTime());
			filters.Add("DateTo", new DateTime());
			var data = dao.GetReport(filters);
			Assert.IsNotNull(data);
			Assert.IsNotNull(data.ReportData);
			Assert.IsTrue(data.ReportData.Rows.Count >= 0);
		}

		[Test]
		public void Test_should_get_products_consumed_report_with_dates()
		{
			var dao = new ProductsConsumedReportDao();
			var filters = new List<FilterPropertyValue>();
			filters.Add("DateFrom", new DateTime());
			filters.Add("DateTo", new DateTime());
			var data = dao.GetReport(filters);
			Assert.AreEqual(new TimeSpan(23, 59, 59), ((DateTime)filters.GetFilter("DateTo").Value).TimeOfDay);
			Assert.IsNotNull(data);
			Assert.IsNotNull(data.ReportData);
			Assert.IsTrue(data.ReportData.Rows.Count >= 0);
		}
	}
}
