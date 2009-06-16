using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Scopes;

namespace Bagge.Seti.DataAccess
{
	public static class SessionScopeUtils
	{
		public static void FlushSessionScope()
		{
			try
			{
				ISessionScope scope = ThreadScopeAccessor.Instance.GetRegisteredScope();
				if (scope != null)
				{
					scope.Flush();
					scope.Dispose();
				}
			}
			catch (ScopeMachineryException)
			{
			}
		}
	}
}
