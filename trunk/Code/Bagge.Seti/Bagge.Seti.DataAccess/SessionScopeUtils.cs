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
		public static IDisposable NewSessionScope()
		{
			return new SessionScope();
		}

		public static bool InSessionScope
		{
			get
			{
				return SessionScope.Current != null || TransactionScope.Current != null;
			}
		}

		public static void FlushSessionScope()
		{
			try
			{
				ISessionScope scope = ThreadScopeAccessor.Instance.GetRegisteredScope();
				if (scope != null && scope.FlushAction != FlushAction.Never)
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
