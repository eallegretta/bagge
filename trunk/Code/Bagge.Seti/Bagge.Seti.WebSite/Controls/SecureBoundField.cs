using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Threading;

namespace Bagge.Seti.WebSite.Controls
{
	public class SecureBoundField : System.Web.UI.WebControls.BoundField, IPropertySecureControl
	{

		public string Mask
		{
			get { return ViewState["Mask"] as string; }
			set
			{
				value = value.Replace("{NumberSeparator}", Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
				ViewState["Mask"] = value;
			}
		}

		public string MaskPlaceHolder
		{
			get { return ViewState["MaskPlaceHolder"] as string; }
			set { ViewState["MaskPlaceHolder"] = value; }
		}

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

		public string DefaultValue
		{
			get { return ViewState["DefaultValue"] as string; }
			set
			{
				value = value.Replace("{NumberSeparator}", Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
				ViewState["DefaultValue"] = value;
			}
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
						textBox.Text = DefaultValue;
						if (!string.IsNullOrEmpty(Mask))
							RegisterMask(cell, textBox);

						SetupMaxLength(textBox);
						AddPropertyProxyValidator();
						SetupValidators(cell, textBox);
					}
				}
			}
		}

		private void RegisterMask(DataControlFieldCell cell, TextBox textBox)
		{
			if (!Control.Page.ClientScript.IsClientScriptIncludeRegistered("SecureBoundFieldMask"))
				Control.Page.ClientScript.RegisterClientScriptInclude("SecureBoundFieldMask", Control.Page.ResolveUrl("~/Scripts/jquery.maskedinput-1.2.2.min.js"));


			string placeHolder = string.Empty;
			if (!string.IsNullOrEmpty(MaskPlaceHolder))
				placeHolder = string.Format(", {{ placeholder: '{0}' }}", MaskPlaceHolder);

			string clientId = Control.ClientID + "_" + textBox.ClientID;
			string mask = string.Format("<script type='text/javascript'>$('#{0}').mask('{1}'{2});</script>", clientId, Mask, placeHolder);

			cell.Controls.Add(new LiteralControl(mask));
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
			get { return DataField; }
			set { DataField = value; }
		}

		#endregion
	}
}
