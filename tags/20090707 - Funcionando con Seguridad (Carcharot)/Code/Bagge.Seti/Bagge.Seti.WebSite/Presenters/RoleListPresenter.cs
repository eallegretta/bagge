using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.DesignByContract;

namespace Bagge.Seti.WebSite.Presenters
{
	public class RoleListPresenter: ListPresenter<Role, int>
	{
		IEmployeeManager _employeeManager;
		IFunctionManager _functionManager;

		public RoleListPresenter(IRoleListView view, IRoleManager manager,
			IEmployeeManager employeeManager, IFunctionManager functionManager)
			:base(view, manager)

		{
			_employeeManager = employeeManager;
			_functionManager = functionManager;

		}

		protected override void OnInit(object sender, EventArgs e)
		{
			if (!View.IsPostBack)
			{
				var view = GetView<IRoleListView>();
				view.Employees = _employeeManager.FindAllActiveOrdered("Lastname");
				view.Functions = _functionManager.FindAll();
			}
			base.OnInit(sender, e);
		}




		public bool CanAdministerRole(Role role)
		{
			Check.Require(role != null);

			return !role.IsSuperAdministratorRole;
		}

	}
}
