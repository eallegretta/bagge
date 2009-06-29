using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Aop;
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.Common.Interceptors
{
	public class AuditInterceptor: IMethodBeforeAdvice
	{
		#region IMethodBeforeAdvice Members

		public void Before(System.Reflection.MethodInfo method, object[] args, object target)
		{
			if (args != null && method.Name.ToUpper().In<string>("CREATE", "UPDATE", "DELETE"))
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

		#endregion
	}
}
