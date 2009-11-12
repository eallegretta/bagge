using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Web.UI.WebControls;
using Bagge.Seti.WebSite.Helpers;
using System.Reflection;
using System.IO;

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

			using (var streamReader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("Bagge.Seti.WebSite.Reports.ReportOutput.htm")))
			{
				string headerTitle = ((Bagge.Seti.WebSite.Site)Master).CurrentViewTitle;

				string headerImageUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Page.ResolveUrl("~/App_Themes/" + Page.Theme + "/Images/siteHeader.jpg");
				string html = streamReader
								.ReadToEnd()
								.Replace("{ReportTitle}", ReportFileName)
								.Replace("{ReportHeaderTitle}", headerTitle)
								.Replace("{ColSpan}", _report.HeaderRow.Cells.Count.ToString())								.Replace("{HeaderImageUrl}", headerImageUrl)
								.Replace("{ReportGrid}", ControlHelper.GetControlAsHtml(_report))
								.Replace("<th ", "<th bgcolor=\"#EBECEE\" ")
								.Replace("{CurrentDateTime}", DateTime.Now.ToString())
								.Replace("<td>True</td>", "<td>" + Resources.WebSite.YesText + "</td>")
								.Replace("<td>False</td>", "<td>" + Resources.WebSite.NoText + "</td>");
				HttpContext.Current.Response.Write(html);
			}
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
