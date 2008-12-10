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

namespace Bagge.Seti.WebSite
{
	public partial class AlertConfigurationEditor : System.Web.UI.Page, IView
	{


		protected override void OnInit(EventArgs e)
		{
			ObjectDataSource.Selecting += new EventHandler<ObjectContainerDataSourceSelectingEventArgs>(ObjectDataSource_Selecting);
			ObjectDataSource.Updating += new EventHandler<ObjectContainerDataSourceUpdatingEventArgs>(ObjectDataSource_Updating);
			ObjectDataSource.DataObjectTypeName = typeof(AlertConfiguration).FullName;
			base.OnInit(e);
		}

		void ObjectDataSource_Selecting(object sender, ObjectContainerDataSourceSelectingEventArgs e)
		{
			Presenter.Select();
		}

		void ObjectDataSource_Updating(object sender, ObjectContainerDataSourceUpdatingEventArgs e)
		{
			AlertConfiguration instance = new AlertConfiguration();
			TypeDescriptionHelper.BuildInstance(e.NewValues, instance);
			Presenter.Save(instance);
		}


		protected AlertConfigurationPresenter Presenter
		{
			get { return null; }
		}
		protected ObjectContainerDataSource ObjectDataSource
		{
			get { return null; }
		}


		public object DataSource
		{
			set
			{
				ObjectDataSource.DataSource = value;
			}
		}
	}
}
