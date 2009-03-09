using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Bagge.Seti.BusinessEntities;
using Spring.Context.Support;
using Bagge.Seti.Common;
using System.Diagnostics;

namespace Bagge.Seti.BusinessLogic.Tests
{
	[TestFixture]
	public class CustomerManagerTestFixture : BaseTestFixture
	{

		public void Test()
		{
			Debug.WriteLine("test".ToMD5());
		}

		[Test]
		public void TestShouldFindAllCustomers()
		{
			Assert.IsTrue(IoCContainer.CustomerManager.FindAll().Length > 0);
		}
	}
}
