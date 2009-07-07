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
using Bagge.Seti.BusinessEntities.Security;

namespace Bagge.Seti.BusinessLogic
{
	[Securizable("Securizable_AlertConfigurationManager", typeof(AlertConfigurationManager))]
	public partial class AlertConfigurationManager : IAlertConfigurationManager
	{
        IAlertConfigurationDao _dao;

		public AlertConfigurationManager(IAlertConfigurationDao dao)
		{
			_dao = dao;
		}
   
		#region IAlertConfigurationManager Members

		[Securizable("Securizable_AlertConfigurationManager_Get", typeof(AlertConfigurationManager))]
		public AlertConfiguration Get()
		{
			return _dao.Get();
		}

		[Securizable("Securizable_AlertConfigurationManager_Update", typeof(AlertConfigurationManager))]
		public void Update(AlertConfiguration alertConfiguration)
		{
			_dao.Update(alertConfiguration);		
        }

		#endregion
	}
}
