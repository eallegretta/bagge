using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Bagge.Seti.DataAccess.ActiveRecord;
using System.Diagnostics;

namespace Bagge.Seti.DataAccess.Tests
{
	[TestFixture]
	public class ProductProviderDaoTestFixture: BaseTestFixture
	{
		[Test]
		public void Test_should_retrieve_ids_and_quantities()
		{
			ProductProviderDao dao = new ProductProviderDao();
			var items = dao.FindProductsInIdsByAggregation(new int[] { 3 }, "max");
			foreach (var item in items)
				Debug.WriteLine(string.Format("Id: {0}, Product: {1}, Price: {2}", item.Id, item.Product, item.Price));
		}
	}
}
