using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.WebSite.Views;

namespace Bagge.Seti.WebSite.Presenters
{
	public class RoleEditorPresenter: EditorPresenter<Role, int>
	{
		IFunctionManager _functionManager;

		public RoleEditorPresenter(IRoleEditorView view,
			IRoleManager roleManager,
			IFunctionManager functionManager)
			: base(view, roleManager)
		{
			_functionManager = functionManager;
		}

		protected new IRoleEditorView View
		{
			get { return GetView<IRoleEditorView>(); }
		}

		protected override void OnInit(object sender, EventArgs e)
		{
			base.OnInit(sender, e);
			View.DataBound += new EventHandler(View_DataBound);
		}

		void View_DataBound(object sender, EventArgs e)
		{
			switch (View.Mode)
			{
				case EditorAction.Insert:
				case EditorAction.Update:
					View.AvailableFunctions = _functionManager.FindAll();
					if (!View.IsPostBack && View.Mode == EditorAction.Update)
					{
						var functionIds = (from function in SelectedEntity.Functions
									   select function.Id).ToArray();
						View.AssignedFunctionIds = functionIds;
					}
					break;
				case EditorAction.View:
					View.AvailableFunctions = SelectedEntity.Functions.ToArray();
					break;
			}
		}


		public override void Save(Role entity)
		{
			entity.Functions = new List<Function>();
			foreach (int functionId in View.AssignedFunctionIds)
				entity.Functions.Add(_functionManager.Get(functionId));

			base.Save(entity);
		}
	}
}
