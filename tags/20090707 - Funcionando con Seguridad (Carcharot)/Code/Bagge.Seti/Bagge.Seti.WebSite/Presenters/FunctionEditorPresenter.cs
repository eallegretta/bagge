using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.WebSite.Views;

namespace Bagge.Seti.WebSite.Presenters
{
	public class FunctionEditorPresenter: EditorPresenter<Function, int>
	{
		IRoleManager _roleManager;

		public FunctionEditorPresenter(IFunctionEditorView view,
			IFunctionManager functionManager,
			IRoleManager roleManager): base(view, functionManager)
		{
			_roleManager = roleManager;
		}
	}
}
