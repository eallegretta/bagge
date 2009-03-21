using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.WebSite.Controls
{
	public class SecureGridView: GridView, ISecureControlContainer
	{
		public SecureGridView()
		{
			
		}

		#region ISecureControl Members

		public string SecureTypeName
		{
			get; set;
		}

		public void ApplySecurityRestrictions(IList<Function> functions)
		{
			foreach (DataControlField field in Columns)
			{
				if (field is IPropertySecureControl)
				{
					var secureField = (IPropertySecureControl)field;

					switch (AccessibilityType.GetAccessibilityForProperty(Type.GetType(SecureTypeName), secureField.PropertyName, functions))
					{
						case AccessibilityTypes.View:
							secureField.ReadOnly = false;
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
				else if (field is IMethodSecureControl)
				{
					var secureField = (IMethodSecureControl)field;
					switch (AccessibilityType.GetAccessibilityForMethod(Type.GetType(SecureTypeName), secureField.MethodName, functions))
					{
						case AccessibilityTypes.None:
							secureField.Visible = false;
							break;
						case AccessibilityTypes.Execute:
							secureField.Visible = true;
							break;
					}
				}
			}
		}

		#endregion
	}
}
