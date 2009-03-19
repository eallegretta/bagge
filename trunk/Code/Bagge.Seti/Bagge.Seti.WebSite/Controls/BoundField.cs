using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet;

namespace Bagge.Seti.WebSite.Controls
{
	public class BoundField: System.Web.UI.WebControls.BoundField
	{
		public int MaxLength
		{
			get
			{
				int i = 0;
				if (this.ViewState["MaxLength"] != null)
					i = Int32.Parse(this.ViewState["MaxLength"].ToString());
				return i;
			}
			set { this.ViewState["MaxLength"] = value; }
		}

		protected override void InitializeDataCell(DataControlFieldCell cell, DataControlRowState rowState)
		{
			base.InitializeDataCell(cell, rowState);

			if (cell.Controls.Count == 1)
			{
				TextBox textBox = cell.Controls[0] as TextBox;
				if (textBox != null)
				{
					SetupMaxLength(textBox);
					SetupValidator(cell, textBox);
				}
			}
		}

		private void SetupValidator(DataControlFieldCell cell, TextBox textBox)
		{
			PropertyProxyValidator validator = new PropertyProxyValidator();
			validator.SourceTypeName = ((ISecureControl)Control).SecureTypeName;
			validator.PropertyName = DataField;
			if (string.IsNullOrEmpty(textBox.ID))
				textBox.ID = DataField + "_txt";
			validator.ControlToValidate = textBox.ID;
			cell.Controls.Add(validator);
		}

		private void SetupMaxLength(TextBox textBox)
		{
			if (MaxLength > 0)
				textBox.MaxLength = MaxLength;
		}
	}
}
