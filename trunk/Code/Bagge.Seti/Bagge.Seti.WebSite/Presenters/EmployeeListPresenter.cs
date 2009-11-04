using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.BusinessEntities.Security;

namespace Bagge.Seti.WebSite.Presenters
{
	public class EmployeeListPresenter: ListPresenter<Employee, int>
	{
		ISimpleFindGetManager<EmployeeCategory, int> _employeeCategoryManager;
		IUser _loggedUser;

		public EmployeeListPresenter(IEmployeeListView view,
			IEmployeeManager employeeManager,
			ISimpleFindGetManager<EmployeeCategory, int> employeeCategoryManager,
			IUser loggedUser): base(view, employeeManager)
		{
			_employeeCategoryManager = employeeCategoryManager;
			_loggedUser = loggedUser;
		}

		protected override void OnInit(object sender, EventArgs e)
		{
			if (!View.IsPostBack)
				GetView<IEmployeeListView>().Categories = _employeeCategoryManager.FindAll();

			base.OnInit(sender, e);
		}

		public bool CanDeleteEmployee(Employee employee)
		{
			if (employee == null)
				return true;

			return !employee.Equals(_loggedUser);
		}
	}
}
