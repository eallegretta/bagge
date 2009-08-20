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
		string[] ClassFullQualifiedNames { set; }
		string SelectedClassFullQualifiedName { get; set; }
		AccessibilityType[] Accessibilities { set; }
		byte SelectedAccessibilityTypeId { get; set;  }
		string[] Members { set; }
		string SelectedMember { get; set;  }
		string[] ConstraintTypes { set; }
		string SelectedConstraintType { get; set; }
		bool ConstraintTypesVisible { set; }
		bool ConstraintValueVisible { set; }
		string SelectedConstraintValue { get; set; }

	}
}
