using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Aop;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.BusinessEntities.Security;

namespace Bagge.Seti.Common.Interceptors
{
	public class AuditInterceptor: IMethodBeforeAdvice
	{
		#region IMethodBeforeAdvice Members

		public void Before(System.Reflection.MethodInfo method, object[] args, object target)
		{
			if (args != null && IsCreateUpdateOrDelete(method))
			{
				foreach (object obj in args)
				{
					if (obj is IAuditable)
					{
						if (IoCContainer.User != null && 
							IoCContainer.User.Identity != null && 
							IoCContainer.User.Identity.IsAuthenticated)
						{
							((IAuditable)obj).AuditUserName = IoCContainer.User.Identity.Name;
						}
					}
				}
			}
		}

		private bool IsCreateUpdateOrDelete(System.Reflection.MethodInfo method)
		{
			if(!method.IsDefined(typeof(SecurizableCrudAttribute), true))
				return false;

			var attr = (SecurizableCrudAttribute)method.GetCustomAttributes(typeof(SecurizableCrudAttribute), true)[0];

			return attr.Action.In(FunctionAction.Create, FunctionAction.Update, FunctionAction.Delete);
		}

		#endregion
	}
}
