using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.BusinessEntities.Reports;
using Bagge.Seti.WebSite.Presenters;
using Bagge.Seti.Common;
using Bagge.Seti.WebSite.Views;

namespace Bagge.Seti.WebSite.Reports
{
	public abstract class ReportPage<T>: Page, IReportView where T: BaseReport
	{
		ReportPresenter<T> _presenter;

		public ReportPage()
		{
			_presenter = new ReportPresenter<T>(this, IoCContainer.ReportManager);
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			ReportMaster master = Master as ReportMaster;
			if (master != null)
			{
				master.ReportTypeName = typeof(T).FullName;
				master.ObjectDataSource.Selecting += new EventHandler<Microsoft.Practices.Web.UI.WebControls.ObjectContainerDataSourceSelectingEventArgs>(ObjectDataSource_Selecting);
			}
			AssignTypeNameToSecureContainers(typeof(T).AssemblyQualifiedName);
		}

		void ObjectDataSource_Selecting(object sender, Microsoft.Practices.Web.UI.WebControls.ObjectContainerDataSourceSelectingEventArgs e)
		{
			Presenter.Select();
		}

		protected virtual ReportPresenter<T> Presenter
		{
			get { return _presenter; }
		}

		#region IReportView Members

		public abstract IList<Bagge.Seti.BusinessEntities.FilterPropertyValue> Filters
		{
			get;
		}

		#endregion


		#region IView Members


		public object DataSource
		{
			set
			{
				ReportMaster master = Master as ReportMaster;
				if (master != null)
					master.DataSource = value;
			}
		}

		#endregion
	}
}
