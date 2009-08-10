using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AopAlliance.Intercept;
using Bagge.Seti.Security.BusinessEntities;
using System.Collections;
using Bagge.Seti.BusinessEntities.Security;

namespace Bagge.Seti.Common.Interceptors
{
	public class SecurityInterceptor: IMethodInterceptor
	{
		#region IMethodInterceptor Members

		public object Invoke(IMethodInvocation invocation)
		{
			IsInvocationAllowed(invocation);

			object returnValue = invocation.Proceed();

			if (returnValue is ISecurizable)
				ApplySecurityRestrictions((ISecurizable)returnValue);
			else if (returnValue is IEnumerable)
				ApplySecurityRestrictions((IEnumerable)returnValue);

			return returnValue;
		}

		private bool IsInvocationAllowed(IMethodInvocation invocation)
		{
			if (!invocation.Method.IsDefined(typeof(SecurizableAttribute), true))
				return true;

			var function = IoCContainer.Storage.GetData(typeof(Function)) as Function;

			if (function == null)
				return true;

			var user = IoCContainer.User.Identity as IUser;

			return false;
		}

		private void ApplySecurityRestrictions(IEnumerable securizables)
		{
			IEnumerator en = securizables.GetEnumerator();
			while (en.MoveNext())
			{
				if(en.Current is ISecurizable)
					ApplySecurityRestrictions((ISecurizable)en.Current);
			}
		}
		private void ApplySecurityRestrictions(ISecurizable securizable)
		{
		}

		#endregion
	}
}
