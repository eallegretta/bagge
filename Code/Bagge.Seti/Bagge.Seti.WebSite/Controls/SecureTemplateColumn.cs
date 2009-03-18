using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Bagge.Seti.WebSite.Controls
{
	public class SecureTemplateColumn : TemplateColumn, IPropertySecureControl
	{

		#region IPropertySecureControl Members

		public string PropertyName
		{
			get; set;
		}

		#endregion
	}
}
