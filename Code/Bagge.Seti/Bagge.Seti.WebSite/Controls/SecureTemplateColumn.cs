using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Bagge.Seti.WebSite.Controls
{
	public class SecureTemplateColumn : TemplateColumn, IPropertySecureControl, IMethodSecureControl
	{

		#region IPropertySecureControl Members

		public string PropertyName
		{
			get; set;
		}

		public bool ReadOnly
		{
			get;
			set;
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
