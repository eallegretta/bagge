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
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.WebSite
{
	public partial class RoleList : FilteredListPage<Role, int>, IRoleListView
	{

		RoleListPresenter _presenter;

		public RoleList()
		{
			_presenter = new RoleListPresenter(this, IoCContainer.RoleManager,
				IoCContainer.EmployeeManager, IoCContainer.FunctionManager);
		}


		public override IList<Bagge.Seti.BusinessEntities.FilterPropertyValue> Filters
		{
			get
			{

				List<FilterPropertyValue> filters = new List<FilterPropertyValue>();
				AddTextBoxFilterValue<string>(_name, "Name", FilterPropertyValueType.Like, filters);
				AddTextBoxFilterValue<string>(_description, "Description", FilterPropertyValueType.Like, filters);
				AddDropDownFilterValue<int>(_employees, "Employees", FilterPropertyValueType.In, filters);
				AddDropDownFilterValue<int>(_functions, "Functions", FilterPropertyValueType.In, filters);
				AddDeletedFilterValue(filters);

				return filters;
			}
		}

		protected override Button FilterButton
		{
			get { return _filter; }
		}

		protected override DropDownList DeletedDropDownList
		{
			get { return _isDeleted; }
		}

		protected override GridView Grid
		{
			get { return _roles; }
		}

		protected override Bagge.Seti.WebSite.Presenters.ListPresenter<Role, int> Presenter
		{
			get { return _presenter; }
		}

		protected override Microsoft.Practices.Web.UI.WebControls.ObjectContainerDataSource ObjectDataSource
		{
			get { return _dataSource; }
		}

		#region IRoleListView Members

		public Bagge.Seti.BusinessEntities.Employee[] Employees
		{
			set
			{
				_employees.DataSource = value;
				_employees.DataBind();
			}
		}

		public Function[] Functions
		{
			set
			{
				_functions.DataSource = value;
				_functions.DataBind();
			}
		}

		#endregion
	}
}
