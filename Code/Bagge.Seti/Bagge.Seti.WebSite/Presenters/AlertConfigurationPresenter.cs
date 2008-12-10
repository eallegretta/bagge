using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.WebSite.Presenters
{
	public class AlertConfigurationPresenter
	{
		IView _view;
		IAlertConfigurationManager _manager;

		public AlertConfigurationPresenter(IView view, IAlertConfigurationManager manager)
		{
			_view = view;
			_manager = manager;
		}

		public void Select()
		{
			_view.DataSource = _manager.Get().ToSingleItemArray<AlertConfiguration>();
		}

		public void Save(AlertConfiguration entity)
		{
			_manager.Update(entity);
		}
	}
}
