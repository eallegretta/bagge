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
using Castle.Components.Validator;
using Bagge.Seti.Common.Validation;

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


		private static T GetObject<T>(string key) where T:class
		{
			return ContextRegistry.GetContext().GetObject(key) as T;
		}

		public static IAlertConfigurationManager AlertConfigurationManager
		{
			get { return GetObject<IAlertConfigurationManager>("AlertConfigurationManager"); }
		}

		public static IManager<CountryState, int> CountryStateManager
		{
			get { return GetObject<IManager<CountryState, int>>("CountryStateManager"); }
		}

		public static ICustomerManager CustomerManager
		{
			get { return GetObject<ICustomerManager>("CustomerManager"); }
		}

		public static IManager<District, int> DistrictManager
		{
			get { return GetObject<IManager<District, int>>("DistrictManager"); }
		}

		public static IEmployeeManager EmployeeManager
		{
			get { return GetObject<IEmployeeManager>("EmployeeManager"); }
		}

		public static IFunctionManager FunctionManager
		{
			get { return GetObject<IFunctionManager>("FunctionManager"); }
		}

		public static IProductManager ProductManager
		{
			get { return GetObject<IProductManager>("ProductManager"); }
		}


		public static IProviderManager ProviderManager
		{
			get { return GetObject<IProviderManager>("ProviderManager"); }
		}


		public static IManager<ProviderCalification, int> ProviderCalificationManager
		{
			get { return GetObject<IManager<ProviderCalification, int>>("ProviderCalificationManager"); }
		}

		public static IRoleManager RoleManager
		{
			get { return GetObject<IRoleManager>("RoleManager"); }
		}

		public static ITicketManager TicketManager
		{
			get { return GetObject<ITicketManager>("TicketManager"); }
		}

		public static ITicketStatusManager TicketStatusManager
		{
			get { return GetObject<ITicketStatusManager>("TicketStatusManager"); }
		}

		public static IValidationEngine ValidationEngine
		{
			get { return GetObject<IValidationEngine>("ValidationEngine"); }
		}
		
	}
}
