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
			get { return GetObject<IAuthenticator>("AuthenticationManager").LoggedInUser; }
			set { GetObject<IAuthenticator>("AuthenticationManager").LoggedInUser = value; }
		}


		private static T GetObject<T>(string key) where T:class
		{
			return ContextRegistry.GetContext().GetObject(key) as T;
		}

		public static IAlertConfigurationManager AlertConfigurationManager
		{
			get { return GetObject<IAlertConfigurationManager>("AlertConfigurationManager"); }
		}

		public static IAccessibilityTypeManager AccessibilityTypeManager
		{
			get { return GetObject<IAccessibilityTypeManager>("AccessibilityTypeManager"); }
		}

		public static ISimpleFindGetManager<CountryState, int> CountryStateManager
		{
			get { return GetObject<ISimpleFindGetManager<CountryState, int>>("CountryStateManager"); }
		}

		public static ICustomerManager CustomerManager
		{
			get { return GetObject<ICustomerManager>("CustomerManager"); }
		}

		public static ISimpleFindGetManager<District, int> DistrictManager
		{
			get { return GetObject<ISimpleFindGetManager<District, int>>("DistrictManager"); }
		}

		public static IEmployeeManager EmployeeManager
		{
			get { return GetObject<IEmployeeManager>("EmployeeManager"); }
		}

		public static ISimpleFindGetManager<EmployeeCategory, int> EmployeeCategoryManager
		{
			get { return GetObject<ISimpleFindGetManager<EmployeeCategory, int>>("EmployeeCategoryManager"); }
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

		public static ISecurityManager SecurityManager
		{
			get { return GetObject<ISecurityManager>("SecurityManager"); }
		}

		public static ISimpleFindGetManager<ProviderCalification, int> ProviderCalificationManager
		{
			get { return GetObject<ISimpleFindGetManager<ProviderCalification, int>>("ProviderCalificationManager"); }
		}

		public static IReportManager ReportManager
		{
			get { return GetObject<IReportManager>("ReportManager"); }
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

		public static IStorage Storage
		{
			get { return GetObject<IStorage>("Storage"); }
		}
		
	}
}
