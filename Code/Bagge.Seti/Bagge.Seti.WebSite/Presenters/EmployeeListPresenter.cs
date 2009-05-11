using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.WebSite.Views;

namespace Bagge.Seti.WebSite.Presenters
{
	public class EmployeeListPresenter: ListPresenter<Employee, int>
	{
		ISimpleFindGetManager<EmployeeCategory, int> _employeeCategoryManager;

		public EmployeeListPresenter(IEmployeeListView view,
			IEmployeeManager employeeManager,
			ISimpleFindGetManager<EmployeeCategory, int> employeeCategoryManager): base(view, employeeManager)
		{
			_employeeCategoryManager = employeeCategoryManager;
		}

		protected override void OnInit(object sender, EventArgs e)
		{
			if (!View.IsPostBack)
				GetView<IEmployeeListView>().Categories = _employeeCategoryManager.FindAll();

			base.OnInit(sender, e);
		}
	}
}
