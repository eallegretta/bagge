using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Bagge.Seti.DataAccess.Contracts;
using Bagge.Seti.DataAccess.ActiveRecord;
using System.Diagnostics;
using Bagge.Seti.BusinessEntities.Security;

namespace Bagge.Seti.DataAccess.Tests
{
	[TestFixture]
	public class SecurityTestFixture: BaseTestFixture
	{
		[Test]
		public void Test_should_list_all_secure_entities_for_all_functions()
		{
			IFunctionDao functionDao = new FunctionDao();
			ISecureEntityDao secureEntityDao = new SecureEntityDao();

			var functions = functionDao.FindAll("Name", true);
			Assert.IsTrue(functions.Length > 0);

			foreach (var function in functions)
			{
				Debug.WriteLine(function.Name);
				Debug.Indent();
				foreach (var entity in function.SecureEntities)
				{
					Debug.WriteLine(SecurizableAttribute.GetName(entity.TargetType));
					Debug.Indent();
					foreach (var property in SecurizableAttribute.GetSecurizableProperties(entity.TargetType))
					{
						Debug.WriteLine(property.Name + " - " + property.Property.Name);
					}
					Debug.Unindent();
				}
				Debug.Unindent();
			}
		}
	}
}
