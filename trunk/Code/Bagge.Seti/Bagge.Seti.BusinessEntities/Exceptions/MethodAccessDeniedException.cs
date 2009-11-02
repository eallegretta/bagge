﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities.Properties;

namespace Bagge.Seti.BusinessEntities.Exceptions
{
	public class MethodAccessDeniedException: BusinessRuleException
	{
		public MethodAccessDeniedException()
			: base(Resources.MethodAccessDeniedErrorMessage)
		{
		}
		public MethodAccessDeniedException(string message)
			: base(message)
		{
		}
	}
}
