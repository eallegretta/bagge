using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.DataAccess.Contracts;
using Bagge.Seti.BusinessEntities;
using Castle.ActiveRecord;
using NHibernate.Expression;

namespace Bagge.Seti.DataAccess.ActiveRecord
{
	public class AlertConfigurationDao: IAlertConfigurationDao
	{
		#region IAlertConfigurationDao Members

		public AlertConfiguration Get()
		{
			return ActiveRecordMediator<AlertConfiguration>.FindByPrimaryKey(new AlertConfiguration().Id);
		}

		public void Update(AlertConfiguration alertConfiguration)
		{
			ActiveRecordMediator<AlertConfiguration>.Update(alertConfiguration);
            SessionScopeUtils.FlushSessionScope();
        }

		#endregion
	}
}
