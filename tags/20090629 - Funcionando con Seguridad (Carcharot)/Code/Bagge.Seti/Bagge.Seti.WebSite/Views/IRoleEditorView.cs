using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.WebSite.Views
{
	public interface IRoleEditorView: IEditorView<int>
	{
		Function[] AvailableFunctions { set; }
		int[] AssignedFunctionIds { get; set; }
	}
}
