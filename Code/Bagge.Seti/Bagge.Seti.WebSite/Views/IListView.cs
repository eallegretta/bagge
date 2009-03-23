using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.WebSite.Views
{
	public interface IListView: IView
	{
		int TotalRows { set; }
		string DefaultSortExpression { get; }

		
	}
}
