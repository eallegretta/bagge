using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.DataAccess.ActiveRecord;
using Bagge.Seti.Security.BusinessEntities;

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
			var dao = new CustomerDao();
			Assert.IsNotNull(dao.FindAll());

			
		}
	}
}
