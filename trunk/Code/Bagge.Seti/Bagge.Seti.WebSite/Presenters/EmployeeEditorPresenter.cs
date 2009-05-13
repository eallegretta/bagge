using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.WebSite.Presenters
{
	public class EmployeeEditorPresenter : EditorPresenter<Employee, int>
	{
		ISimpleFindGetManager<EmployeeCategory, int> _employeeCategoryManager;
		IRoleManager _roleManager;

		public EmployeeEditorPresenter(IEmployeeEditorView view,
			IEmployeeManager employeeManager,
			ISimpleFindGetManager<EmployeeCategory, int> employeeCategoryManager,
			IRoleManager roleManager)
			: base(view, employeeManager)
		{
			_employeeCategoryManager = employeeCategoryManager;
			_roleManager = roleManager;
		}

		protected new IEmployeeEditorView View
		{
			get { return GetView<IEmployeeEditorView>(); }
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
					View.AvailableRoles = _roleManager.FindAllOrdered("Name");
					View.Categories = _employeeCategoryManager.FindAll();
					if (!View.IsPostBack && View.Mode == EditorAction.Update)
					{
						View.SelectedCategoryId = SelectedEntity.Category.Id;
						var roleIds = (from role in SelectedEntity.Roles
									   select role.Id).ToArray();
						View.AssignedRoleIds = roleIds;
					}
					break;
				case EditorAction.View:
					View.AvailableRoles = SelectedEntity.Roles.ToArray();
					break;
			}
		}


		public override void Save(Employee entity)
		{
			entity.Category = _employeeCategoryManager.Get(View.SelectedCategoryId);
			entity.Password = View.Password;
			entity.Roles = new List<Role>();
			foreach (int roleId in View.AssignedRoleIds)
				entity.Roles.Add(_roleManager.Get(roleId));

			base.Save(entity);
		}
	}
}
