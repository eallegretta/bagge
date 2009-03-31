using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Bagge.Seti.WebSite.Controls
{
	public class SecureBoundField: System.Web.UI.WebControls.BoundField, IPropertySecureControl
	{

		public SecureBoundField()
		{
			Validators = new List<BaseValidator>();
		}

		[PersistenceMode(PersistenceMode.InnerProperty)]
		public List<BaseValidator> Validators
		{
			get;
			private set;
		}


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

			if ((rowState & DataControlRowState.Insert) == DataControlRowState.Insert || 
				(rowState & DataControlRowState.Edit) == DataControlRowState.Edit)
			{
				if (cell.Controls.Count == 1)
				{
					TextBox textBox = cell.Controls[0] as TextBox;
					if (string.IsNullOrEmpty(textBox.ID))
						textBox.ID = DataField + "_txt";
					if (textBox != null)
					{
						SetupMaxLength(textBox);
						AddPropertyProxyValidator();
						SetupValidators(cell, textBox);
					}
				}
			}
		}

		private void AddPropertyProxyValidator()
		{
			PropertyProxyValidator validator = new PropertyProxyValidator();
			validator.SourceTypeName = ((ISecureControlContainer)Control).SecureTypeName;
			validator.PropertyName = DataField;
			validator.DisplayMode = ValidationSummaryDisplayMode.SingleParagraph;
			Validators.Add(validator);
		}

		private void SetupValidators(DataControlFieldCell cell, TextBox textBox)
		{
			foreach (var validator in Validators)
			{
				validator.ControlToValidate = textBox.ID;
				validator.Display = ValidatorDisplay.Dynamic;
				cell.Controls.Add(validator);
			}
		}

		private void SetupMaxLength(TextBox textBox)
		{
			if (MaxLength > 0)
				textBox.MaxLength = MaxLength;
		}

		#region IPropertySecureControl Members

		public string PropertyName
		{
			get{ return DataField; }
			set { DataField = value; }
		}

		#endregion
	}
}
