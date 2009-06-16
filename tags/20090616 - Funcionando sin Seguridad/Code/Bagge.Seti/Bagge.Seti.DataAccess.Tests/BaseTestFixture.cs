using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Castle.ActiveRecord.Framework;
using Castle.ActiveRecord.Framework.Config;
using System.Reflection;
using Castle.ActiveRecord;

namespace Bagge.Seti.DataAccess.Tests
{
	public class BaseTestFixture
	{
		[TestFixtureSetUp]
		public void SetUp()
		{
			IConfigurationSource config = ActiveRecordSectionHandler.Instance;
			Assembly asm = Assembly.Load("Bagge.Seti.BusinessEntities");
			ActiveRecordStarter.Initialize(asm, config);
		}
	}
}
