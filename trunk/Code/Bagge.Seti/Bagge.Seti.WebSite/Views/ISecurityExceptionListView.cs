using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.WebSite.Views
{
	public interface ISecurityExceptionListView
	{
		Role[] Roles { set; }
		int SelectedRoleId { get; }
		Function[] Functions { set; }
		int SelectedFunctionId { get; }
	}
}
