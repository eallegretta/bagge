using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.DataAccess.ActiveRecord;
using Bagge.Seti.Security.BusinessEntities;
using Rhino.Mocks;
using Bagge.Seti.DataAccess.Contracts;

namespace Bagge.Seti.DataAccess.Tests
{
	[TestFixture]
	public class CustomerDaoTestFixture: BaseTestFixture
	{
		[Test]
		public void TestShouldCreateAndDeleteACustomer()
		{
			
			
		}

		[Test]
		public void TestShouldFindAllCustomers()
		{
			MockRepository mocks = new MockRepository();
			ICustomerDao dao = mocks.StrictMock<ICustomerDao>();
			using (mocks.Record())
			{
				Expect.Call(dao.FindAll(null, null)).Return(new Customer[] { new Customer(), new Customer() });
			}
			using (mocks.Playback())
			{
				var customers = dao.FindAll(null, null);
				Assert.IsNotNull(customers);
				Assert.IsTrue(customers.Length > 0);
			}

			
		}
	}
}
