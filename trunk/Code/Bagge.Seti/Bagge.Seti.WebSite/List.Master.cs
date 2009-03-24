using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bagge.Seti.WebSite
{
	public partial class List : System.Web.UI.MasterPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (_filters.Controls.Count == 0)
				_filtersPanel.Visible = false;
		}
	}
}
