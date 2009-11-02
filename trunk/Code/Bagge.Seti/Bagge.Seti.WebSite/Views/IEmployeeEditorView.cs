using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.WebSite.Views
{
	public interface IEmployeeEditorView: IEditProfileView
	{
		EmployeeCategory[] Categories { set; }
		int SelectedCategoryId { get; set; }
		Role[] AvailableRoles { set; }
		int[] AssignedRoleIds { get; set; }

		string Username { get; set; }

		bool ShowInvalidActiveDirectoryUserMessage { set; }

		bool PasswordVisible { set; }
		bool FirstnameReadOnly { set; }
		bool LastnameReadOnly { set; }
		bool PhoneReadOnly { set; }
		bool EmergencyPhoneReadOnly { set; }
		bool EmailReadOnly { set; }

		string Firstname { set; }
		string Lastname { set; }
		string Phone { set; }
		string EmergencyPhone { set; }
		string Email { set; }


		event EventHandler OnActiveDirectoryValidate;

		bool IsWindowsAuthentication { set; }
	}
}
