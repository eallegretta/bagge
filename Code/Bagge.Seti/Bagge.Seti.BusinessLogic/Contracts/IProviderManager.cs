using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.BusinessLogic.Contracts
{
	public interface IProviderManager: IAuditableManager<Provider, int>
	{
		Provider GetByCuit(string cuit);
	}
}
