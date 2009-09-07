using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Bagge.Seti.WebSite.Helpers;


namespace Bagge.Seti.WebSite.Controls
{
	public partial class EditorCommands : System.Web.UI.UserControl
	{
		public EditorCommands()
		{
		}

		public string DefaultButton
		{
			set;
			get;
		}




		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ControlCollection ExtraButtons
		{
			get { return _extraButtons.Controls; }
		}

		public string DetailsViewID
		{
			get { return ViewState["DetailsViewID"] as string; }
			set { ViewState["DetailsViewID"] = value; }
		}
		public string AcceptPostBackUrl
		{
			get { return ViewState["AcceptPostBackUrl"] as string; }
			set { ViewState["AcceptPostBackUrl"] = value; }
		}
		public string CancelPostBackUrl
		{
			get { return _cancel.PostBackUrl; }
			set { _cancel.PostBackUrl = _back.PostBackUrl = value; }
		}

		public string AcceptText
		{
			get { return _accept.Text; }
			set { _accept.Text = value; }
		}

		public string CancelText
		{
			get { return _cancel.Text; }
			set { _cancel.Text = value; }
		}

		public string BackText
		{
			get { return _back.Text; }
			set { _back.Text = value; }
		}

		protected override void OnInit(EventArgs e)
		{
			if(DetailsView != null)
				DetailsView.DataBound += new EventHandler(DetailsView_DataBound);
			base.OnInit(e);
		}

		void DetailsView_DataBound(object sender, EventArgs e)
		{
			if (DetailsView.CurrentMode == DetailsViewMode.Edit && (!DetailsView.Visible || DetailsView.DataItemCount == 0))
			{
				_accept.Visible = _cancel.Visible = false;
				//_noRecord.Visible = true;
				_back.Visible = true;
			}
			else if (DetailsView.CurrentMode == DetailsViewMode.ReadOnly)
			{
				_accept.Visible = _cancel.Visible = false;
				_back.Visible = true;
			}
			else
				_back.Visible = false;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (DefaultButton != null)
				SetDefaultButton();
		}

		private void SetDefaultButton()
		{
			var button = ControlHelper.FindControlRecursive(this, DefaultButton);
			if (button != null)
				Page.Form.DefaultButton = button.UniqueID;
		}

		private DetailsView _detailsView;

		private DetailsView DetailsView
		{
			get
			{
				if (_detailsView == null)
					_detailsView = ControlHelper.FindControlRecursive(Page, DetailsViewID) as DetailsView;
				return _detailsView;
			}
		}

		private EventHandler _onAccept;

		public event EventHandler AcceptClick
		{
			add
			{
				_onAccept += value;
			}
			remove
			{
				_onAccept -= value;
			}
		}

		protected void OnAcceptClick(object sender, EventArgs e)
		{
			if (_onAccept != null)
				_onAccept(this, e);
		}
		protected void _accept_Click(object sender, EventArgs e)
		{
			if (_onAccept != null)
			{
				OnAcceptClick(sender, e);
				return;
			}
			Page.Validate();
			if (Page.IsValid)
			{
				if (DetailsView != null)
				{
					if (DetailsView.CurrentMode == DetailsViewMode.Insert)
						DetailsView.InsertItem(true);
					else if (DetailsView.CurrentMode == DetailsViewMode.Edit)
						DetailsView.UpdateItem(true);
				}
				Response.Redirect(AcceptPostBackUrl);
			}
		}
	}
}
