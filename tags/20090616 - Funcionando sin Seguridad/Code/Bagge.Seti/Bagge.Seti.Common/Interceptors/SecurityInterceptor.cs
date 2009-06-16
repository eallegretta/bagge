using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AopAlliance.Intercept;
using Bagge.Seti.Security.BusinessEntities;
using System.Collections;

namespace Bagge.Seti.Common.Interceptors
{
	public class SecurityInterceptor: IMethodInterceptor
	{
		#region IMethodInterceptor Members

		public object Invoke(IMethodInvocation invocation)
		{
			object returnValue = invocation.Proceed();

			if (returnValue is ISecurizable)
				ApplySecurityRestrictions((ISecurizable)returnValue);
			else if (returnValue is IEnumerable)
				ApplySecurityRestrictions((IEnumerable)returnValue);

			return returnValue;
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
