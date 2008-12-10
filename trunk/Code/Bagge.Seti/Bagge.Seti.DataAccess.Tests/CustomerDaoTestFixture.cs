using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.DataAccess.ActiveRecord;

namespace Bagge.Seti.DataAccess.Tests
{
	[TestFixture]
	public class CustomerDaoTestFixture: BaseTestFixture
	{
		[Test]
		public void TestShouldCreateAndDeleteACustomer()
		{
			var dao = new GenericDao<Customer, int>();
			var customer = new Customer();
		}

		[Test]
		public void TestShouldFindAllCustomers()
		{
			var dao = new GenericDao<Customer, int>();
			Assert.IsTrue(dao.FindAll() != null);
		}
	}
}
