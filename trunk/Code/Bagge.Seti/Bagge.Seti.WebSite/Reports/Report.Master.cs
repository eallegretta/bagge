using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Web.UI.WebControls;
using Bagge.Seti.WebSite.Helpers;

namespace Bagge.Seti.WebSite.Reports
{
	public partial class ReportMaster : System.Web.UI.MasterPage
	{

		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void _export_Click(object sender, EventArgs e)
		{
			HttpContext.Current.Response.Clear();
			HttpContext.Current.Response.AddHeader(
				"content-disposition", string.Format("attachment; filename={0}.xls", ReportFileName ));
			HttpContext.Current.Response.ContentType = "application/ms-excel";
			HttpContext.Current.Response.Write(ControlHelper.GetControlAsHtml(_report));
			HttpContext.Current.Response.End();
		}

		public event EventHandler DataBound
		{
			add { _report.DataBound += value; }
			remove { _report.DataBound -= value; }
		}


		public string ReportFileName
		{
			set { ViewState["ReportFileName"] = value; }
			get { return ViewState["ReportFileName"] as string; }
		}

		public bool ShowFilters
		{
			set
			{
				_filtersPanel.Visible = value;
			}
		}

		public object DataSource
		{
			set
			{
				_report.DataSource = value;
				_report.DataBind();

				_export.Visible = _report.Rows.Count > 0;
			}
		}
	}
}
