using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Bagge.Seti.WebSite.Views
{
	public interface IListView: IView
	{
		event GridViewPageEventHandler PageIndexChanging;
	}
}
