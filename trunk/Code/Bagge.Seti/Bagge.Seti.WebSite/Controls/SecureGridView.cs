using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.WebSite.Controls
{
	public class SecureGridView: GridView
	{
		public SecureGridView()
		{
			this.RowDataBound += new GridViewRowEventHandler(SecureGridView_RowDataBound);
		}

		void SecureGridView_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType.In(DataControlRowType.DataRow, DataControlRowType.Header))
			{
				var item = (ISecurizable)e.Row.DataItem;
			}
		}
	}
}
