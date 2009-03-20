using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.Helpers;

namespace Bagge.Seti.WebSite.Controls
{
	public class SecureMethodPlaceHolder: SecurePlaceHolder, IMethodSecureControl
	{
		public override void ApplySecurityRestrictions(IList<Bagge.Seti.Security.BusinessEntities.Function> functions)
		{
			switch (AccessibilityType.GetAccessibilityForProperty(Type.GetType(SecureTypeName), MethodName, functions))
			{
				case AccessibilityTypes.None:
					Visible = false;
					return;
				case AccessibilityTypes.Execute:
					Visible = true;
					return;
			}
		}

		#region IMethodSecureControl Members

		public string MethodName
		{
			get; set;
		}
		#endregion
	}
}
