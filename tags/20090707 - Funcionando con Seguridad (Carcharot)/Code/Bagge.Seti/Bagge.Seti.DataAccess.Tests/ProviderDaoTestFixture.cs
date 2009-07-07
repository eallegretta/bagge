using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Castle.ActiveRecord.Queries;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.DataAccess.ActiveRecord;

namespace Bagge.Seti.DataAccess.Tests
{
	[TestFixture]
	public class ProviderDaoTestFixture: BaseTestFixture
	{
		[Test]
		public void TestShouldRetrieveFilteredProviders()
		{
			var product = new ProductDao().Get(1);


			string hql = "from Provider p where ? in elements(p.Products)";
			SimpleQuery<Provider> query = new SimpleQuery<Provider>(hql, product);

			Assert.IsTrue(query.Execute().Length > 0);

		}
	}
}
