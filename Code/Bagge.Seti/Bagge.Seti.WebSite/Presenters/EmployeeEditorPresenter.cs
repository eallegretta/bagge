using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.WebSite.Security;
using Bagge.Seti.Common;
using Bagge.Seti.WebSite.Properties;
using Castle.ActiveRecord;
using Bagge.Seti.BusinessEntities.Exceptions;

namespace Bagge.Seti.WebSite.Presenters
{
	public class EmployeeEditorPresenter : EditorPresenter<Employee, int>
	{
		ISimpleFindGetManager<EmployeeCategory, int> _employeeCategoryManager;
		IRoleManager _roleManager;
		IAuthenticator _authenticationProvider;

		public EmployeeEditorPresenter(IEmployeeEditorView view,
			IEmployeeManager employeeManager,
			ISimpleFindGetManager<EmployeeCategory, int> employeeCategoryManager,
			IRoleManager roleManager,
			IAuthenticator authenticationProvider)
			: base(view, employeeManager)
		{
			_employeeCategoryManager = employeeCategoryManager;
			_roleManager = roleManager;
			_authenticationProvider = authenticationProvider;
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
			bool isWindowsAuthentication = AuthenticationManager.IsWindowsAuthentication(_authenticationProvider);
			View.IsWindowsAuthentication = isWindowsAuthentication;
			if (isWindowsAuthentication)
				View.PasswordVisible = false;
			switch (View.Mode)
			{
				case EditorAction.Insert:
				case EditorAction.Update:
					View.OnActiveDirectoryValidate += new EventHandler(View_OnActiveDirectoryValidate);	
					if (isWindowsAuthentication)
					{
						View.FirstnameReadOnly = View.LastnameReadOnly =
						View.PhoneReadOnly = View.EmailReadOnly = View.EmergencyPhoneReadOnly = true;
					}

					View.AvailableRoles = _roleManager.FindAllActiveOrdered("Name");
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

		void View_OnActiveDirectoryValidate(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(View.Username))
			{
				var employee = GetEmployeeFromActiveDirectory();

				if (employee != null)
				{
					View.Firstname = employee.Firstname;
					View.Lastname = employee.Lastname;
					View.Email = employee.Email;
					View.Phone = employee.Phone;
					View.EmergencyPhone = employee.EmergencyPhone;
				}
				else
				{
					View.Username = string.Empty;
					View.ShowInvalidActiveDirectoryUserMessage = true;
				}
			}
		}

		private Employee GetEmployeeFromActiveDirectory()
		{
			var employee = GetManager<IEmployeeManager>().GetFromActiveDirectory(
							Settings.Default.DCServerAddress,
							Settings.Default.DCLoginUsername,
							Settings.Default.DCLoginPassword,
							View.Username);
			return employee;
		}


		public override void Save(Employee entity)
		{
			if (AuthenticationManager.IsWindowsAuthentication(_authenticationProvider))
			{
				var employee = GetEmployeeFromActiveDirectory();
				if (employee == null)
					throw new ObjectNotFoundException(string.Format(Properties.Resources.EmployeeNotFoundException, View.Username));
				entity.Username = View.Username;
				entity.Password = Employee.DEFAULT_PASSWORD_FOR_AD_USERS;
				entity.Firstname = employee.Firstname;
				entity.Lastname = employee.Lastname;
				entity.Email = employee.Email;
				entity.Phone = employee.Phone;
				entity.EmergencyPhone = employee.EmergencyPhone;
			}
			else
				entity.Password = View.Password;
			entity.Category = _employeeCategoryManager.Get(View.SelectedCategoryId);
			entity.Roles = new List<Role>();
			foreach (int roleId in View.AssignedRoleIds)
				entity.Roles.Add(_roleManager.Get(roleId));

			base.Save(entity);
		}

		public bool IsUsernameUnique(string username)
		{
			if (View.Mode == EditorAction.Insert)
			{
				try
				{
					var employee = GetManager<IEmployeeManager>().GetByUsername(username);
					if (employee != null)
						return false;
					return true;
				}
				catch (ObjectNotFoundException)
				{
					return true;
				}
			}
			else 
			{
				try
				{
					var employee = GetManager<IEmployeeManager>().GetByUsername(username);
					if (employee != null && employee.Id != View.PrimaryKey)
						return false;
					return true;
				}
				catch (ObjectNotFoundException)
				{
					return true;
				}
			}

		}
	}
}
