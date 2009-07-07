using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Diagnostics;

namespace Bagge.Seti.Extensions.Tests
{
	[TestFixture]
	public class TypeExtensionsTest
	{
		private class Parent
		{
		}

		private class Child: Parent
		{

			public void t()
			{
				Debug.Write("test".ToMD5());
			}
		}


		[Test]
		public void TypeShouldBeInt()
		{
			int value = 0;
			Assert.IsTrue(value.GetType().IsOfType(typeof(int)));

			object value2 = new Int32();

			Assert.IsTrue(value.GetType().IsOfType(typeof(int)));

			Assert.IsFalse(value.GetType().IsOfType(typeof(byte)));
		}

		[Test]
		public void TypeShouldBeOfClassParent()
		{
			Parent parent = new Parent();
			Parent child = new Child();

			Assert.IsTrue(parent.GetType().IsOfType(typeof(Parent)));
			Assert.IsTrue(child.GetType().IsOfType(typeof(Parent)));

			Assert.IsFalse(parent.GetType().IsOfType(typeof(Child)));
		}
	}
}
