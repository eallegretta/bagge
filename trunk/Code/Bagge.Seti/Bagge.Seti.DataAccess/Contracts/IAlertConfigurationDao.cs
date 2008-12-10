using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.DataAccess.Contracts
{
	public interface IAlertConfigurationDao
	{
		AlertConfiguration Get();
		void Update(AlertConfiguration alertConfiguration);
	}
}
