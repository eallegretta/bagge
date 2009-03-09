using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessLogic;
using Bagge.Seti.BusinessEntities;
using Spring.Context.Support;
using Bagge.Seti.BusinessLogic.Contracts;
using System.Security.Principal;
using System.Web;
using Spring.Context;

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

		public static IPrincipal User
		{
			get { return HttpContext.Current.User; }
			set { HttpContext.Current.User = value; }
		}

		private static ICustomerManager _customerManager = (ICustomerManager)ContextRegistry.GetContext().GetObject("CustomerManager");
		private static IEmployeeManager _employeeManager = (IEmployeeManager)ContextRegistry.GetContext().GetObject("EmployeeManager");
		private static IManager<CountryState, int> _countryStateManager = (IManager<CountryState, int>)ContextRegistry.GetContext().GetObject("CountryStateManager");
		private static IManager<District, int> _districtManager = (IManager<District, int>)ContextRegistry.GetContext().GetObject("DistrictManager");

		public static IManager<CountryState, int> CountryStateManager
		{
			get {

				return _countryStateManager; }
		}

		public static IManager<District, int> DistrictManager
		{
			get { return _districtManager; }
		}

		public static ICustomerManager CustomerManager
		{
			get { return _customerManager; }
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
			get { return _employeeManager; }
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
