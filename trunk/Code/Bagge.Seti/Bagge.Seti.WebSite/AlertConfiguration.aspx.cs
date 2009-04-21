using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Web.UI.WebControls;
using Bagge.Seti.BusinessEntities;
using Microsoft.Practices.Web.UI.WebControls.Utility;
using Bagge.Seti.WebSite.Presenters;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.Common;

namespace Bagge.Seti.WebSite
{
	public partial class AlertConfigurationEditor : Page
	{

		public AlertConfigurationEditor()
		{
		}

		protected override void OnInit(EventArgs e)
		{
			_dataSource.Selecting += new EventHandler<ObjectContainerDataSourceSelectingEventArgs>(_dataSource_Selecting);
			_dataSource.Updating += new EventHandler<ObjectContainerDataSourceUpdatingEventArgs>(_dataSource_Updating);
			_dataSource.DataObjectTypeName = typeof(AlertConfiguration).FullName;
			base.OnInit(e);
		}

		void _dataSource_Selecting(object sender, ObjectContainerDataSourceSelectingEventArgs e)
		{
			_dataSource.DataSource = IoCContainer.AlertConfigurationManager.Get();
		}

		void _dataSource_Updating(object sender, ObjectContainerDataSourceUpdatingEventArgs e)
		{
			AlertConfiguration instance = new AlertConfiguration();
			TypeDescriptionHelper.BuildInstance(e.NewValues, instance);
			IoCContainer.AlertConfigurationManager.Update(instance);
		}

		public object DataSource
		{
			set
			{
				_dataSource.DataSource = value;
			}
		}
	}
}
