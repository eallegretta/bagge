using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.WebSite.Presenters;
using Bagge.Seti.Common;
using Bagge.Seti.WebSite.Views;

namespace Bagge.Seti.WebSite
{
	public partial class EmployeeList : FilteredListPage<Employee, int>, IEmployeeListView
	{
		EmployeeListPresenter _presenter;

		public EmployeeList()
		{
			_presenter = new EmployeeListPresenter(this,
				IoCContainer.EmployeeManager,
				IoCContainer.EmployeeCategoryManager);
		}


		protected override GridView Grid
		{
			get { return _employees; }
		}

		protected override Bagge.Seti.WebSite.Presenters.ListPresenter<Employee, int> Presenter
		{
			get { return _presenter; }
		}

		protected override Microsoft.Practices.Web.UI.WebControls.ObjectContainerDataSource ObjectDataSource
		{
			get { return _dataSource; }
		}

		#region IEmployeeListView Members

		public EmployeeCategory[] Categories
		{
			set
			{
				_category.DataSource = value;
				_category.DataBind();
			}
		}

		#endregion

		public override IList<FilterPropertyValue> Filters
		{
			get
			{
				List<FilterPropertyValue> filters = new List<FilterPropertyValue>();
				AddTextBoxFilterValue<string>(_fileNumber, "FileNumber", FilterPropertyValueType.Like, filters);
				AddTextBoxFilterValue<string>(_username, "Username", FilterPropertyValueType.Like, filters);
				AddTextBoxFilterValue<string>(_firstname, "Firstname", FilterPropertyValueType.Like, filters);
				AddTextBoxFilterValue<string>(_lastname, "Lastname", FilterPropertyValueType.Like, filters);
				AddDropDownFilterValue<int>(_category, "Category", FilterPropertyValueType.Equals, filters);
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
	}
}
