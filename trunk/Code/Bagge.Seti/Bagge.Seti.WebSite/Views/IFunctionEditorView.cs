using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.WebSite.Views
{
	public interface IFunctionEditorView: IEditorView<int>
	{
		Role[] Roles { set; }
		int SelectedRoleId { get; }
		string[] Classnames { set; }
		string SelectedClassname { get; }
		AccessibilityType[] Accessibilities { set; }
		byte SelectedAccessibilityTypeId { get; }
		string[] Members { set; }
		string SelectedMember { get; }
		string[] ConstraintTypes { set; }
		string SelectedConstraintType { get; }
		bool ConstraintTypesVisible { set; }
		bool ConstraintValueVisible { set; }
		string SelectedConstraintValue { get; }
	}
}
