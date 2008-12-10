using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.DataAccess.Contracts;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.BusinessLogic
{
	public class AlertConfigurationManager: IAlertConfigurationManager
	{
		IAlertConfigurationDao _dao;

		public AlertConfigurationManager(IAlertConfigurationDao dao)
		{
			_dao = dao;
		}

		#region IAlertConfigurationManager Members

		public AlertConfiguration Get()
		{
			return _dao.Get();
		}

		public void Update(AlertConfiguration alertConfiguration)
		{
			_dao.Update(alertConfiguration);
		}

		#endregion
	}
}
