using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.Helpers;

namespace Bagge.Seti.WebSite.Controls
{
	public class SecurePropertyPlaceHolder: SecurePlaceHolder, IPropertySecureControl
	{
		public override void ApplySecurityRestrictions(IList<Bagge.Seti.Security.BusinessEntities.Function> functions)
		{
			switch (AccessibilityType.GetAccessibilityForProperty(Type.GetType(SecureTypeName), PropertyName, functions))
			{
				case AccessibilityTypes.None:
					Visible = false;
					return;
				case AccessibilityTypes.View:
					Visible = true;
					ControlHelper.DisableControlHierarchy(Controls);
					return;
				case AccessibilityTypes.Edit:
					Visible = true;
					ControlHelper.EnableControlHierarchy(Controls);
					return;
			}

		}


		#region IPropertySecureControl Members

		public string PropertyName
		{
			get;
			set;
		}

		public bool ReadOnly
		{
			get {  return (bool)(ViewState["ReadOnly"] ?? false); }
			set {  ViewState["ReadOnly"] = value; }
		}

		#endregion
	}
}
