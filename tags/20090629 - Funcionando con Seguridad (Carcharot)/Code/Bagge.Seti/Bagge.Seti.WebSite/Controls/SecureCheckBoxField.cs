using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Bagge.Seti.WebSite.Controls
{
	public class SecureCheckBoxField: CheckBoxField, IPropertySecureControl, IMethodSecureControl
	{
		#region IPropertySecureControl Members

		public string PropertyName
		{
			get	{ return DataField; }
			set { DataField = value; }
		}

		#endregion

		#region IMethodSecureControl Members

		public string MethodName
		{
			get; set;
		}

		#endregion
	}
}
