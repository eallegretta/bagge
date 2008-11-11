using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Bagge.Seti.BusinessEntities;
using Spring.Context.Support;
using Bagge.Seti.Common;

namespace Bagge.Seti.BusinessLogic.Tests
{
	[TestFixture]
	public class CustomerManagerTestFixture: BaseTestFixture
	{

		[Test]
		public void TestShouldFindAllCustomers()
		{
			Assert.IsTrue(SpringContext.CustomerManager.FindAll().Length > 0);
		}
	}
}
