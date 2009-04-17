using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.DataAccess.Contracts;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.BusinessEntities.Exceptions;
using Bagge.Seti.BusinessLogic.Properties;
using Bagge.Seti.DesignByContract;
using Bagge.Seti.DataAccess;

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

        public AlertConfiguration GetActualAlert()
        {
            //AlertConfiguration[] alerts = Dao.FindAll();
            AlertConfiguration[] alerts = new AlertConfiguration[0];
            if (alerts.Length > 1)
                throw new BusinessRuleException(Resources.MultipleNamesErrorMessage);

            if (alerts.Length == 1)
                return alerts[0];

            throw new ObjectNotFoundException(Resources.InstanceNotFound);

        }
		#endregion
	}
}
