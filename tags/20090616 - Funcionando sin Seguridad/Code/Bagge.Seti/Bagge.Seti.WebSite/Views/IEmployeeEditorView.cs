using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.WebSite.Views
{
	public interface IEmployeeEditorView: IEditorView<int>
	{
		string Password { get; }
		string ConfirmPassword { get; }
		EmployeeCategory[] Categories { set; }
		int SelectedCategoryId { get; set; }
		Role[] AvailableRoles { set; }
		int[] AssignedRoleIds { get; set; }
	}
}
