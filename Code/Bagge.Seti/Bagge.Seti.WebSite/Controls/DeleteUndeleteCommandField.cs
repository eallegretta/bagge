﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eaa.Framework.Web.UI.WebControls;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Web.UI;

namespace Bagge.Seti.WebSite.Controls
{
	public class DeleteUndeleteCommandField: DeleteCommandField, IMethodSecureControl
	{
		public string DeleteDataField
		{
			get { return ViewState["DeleteDataField"] as string; }
			set { ViewState["DeleteDataField"] = value; }
		}

		public string UndeleteConfirmationMessage
		{
			get { return ViewState["UndeleteConfirmationMessage"] as string; }
			set { ViewState["UndeleteConfirmationMessage"] = value; }
		}

		public string UndeleteImageUrl
		{
			get { return ViewState["UndeleteImageUrl"] as string; }
			set { ViewState["UndeleteImageUrl"] = value; }
		}

		public string UndeleteText
		{
			get { return ViewState["UndeleteText"] as string; }
			set { ViewState["UndeleteText"] = value; }
		}

		public bool ShowUndelete
		{
			get { return (bool)(ViewState["ShowUndelete"] ?? true); }
			set { ViewState["ShowUndelete"] = value; }
		}

		protected override System.Web.UI.WebControls.DataControlField CreateField()
		{
			return new DeleteUndeleteCommandField();
		}

		IButtonControl _control;

		private void SetButtonProperties(bool isDelete)
		{
			_control.Text = (isDelete) ? this.Text : this.UndeleteText;
			_control.CommandName = (isDelete) ? "Delete" : "Undelete";
			
			if(_control is ImageButton)
				((ImageButton)_control).ImageUrl = (isDelete) ? this.ImageUrl : this.UndeleteImageUrl;

			if (isDelete)
			{
				if (!string.IsNullOrEmpty(this.ConfirmationMessage))
				{
					((WebControl)_control).Attributes.Add("onclick", "if(!confirm('" + this.ConfirmationMessage + "')) return false;");
				}

				((WebControl)_control).ID = "delete";
			}
			else
			{
				if (!ShowUndelete)
				{
					((WebControl)_control).Visible = false;
					return;
				}

				if (!string.IsNullOrEmpty(this.UndeleteConfirmationMessage))
				{
					((WebControl)_control).Attributes.Add("onclick", "if(!confirm('" + this.UndeleteConfirmationMessage + "')) return false;");
				}

				((WebControl)_control).ID = "undelete";
			}
		}

		// Methods
		private void AddButtonToCell(DataControlFieldCell cell, int rowIndex)
		{
			bool isButton = true;
			switch (this.ButtonType)
			{
				case ButtonType.Button:
					_control = new Button();
					break;

				case ButtonType.Link:
					_control = new LinkButton();
					isButton = false;
					break;

				default:
					_control = new ImageButton();
					break;
			}

			_control.CommandArgument = rowIndex.ToString(CultureInfo.InvariantCulture);
			
			if (isButton)
			{
				_control.CausesValidation = this.CausesValidation;
			}
			_control.ValidationGroup = this.ValidationGroup;
			cell.Controls.Add((WebControl)_control);
		}

		public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
		{
			
			if (cellType == DataControlCellType.DataCell)
			{
				cell.DataBinding += new EventHandler(cell_DataBinding);
				this.AddButtonToCell(cell, rowIndex);
			}
			else
				base.InitializeCell(cell, cellType, rowState, rowIndex);
		}

		void cell_DataBinding(object sender, EventArgs e)
		{
			bool isDeleted = (bool)DataBinder.GetDataItem(((Control)sender).NamingContainer).GetPropertyValue(DeleteDataField);
			SetButtonProperties(!isDeleted);
		}

		public override void ExtractValuesFromCell(System.Collections.Specialized.IOrderedDictionary dictionary, DataControlFieldCell cell, DataControlRowState rowState, bool includeReadOnly)
		{
			base.ExtractValuesFromCell(dictionary, cell, rowState, includeReadOnly);
		}


		#region IMethodSecureControl Members

		public string MethodName
		{
			get; set;
		}

		#endregion
	}
}
