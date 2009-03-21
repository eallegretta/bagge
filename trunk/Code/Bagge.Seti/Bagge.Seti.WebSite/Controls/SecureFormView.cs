using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.WebSite.Controls
{
	public class SecureFormView: FormView, ISecureControlContainer
	{
		#region ISecureControl Members

		public string SecureTypeName
		{
			get; set;
		}

		public void ApplySecurityRestrictions(IList<Bagge.Seti.Security.BusinessEntities.Function> functions)
		{
			foreach (Control ctrl in Controls)
			{
				if (ctrl is IPropertySecureControl)
				{
					ApplySecurityRestrictionsForProperty(functions, ctrl);
				}
				if (ctrl is IMethodSecureControl)
				{
					ApplySecurityRestrictionsForMethod(functions, ctrl);
				}
			}
		}

		private void ApplySecurityRestrictionsForMethod(IList<Bagge.Seti.Security.BusinessEntities.Function> functions, Control ctrl)
		{
			var secureControl = (IMethodSecureControl)ctrl;
			switch (AccessibilityType.GetAccessibilityForMethod(Type.GetType(SecureTypeName), secureControl.MethodName, functions))
			{
				case AccessibilityTypes.None:
					secureControl.Visible = false;
					break;
				case AccessibilityTypes.Execute:
					secureControl.Visible = true;
					break;
			}
		}

		private void ApplySecurityRestrictionsForProperty(IList<Bagge.Seti.Security.BusinessEntities.Function> functions, Control ctrl)
		{
			var secureControl = (IPropertySecureControl)ctrl;

			switch (AccessibilityType.GetAccessibilityForProperty(Type.GetType(SecureTypeName), secureControl.PropertyName, functions))
			{
				case AccessibilityTypes.View:
					secureControl.ReadOnly = false;
					secureControl.Visible = true;
					break;
				case AccessibilityTypes.Edit:
					secureControl.ReadOnly = false;
					secureControl.Visible = true;
					break;
				case AccessibilityTypes.None:
					secureControl.Visible = false;
					break;
			}
		}

		#endregion
	}
}
