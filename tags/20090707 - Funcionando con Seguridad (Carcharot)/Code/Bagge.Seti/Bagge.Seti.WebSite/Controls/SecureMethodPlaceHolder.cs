using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.Common;

namespace Bagge.Seti.WebSite.Controls
{
	public class SecureMethodPlaceHolder: SecurePlaceHolder, IMethodSecureControl
	{
		public override void ApplySecurityRestrictions(IList<Bagge.Seti.Security.BusinessEntities.Function> functions)
		{
			IUser user = Page.User.Identity as IUser;

			if (user != null)
			{
				switch (IoCContainer.AccessibilityTypeManager.GetUserAccessibilityForProperty(
					user, Type.GetType(SecureTypeName), MethodName))
				{
					case AccessibilityTypes.None:
						Visible = false;
						return;
					case AccessibilityTypes.Execute:
						Visible = true;
						return;
				}
			}
			else
				Visible = false;
		}

		#region IMethodSecureControl Members

		public string MethodName
		{
			get; set;
		}
		#endregion
	}
}
