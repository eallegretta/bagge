using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.DataAccess.Contracts;
using Bagge.Seti.BusinessEntities;
using Castle.ActiveRecord;

namespace Bagge.Seti.DataAccess.ActiveRecord
{
	public class AlertConfigurationDao: IAlertConfigurationDao
	{
		#region IAlertConfigurationDao Members

		public AlertConfiguration Get()
		{
			return null;
		}

		public void Update(AlertConfiguration alertConfiguration)
		{

		}

		#endregion
	}
}
