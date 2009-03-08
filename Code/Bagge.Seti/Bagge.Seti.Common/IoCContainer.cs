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
	public static class IoCContainer
	{
		public static T Resolve<T>()
		{
			foreach (T obj in ContextRegistry.GetContext().GetObjectsOfType(typeof(T)).Values)
				return obj;
			return default(T);
		}

		//private static ICustomerManager _customerManager = (ICustomerManager)ContextRegistry.GetContext().GetObject("CustomerManager");

		public static ICustomerManager CustomerManager
		{
			get { return (ICustomerManager)ContextRegistry.GetContext().GetObject("CustomerManager"); }
		}

		public static IAlertConfigurationManager AlertConfigurationManager
		{
			get { return null; }
		}

		public static IProductManager ProductManager
		{
			get { return null; }
		}

		public static IEmployeeManager EmployeeManager
		{
			get { return null; }
		}

		public static ITicketManager TicketManager
		{
			get { return null; }
		}

		public static ITicketStatusManager TicketStatusManager
		{
			get { return null; }
		}

		public static IFunctionManager FunctionManager
		{
			get { return null; }
		}

		public static IRoleManager RoleManager
		{
			get { return null; }
		}
	}
}
