using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Aop;
using Bagge.Seti.Extensions;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.BusinessEntities.Exceptions;
using Bagge.Seti.BusinessEntities.Validators;

namespace Bagge.Seti.Common.Interceptors
{
	public class ValidationInterceptor: IMethodBeforeAdvice
	{
		#region IMethodBeforeAdvice Members

		public void Before(System.Reflection.MethodInfo method, object[] args, object target)
		{
			if (args != null)
			{
				if (method.IsDefined(typeof(AvoidValidationAttribute), true))
					return;

				foreach (object arg in args)
				{
					if (arg is IAuditable)
					{
						if (!IoCContainer.ValidationEngine.IsValid(arg))
						{
							throw new ValidationException(string.Join(Environment.NewLine, IoCContainer.ValidationEngine.GetErrorMessages(arg)));
						}
					}
				}
			}
		}

		#endregion
	}
}
