using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.BusinessEntities.Reports;
using System.Web.UI.WebControls;

namespace Bagge.Seti.WebSite.Reports
{
	public abstract class FilteredReportPage<T> : ReportPage<T> where T : BaseReport
	{
		protected override void OnInit(EventArgs e)
		{
			FilterButton.Click += new EventHandler(FilterButton_Click);

			base.OnInit(e);
		}

		void FilterButton_Click(object sender, EventArgs e)
		{
			Presenter.Select();
		}


		protected override void OnLoad(EventArgs e)
		{
			Presenter.Select();

		}

		protected abstract Button FilterButton { get; }
	}
}
