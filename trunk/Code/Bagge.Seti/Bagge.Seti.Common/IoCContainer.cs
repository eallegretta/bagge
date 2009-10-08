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
using Microsoft.Practices.ServiceLocation;
using CommonServiceLocator.SpringAdapter;

namespace Bagge.Seti.Common
{
	public static class IoCContainer
	{

		static IoCContainer()
		{
			ServiceLocator.SetLocatorProvider(() => new SpringServiceLocatorAdapter(ContextRegistry.GetContext()));
		}

		public static IServiceLocator Locator
		{
			get
			{
				return ServiceLocator.Current;
			}
		}

		public static IPrincipal User
		{
			get { return Locator.GetInstance<IAuthenticator>().LoggedInUser; }
			set { Locator.GetInstance<IAuthenticator>().LoggedInUser = value; }
		}


		public static IAlertConfigurationManager AlertConfigurationManager
		{
			get { return Locator.GetInstance<IAlertConfigurationManager>(); }
		}

		public static IAccessibilityTypeManager AccessibilityTypeManager
		{
			get { return Locator.GetInstance<IAccessibilityTypeManager>(); }
		}

		public static ISimpleFindGetManager<CountryState, int> CountryStateManager
		{
			get { return Locator.GetInstance<ISimpleFindGetManager<CountryState, int>>(); }
		}

		public static ICustomerManager CustomerManager
		{
			get { return Locator.GetInstance<ICustomerManager>(); }
		}

		public static ISimpleFindGetManager<District, int> DistrictManager
		{
			get { return Locator.GetInstance<ISimpleFindGetManager<District, int>>(); }
		}

		public static IEmployeeManager EmployeeManager
		{
			get { return Locator.GetInstance<IEmployeeManager>(); }
		}

		public static ISimpleFindGetManager<EmployeeCategory, int> EmployeeCategoryManager
		{
			get { return Locator.GetInstance<ISimpleFindGetManager<EmployeeCategory, int>>(); }
		}

		public static IFunctionManager FunctionManager
		{
			get { return Locator.GetInstance<IFunctionManager>(); }
		}

		public static IProductManager ProductManager
		{
			get { return Locator.GetInstance<IProductManager>(); }
		}


		public static IProviderManager ProviderManager
		{
			get { return Locator.GetInstance<IProviderManager>(); }
		}

		public static ISecurityManager SecurityManager
		{
			get { return Locator.GetInstance<ISecurityManager>(); }
		}

		public static ISimpleFindGetManager<ProviderCalification, int> ProviderCalificationManager
		{
			get { return Locator.GetInstance<ISimpleFindGetManager<ProviderCalification, int>>(); }
		}

		public static IReportManager ReportManager
		{
			get { return Locator.GetInstance<IReportManager>(); }
		}

		public static IRoleManager RoleManager
		{
			get { return Locator.GetInstance<IRoleManager>(); }
		}

		public static ITicketManager TicketManager
		{
			get { return Locator.GetInstance<ITicketManager>(); }
		}

		public static ITicketStatusManager TicketStatusManager
		{
			get { return Locator.GetInstance<ITicketStatusManager>(); }
		}

		public static IValidationEngine ValidationEngine
		{
			get { return Locator.GetInstance<IValidationEngine>(); }
		}

		public static IStorage Storage
		{
			get { return Locator.GetInstance<IStorage>(); }
		}
		
	}
}
