using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.WebSite.Views
{
	public interface IRoleListView: IListView
	{
		Employee[] Employees { set; }
		Function[] Functions { set; }
	}
}
