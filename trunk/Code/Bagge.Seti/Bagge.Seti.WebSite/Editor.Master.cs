using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bagge.Seti.WebSite
{
	public partial class Editor : System.Web.UI.MasterPage
	{
		public Label RequiredInformation
		{
			get { return _requiredInformationLabel; }
		}

		protected void Page_Load(object sender, EventArgs e)
		{

		}
	}
}
