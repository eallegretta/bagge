using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AopAlliance.Intercept;
using Bagge.Seti.Security.BusinessEntities;
using System.Collections;
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.BusinessEntities.Exceptions;
using Bagge.Seti.Security.Constraints;

namespace Bagge.Seti.Common.Interceptors
{
	public class SecurityInterceptor: IMethodInterceptor
	{
		#region IMethodInterceptor Members


		private Function _currentFunction;
		private IUser _user;

		private SecurityException[] GetUserSecurityExceptions()
		{
			if (_currentFunction != null)
			{
				var exceptions = IoCContainer.Storage.GetData(typeof(SecurityException[])) as SecurityException[];
				if (exceptions == null)
					IoCContainer.Storage.SetData(typeof(SecurityException[]),
						IoCContainer.SecurityManager.FindAllSecurityExceptions(_user, _currentFunction.Id));

				return IoCContainer.Storage.GetData(typeof(SecurityException[])) as SecurityException[];
			}
			return null;
		}

		public object Invoke(IMethodInvocation invocation)
		{
			_currentFunction = IoCContainer.Storage.GetData(typeof(Function)) as Function;
			_user = IoCContainer.User.Identity as IUser;

			if (_user != null && !_user.IsSuperAdministrator)
			{
				if (!IsInvocationAllowed(invocation))
					throw new MethodAccessDeniedException();
			}

			object returnValue = invocation.Proceed();

			/*if (returnValue is IEnumerable)
				return GetAllowedObjects(returnValue, returnValue.GetType());*/
	
			return returnValue;
		}

		private object GetAllowedObjects(object objects, Type returnValueType)
		{
			object list;
			if (returnValueType.IsArray)
				list = new ArrayList();
			else if (objects is IList)
				list = Activator.CreateInstance(returnValueType);
			else
				return objects;

			IEnumerator en = ((IEnumerable)objects).GetEnumerator();
			while (en.MoveNext())
			{
				if (IoCContainer.SecurityManager.UserHasAccessToInstance(en.Current, GetUserSecurityExceptions()))
					((IList)list).Add(en.Current);

			}

			if (returnValueType.IsArray)
				return ((ArrayList)list).ToArray(returnValueType.GetElementType());
			else
				return list;
		}

		private bool IsInvocationAllowed(IMethodInvocation invocation)
		{

			if (!invocation.Method.IsDefined(typeof(SecurizableAttribute), true))
				return true;

			if (_currentFunction == null)
				return true;

			if(!IoCContainer.FunctionManager.UserHasAccessToFunction(_user, _currentFunction))
				return false;

			foreach (object instance in invocation.Arguments)
			{
				if (instance is ISecurizable)
				{
					if (!IoCContainer.SecurityManager.UserHasAccessToInstance(instance, GetUserSecurityExceptions()))
						return false;
				}
			}

			return true;
		}
		#endregion
	}
}
