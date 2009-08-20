using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.Common;

namespace Bagge.Seti.WebSite.Controls
{
	public class SecureDetailsView: DetailsView, ISecureControlContainer
	{
		#region ISecureControl Members

		public string SecureTypeName
		{
			get; set;
		}


		public void ApplySecurityRestrictions(IList<Bagge.Seti.Security.BusinessEntities.Function> functions)
		{
			foreach (DataControlField field in Fields)
			{
				if (field is IPropertySecureControl)
				{
					ApplySecurityRestrictionsForProperty(functions, field);
				}
				if (field is IMethodSecureControl)
				{
					ApplySecurityRestrictionsForMethod(functions, field);
				}
			}
		}

		private void ApplySecurityRestrictionsForMethod(IList<Bagge.Seti.Security.BusinessEntities.Function> functions, DataControlField field)
		{
			IUser user = Page.User.Identity as IUser;
			var secureField = (IMethodSecureControl)field;
			if (user != null)
			{
				switch (IoCContainer.AccessibilityTypeManager.GetUserAccessibilityForMethod(
					user, Type.GetType(SecureTypeName), secureField.MethodName))
				{
					case AccessibilityTypes.None:
						secureField.Visible = false;
						break;
					case AccessibilityTypes.Execute:
						secureField.Visible = true;
						break;
				}
			}
			else
				secureField.Visible = false;
		}

		private void ApplySecurityRestrictionsForProperty(IList<Bagge.Seti.Security.BusinessEntities.Function> functions, DataControlField field)
		{
			IUser user = Page.User.Identity as IUser;
			var secureField = (IPropertySecureControl)field;

			if (user != null)
			{
				switch (IoCContainer.AccessibilityTypeManager.GetUserAccessibilityForProperty(
					user, Type.GetType(SecureTypeName), secureField.PropertyName))
				{
					case AccessibilityTypes.View:
						secureField.ReadOnly = true;
						secureField.Visible = true;
						break;
					case AccessibilityTypes.Edit:
						secureField.ReadOnly = false;
						secureField.Visible = true;
						break;
					case AccessibilityTypes.None:
						secureField.Visible = false;
						break;
				}
			}
			else
				secureField.Visible = false;
		}

		#endregion
	}
}
