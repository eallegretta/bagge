using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.WebSite.Presenters;
using Bagge.Seti.Common;
using Bagge.Seti.BusinessEntities.Security;

namespace Bagge.Seti.WebSite
{
	[SecurizableWeb("Securizable_RoleEditor", typeof(RoleEditor), FunctionAction.Retrieve | FunctionAction.Create | FunctionAction.Update)]
	public partial class RoleEditor : EditorPage<Role, int>, IRoleEditorView
	{
		RoleEditorPresenter _presenter;

		public RoleEditor()
		{
			_presenter = new RoleEditorPresenter(this, IoCContainer.RoleManager, IoCContainer.FunctionManager);
		}

		protected override Bagge.Seti.WebSite.Presenters.EditorPresenter<Role, int> Presenter
		{
			get { return _presenter; }
		}

		protected override CompositeDataBoundControl Details
		{
			get { return _details; }
		}

		protected override Microsoft.Practices.Web.UI.WebControls.ObjectContainerDataSource ObjectDataSource
		{
			get { return _dataSource; }
		}

		protected override bool ShowRequiredInformationLabel
		{
			get { return true; }
		}

		#region IRoleEditorView Members

		public Function[] AvailableFunctions
		{
			set
			{
				var functions = Details.FindControl("_functions") as BaseDataBoundControl;
				if (functions != null)
				{
					functions.DataSource = value;
					functions.DataBind();
				}
			}
		}

		public int[] AssignedFunctionIds
		{
			get
			{
				var functions = Details.FindControl("_functions") as CheckBoxList;
				if (functions != null)
				{
					var ids = from function in functions.Items.Cast<ListItem>()
							  where function.Selected == true
							  select function.Value.ToInt32();
					return ids.ToArray();
				}
				return null;
			}
			set
			{
				var functions = Details.FindControl("_functions") as CheckBoxList;
				if (functions != null)
				{
					foreach (int id in value)
						functions.Items.FindByValue(id.ToString()).Selected = true;
				}
			}
		}
		#endregion
	}
}
