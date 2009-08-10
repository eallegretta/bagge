using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Bagge.Seti.DataAccess.ActiveRecord;
using System.Diagnostics;
using Bagge.Seti.BusinessEntities.Security;
using System.Reflection;
using Bagge.Seti.WebSite;

namespace Bagge.Seti.BusinessLogic.Tests
{
	[TestFixture]
	public class FunctionManagerTestFixture: BaseTestFixture
	{
		[Test]
		public void Test_should_return_all_securizable_assemblies()
		{
			var assembly = Assembly.LoadFile(Environment.CurrentDirectory + "\\Bagge.Seti.WebSite.dll");
			foreach (var type in assembly.GetTypes())
			{
				if (type.IsDefined(typeof(SecurizableCrudAttribute), true))
				{
					var attr = type.GetCustomAttributes(typeof(SecurizableCrudAttribute), true)[0] as SecurizableCrudAttribute;
					Debug.WriteLine(string.Format("{0} - {1} - {2}",
						type.FullName, attr.Name, attr.Action));
				}
			}
		}
	}
}
