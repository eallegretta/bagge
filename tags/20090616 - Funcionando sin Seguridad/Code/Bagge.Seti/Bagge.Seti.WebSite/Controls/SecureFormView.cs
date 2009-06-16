using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.Common;

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
			IUser user = Page.User.Identity as IUser;
			var secureControl = (IMethodSecureControl)ctrl;
			if (user != null)
			{
				switch (IoCContainer.AccessibilityTypeManager.GetUserAccessibilityForMethod(
					user, Type.GetType(SecureTypeName), secureControl.MethodName))
				{
					case AccessibilityTypes.None:
						secureControl.Visible = false;
						break;
					case AccessibilityTypes.Execute:
						secureControl.Visible = true;
						break;
				}
			}
			else
				secureControl.Visible = false;
		}

		private void ApplySecurityRestrictionsForProperty(IList<Bagge.Seti.Security.BusinessEntities.Function> functions, Control ctrl)
		{
			IUser user = Page.User.Identity as IUser;
			var secureControl = (IPropertySecureControl)ctrl;
			if (user != null)
			{
				switch (IoCContainer.AccessibilityTypeManager.GetUserAccessibilityForProperty(
					user, Type.GetType(SecureTypeName), secureControl.PropertyName))
				{
					case AccessibilityTypes.View:
						secureControl.ReadOnly = true;
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
			else
				secureControl.Visible = false;
		}

		#endregion
	}
}
