using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessLogic;
using Bagge.Seti.BusinessEntities;
using Spring.Context.Support;
using Bagge.Seti.BusinessLogic.Contracts;

namespace Bagge.Seti.Common
{
	public static class SpringContext
	{
		private static IManager<Customer, int> _customerManager = (IManager<Customer, int>)ContextRegistry.GetContext().GetObject("CustomerManager");

		public static IManager<Customer, int> CustomerManager
		{
			get { return _customerManager; }
		}

		public static IAlertConfigurationManager AlertConfigurationManager
		{
			get { return null; }
		}
	}
}
