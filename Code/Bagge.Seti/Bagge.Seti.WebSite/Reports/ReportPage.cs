using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.BusinessEntities.Reports;
using Bagge.Seti.WebSite.Presenters;
using Bagge.Seti.Common;
using Bagge.Seti.WebSite.Views;
using System.Web.UI.WebControls;

namespace Bagge.Seti.WebSite.Reports
{
	public abstract class ReportPage<T>: Page, IReportView where T: BaseReport
	{
		ReportPresenter<T> _presenter;

		public ReportPage()
		{
			_presenter = new ReportPresenter<T>(this, IoCContainer.ReportManager);
		}

		public virtual string GetFormattedColumnValue(int columnIndex, string value)
		{
			return null;
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			ReportMaster master = Master as ReportMaster;
			if (master != null)
			{
				master.ReportFileName = typeof(T).Name;
				master.DataBound += new EventHandler(master_DataBound);
			}
			AssignTypeNameToSecureContainers(typeof(T).AssemblyQualifiedName);
		}

		void master_DataBound(object sender, EventArgs e)
		{
			var grid = (GridView)sender;
			if (grid.HeaderRow == null)
				return;

			int colCount = grid.HeaderRow.Cells.Count;
			foreach (GridViewRow row in grid.Rows)
			{
				for (int cellIndex = 0; cellIndex < colCount; cellIndex++)
				{
					string value = GetFormattedColumnValue(cellIndex, row.Cells[cellIndex].Text.Replace("&nbsp;", string.Empty).Trim());
					if (value != null)
						row.Cells[cellIndex].Text = value;
				}
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			ReportMaster master = Master as ReportMaster;
			if (master != null)
				master.ShowFilters = false;

			Presenter.Select();
		}

		protected virtual ReportPresenter<T> Presenter
		{
			get { return _presenter; }
		}

		
		


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
