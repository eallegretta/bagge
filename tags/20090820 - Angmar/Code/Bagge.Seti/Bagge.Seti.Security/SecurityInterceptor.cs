using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AopAlliance.Intercept;

namespace Bagge.Seti.Security
{
	public class SecurityInterceptor: IMethodInterceptor
	{
		#region IMethodInterceptor Members

		public object Invoke(IMethodInvocation invocation)
		{
			invocation.Proceed();

			return invocation.Proceed();
		}

		#endregion
	}
}
