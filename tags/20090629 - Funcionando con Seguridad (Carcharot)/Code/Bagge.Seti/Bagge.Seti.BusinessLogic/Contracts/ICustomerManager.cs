﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.BusinessLogic.Contracts
{
	public interface ICustomerManager: IAuditableManager<Customer, int>
	{
		Customer GetByCuit(string cuit);
	}
}