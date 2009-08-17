using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AopAlliance.Intercept;
using Castle.ActiveRecord;
using System.Threading;

namespace Bagge.Seti.Common.Interceptors
{
	public class SessionScopeInterceptor: IMethodInterceptor
	{
		#region IMethodInterceptor Members

		public object Invoke(IMethodInvocation invocation)
		{
			using (new SessionScope())
			{
				return invocation.Proceed();
			}
		}

		#endregion
	}
}
