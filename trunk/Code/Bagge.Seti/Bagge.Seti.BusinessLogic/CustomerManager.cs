using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.DataAccess.Contracts;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.BusinessLogic.Contracts;

namespace Bagge.Seti.BusinessLogic
{
	public class CustomerManager: GenericManager<Customer, int>, ICustomerManager
	{
		public CustomerManager(ICustomerDao dao)
			: base(dao)
		{
		}
	}
}
