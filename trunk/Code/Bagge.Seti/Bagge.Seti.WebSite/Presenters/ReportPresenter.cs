using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.BusinessEntities.Reports;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.BusinessLogic.Contracts;

namespace Bagge.Seti.WebSite.Presenters
{
	public class ReportPresenter<T> where T : BaseReport
	{
		IReportView _view;
		IReportManager _manager;

		protected IReportView View
		{
			get { return _view; }
		}

		protected IReportManager Manager
		{
			get { return _manager; }
		}

		public ReportPresenter(IReportView view, IReportManager manager)
		{
			_view = view;
			_view.Load += new EventHandler(OnLoad);
			_manager = manager;
		}


		protected virtual void OnLoad(object sender, EventArgs e)
		{
			if (View.IsPostBack)
				View.DataBind();
		}

		public virtual void Select()
		{
			var report = _manager.GetReport<T>(_view.Filters);

			if(report != null)
				_view.DataSource = report.ReportData;
		}

	}
}
