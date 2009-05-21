using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Web.UI.WebControls;

namespace Bagge.Seti.WebSite.Reports
{
	public partial class ReportMaster : System.Web.UI.MasterPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		public string ReportTypeName
		{
			set
			{
				_dataSource.DataObjectTypeName = value;
			}
		}

		public ObjectContainerDataSource ObjectDataSource
		{
			get
			{
				return _dataSource;
			}
		}

		public object DataSource
		{
			set
			{
				_dataSource.DataSource = value;
				_dataSource.DataBind();
			}
		}
	}
}
