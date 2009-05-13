using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.WebSite.Presenters;
using Bagge.Seti.Common;

namespace Bagge.Seti.WebSite
{
	public partial class EmployeeEditor : EditorPage<Employee, int>, IEmployeeEditorView
	{
		EmployeeEditorPresenter _presenter;

		public EmployeeEditor()
		{
			_presenter = new EmployeeEditorPresenter(this, IoCContainer.EmployeeManager,
				IoCContainer.EmployeeCategoryManager, IoCContainer.RoleManager);
		}

		protected override Bagge.Seti.WebSite.Presenters.EditorPresenter<Employee, int> Presenter
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

		#region IEmployeeEditorView Members

		public EmployeeCategory[] Categories
		{
			set
			{
				var categories = Details.FindControl("_categories") as DropDownList;
				if (categories != null)
				{
					categories.DataSource = value;
					categories.DataBind();
				}
			}
		}

		public int SelectedCategoryId
		{
			get
			{
				var categories = Details.FindControl("_categories");
				return Request.Form[categories.UniqueID].ToInt32();
			}
			set
			{
				var categories = Details.FindControl("_categories");
				if (categories is DropDownList)
					((DropDownList)categories).SelectedValue = value.ToString();
				else
					((HiddenField)categories).Value = value.ToString();
			}
		}

		public Bagge.Seti.Security.BusinessEntities.Role[] AvailableRoles
		{
			set
			{
				var roles = Details.FindControl("_roles") as BaseDataBoundControl;
				if (roles != null)
				{
					roles.DataSource = value;
					roles.DataBind();
				}
			}
		}

		public int[] AssignedRoleIds
		{
			get
			{
				var roles = Details.FindControl("_roles") as CheckBoxList;
				if (roles != null)
				{
					var ids = from role in roles.Items.Cast<ListItem>()
						select role.Value.ToInt32();
					return ids.ToArray();
				}
				return null;
			}
			set
			{
				var roles = Details.FindControl("_roles") as CheckBoxList;
				if (roles != null)
				{
					foreach (int id in value)
						roles.Items.FindByValue(id.ToString()).Selected = true;
				}
			}
		}

		public string Password
		{
			get 
			{
				var password = Details.FindControl("_password") as TextBox;
				if (password != null)
					return password.Text;
				return null;
			}
		}

		public string ConfirmPassword
		{
			get
			{
				var confirmPassword = Details.FindControl("_confirmPassword") as TextBox;
				if (confirmPassword != null)
					return confirmPassword.Text;
				return null;
			}
		}

		#endregion
	}
}
