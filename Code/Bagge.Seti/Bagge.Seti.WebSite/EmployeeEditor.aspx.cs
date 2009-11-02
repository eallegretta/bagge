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
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.WebSite.Controls;
using System.Text;

namespace Bagge.Seti.WebSite
{
	[SecurizableCrud("Securizable_EmployeeEditor", typeof(EmployeeEditor), FunctionAction.Get | FunctionAction.Create | FunctionAction.Update)]
	public partial class EmployeeEditor : EditorPage<Employee, int>, IEmployeeEditorView
	{
		EmployeeEditorPresenter _presenter;

		public EmployeeEditor()
		{
			_presenter = new EmployeeEditorPresenter(this, 
				IoCContainer.EmployeeManager,
				IoCContainer.EmployeeCategoryManager, 
				IoCContainer.RoleManager, 
				IoCContainer.AuthenticationProvider);
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
							  where role.Selected == true
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

		#region IEmployeeEditorView Members


		public bool PasswordVisible
		{
			set
			{
				//CORRECT IMPLEMENTATION IF GRIDVIEW WORKED PROPERLY
				/*var fields = from f in _details.Fields.Cast<DataControlField>()
							 where f is SecureTemplateField &&
									((SecureTemplateField)f).PropertyName == "Password"
							 select f;

				foreach (var field in fields)
					field.Visible = false;*/

				//SINCE GRIDVIEW DOES NOT BIND TEXT WHEN SOME FIELD ARE NOT SHOWN WE 
				//MANUALLY HIDE THEM USING JQUERY
				if (!value)
				{
					var password = _details.FindControl("_password");
					var confirmPassword = _details.FindControl("_confirmPassword");

					var passwordVal = _details.FindControl("_passwordVal") as RequiredFieldValidator;
					var confirmPasswordReqVal = _details.FindControl("_confirmPasswordReqVal") as RequiredFieldValidator;
					var confirmPasswordCmpVal = _details.FindControl("_confirmPasswordCmpVal") as CompareValidator;

					if (passwordVal != null)
						passwordVal.Visible = false;

					if (confirmPasswordReqVal != null)
						confirmPasswordReqVal.Visible = false;
					
					if (confirmPasswordCmpVal != null)
						confirmPasswordCmpVal.Visible = false;


					StringBuilder script = new StringBuilder();
					if (password != null)
						script.AppendFormat("$('#{0}').parents('tr.detailsRow').remove();", password.ClientID);
					if (confirmPassword != null)
						script.AppendFormat("$('#{0}').parents('tr.detailsRow').remove();", confirmPassword.ClientID);

					script.AppendFormat("$('.password').parents('tr.detailsRow').remove();");

					if (ScriptManager.GetCurrent(Page).IsInAsyncPostBack)
						ScriptManager.RegisterStartupScript(Page, typeof(string), "HidePassword", script.ToString(), true);
					else
						Page.ClientScript.RegisterStartupScript(typeof(string), "HidePassword", script.ToString(), true);


				}
			}
		}

		private void SetText(string ctrlID, string text)
		{
			var ctrl = _details.FindControl(ctrlID) as TextBox;
			if(ctrl != null)
				ctrl.Text = text;
		}
		private void SetReadOnly(string ctrlID, bool isReadOnly)
		{
			var ctrl = _details.FindControl(ctrlID) as TextBox;
			if(ctrl != null)
				ctrl.ReadOnly = isReadOnly;
		}

		public string Username
		{
			get
			{
				var username = _details.FindControl("_username") as TextBox;
				if (username != null)
				{
					if (string.IsNullOrEmpty(username.Text))
						return Request.Form[username.UniqueID];
					else
						return username.Text;
				}
				return null;
			}
			set
			{
				SetText("_username", value);
			}
		}

		public bool FirstnameReadOnly
		{
			set
			{
				SetReadOnly("Firstname_txt", value);
			}
		}

		public bool LastnameReadOnly
		{
			set 
			{
				SetReadOnly("Lastname_txt", value);
			}
		}

		public bool PhoneReadOnly
		{
			set 
			{
				SetReadOnly("Phone_txt", value);
			}
		}

		public bool EmergencyPhoneReadOnly
		{
			set { SetReadOnly("EmergencyPhone_txt", value); }
		}

		public bool EmailReadOnly
		{
			set { SetReadOnly("Email_txt", value); }
		}

		public string Firstname
		{
			set { SetText("Firstname_txt", value); }
		}

		public string Lastname
		{
			set { SetText("Lastname_txt", value); }
		}

		public string Phone
		{
			set { SetText("Phone_txt", value); }
		}

		public string EmergencyPhone
		{
			set { SetText("EmergencyPhone_txt", value); }
		}

		public string Email
		{
			set { SetText("Email_txt", value); }
		}

		#endregion

		#region IEmployeeEditorView Members


		public event EventHandler OnActiveDirectoryValidate
		{
			add
			{
				var button = _details.FindControl("_validateUsername") as Button;
				if(button != null)
					button.Click += value;
			}
			remove
			{
				var button = _details.FindControl("_validateUsername") as Button;
				if(button != null)
					button.Click -= value;
			}
		}

		public bool IsWindowsAuthentication
		{
			set
			{
				var ph = _details.FindControl("_windowsAuthUsernameConfig") as PlaceHolder;
				if (ph != null)
					ph.Visible = value;

				var hint = _details.FindControl("_domainUsernameHint") as Literal;
				if (hint != null)
					hint.Visible = value;
			}
		}

		#endregion

		#region IEmployeeEditorView Members


		public bool ShowInvalidActiveDirectoryUserMessage
		{
			set 
			{
				var validator = _details.FindControl("_usernameInvalidVal") as CustomValidator;
				if (validator != null)
					validator.IsValid = !value;
			}
		}

		#endregion


		protected void _usernameUniqueVal_ServerValidate(object sender, ServerValidateEventArgs e)
		{
			e.IsValid = _presenter.IsUsernameUnique(e.Value);
		}
	}
}
