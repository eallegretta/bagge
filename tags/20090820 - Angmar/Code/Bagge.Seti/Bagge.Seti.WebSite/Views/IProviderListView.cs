using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.WebSite.Views
{
	public interface IProviderListView: IListView
	{
		Product[] Products { set; } 
	}
}
