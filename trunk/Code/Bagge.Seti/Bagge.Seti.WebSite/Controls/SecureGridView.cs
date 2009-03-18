using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.WebSite.Controls
{
	public class SecureGridView: GridView, ISecureControl
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
				if (field is BoundField || field is IPropertySecureControl)
				{
					string propertyName;
					if(field is BoundField)
						propertyName = ((BoundField)field).DataField;
					else
						propertyName = ((IPropertySecureControl)field).PropertyName;

					switch (AccessibilityType.GetAccessibilityForProperty(Type.GetType(SecureTypeName), propertyName, functions))
					{
						case AccessibilityTypes.Edit:
						case AccessibilityTypes.View:
							field.Visible = true;
							break;
						case AccessibilityTypes.None:
							field.Visible = false;
							break;
					}

				}
			}
		}

		#endregion
	}
}
