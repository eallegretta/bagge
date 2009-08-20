using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Bagge.Seti.WebSite.Controls
{
	public class SecureTemplateField: TemplateField, IPropertySecureControl, IMethodSecureControl
	{
		#region IPropertySecureControl Members

		public string PropertyName
		{
			get; set;
		}

		public bool ReadOnly
		{
			get { return (bool)(ViewState["ReadOnly"] ?? false); }
			set { ViewState["ReadOnly"] = value; }
		}

		public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
		{
			if (ReadOnly)
				rowState = DataControlRowState.Normal;

			base.InitializeCell(cell, cellType, rowState, rowIndex);
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
