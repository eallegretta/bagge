﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bagge.Seti.BusinessEntities.Validators
{
	[AttributeUsage(AttributeTargets.Method)]
	public class AvoidValidationAttribute: Attribute
	{
	}
}
