using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.WebSite.Presenters;
using Bagge.Seti.Common;
using Microsoft.Practices.Web.UI.WebControls;
using System.Threading;
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.WebSite.Controls;
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.WebSite
{
	[SecurizableWeb("Securizable_TicketEditor", typeof(TicketEditor), FunctionAction.Retrieve | FunctionAction.Create | FunctionAction.Update)]
	public partial class TicketEditor : EditorPage<Ticket, int>, ITicketEditorView
	{
		TicketEditorPresenter _presenter;

		public TicketEditor()
		{
			_presenter = new TicketEditorPresenter(this, IoCContainer.TicketManager,
				IoCContainer.EmployeeManager, IoCContainer.CustomerManager,
				IoCContainer.TicketStatusManager, IoCContainer.ProductManager, IoCContainer.User.Identity as IUser);



		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			if(IsUpdateProgress)
				_commands.AcceptClick += new EventHandler(_commands_AcceptClick);
		}

		public bool IsUpdateProgress
		{
			get
			{
				if (Mode != EditorAction.Update)
					return false;

				return Request.QueryString["UpdateProgress"].ToBoolean(false) || ((Employee)IoCContainer.User.Identity).IsTechnician;
			}
		}

		public string EmailUrl
		{
			get
			{
				return Request.Url.Scheme + "://" + Request.Url.Authority + "/TicketEditor.aspx?Id=" + PrimaryKey + "&Action=View";
			}
		}

		public bool ShowApproveButton
		{
			set
			{
				_approve.Visible = value;
				
			}
		}

		public bool ShowCloseButton
		{
			set
			{
				_close.Visible = value;
			}
		}

		void _commands_AcceptClick(object sender, EventArgs e)
		{
			UpdateTicketProgress();
			Response.Redirect(_commands.AcceptPostBackUrl);
		}

		private void UpdateTicketProgress()
		{
			var durationTextBox = Details.FindControl("_realDuration") as TextBox;
			var notes = Details.FindControl("Notes_txt") as TextBox;

			if (durationTextBox != null && notes != null)
			{
				decimal duration;

				if (decimal.TryParse(durationTextBox.Text, out duration))
				{
					decimal? durationNullable = duration;
					_presenter.UpdateProgress(durationNullable, notes.Text);
				}
				else
					_presenter.UpdateProgress(null, notes.Text);
			}
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);


			if (IsUpdateProgress)
				SetUpdateProgessView();
		}

		private void SetUpdateProgessView()
		{

			foreach (DataControlField field in ((DetailsView)Details).Fields)
			{
				if (field is IPropertySecureControl)
				{
					string propertyName = ((IPropertySecureControl)field).PropertyName;
					if (propertyName != "RealDuration" && propertyName != "Notes")
						((IPropertySecureControl)field).ReadOnly = true;
				}
			}
		}


		bool _isApprove = false;

		protected override void OnInserting(Ticket instance)
		{
			SetCustomerArrivalTime(instance);
			if (!_isApprove)
				_presenter.Save(instance);
			else
				_presenter.ApproveTicket(instance);
		}

		private void SetCustomerArrivalTime(Ticket instance)
		{
			if (instance == null)
				return;

			var customerArrival = Details.FindControl("_customerArrival") as TextBox;
			if (customerArrival != null)
			{
				if (instance.ExecutionDateTime != null)
				{
					var time = TimeSpan.Parse(customerArrival.Text);

					instance.ExecutionDateTime = new DateTime(instance.ExecutionDateTime.Value.Year,
						instance.ExecutionDateTime.Value.Month,
						instance.ExecutionDateTime.Value.Day,
						time.Hours,
						time.Minutes,
						0);
				}

			}
		}

		protected override void OnUpdating(Ticket instance)
		{
			SetCustomerArrivalTime(instance);
			if (!_isApprove)
				_presenter.Save(instance);
			else
				_presenter.ApproveTicket(instance);
		}


		protected override EditorPresenter<Ticket, int> Presenter
		{
			get { return _presenter; }
		}

		protected override CompositeDataBoundControl Details
		{
			get { return _details; }
		}

		protected override ObjectContainerDataSource ObjectDataSource
		{
			get { return _dataSource; }
		}


		#region ITicketEditorView Members

		public Employee[] Technicians
		{
			set
			{
				var employees = Details.FindControl("_employees") as BaseDataBoundControl;
				if (employees != null)
				{
					employees.DataSource = value;
					employees.DataBind();
				}
			}
		}

		public Customer[] Customers
		{
			set
			{
				var customers = Details.FindControl("_customer") as DropDownList;
				if (customers != null)
				{
					customers.DataSource = value;
					customers.DataBind();
				}
			}
		}


		#endregion


		protected override bool ShowRequiredInformationLabel
		{
			get { return true; }
		}

		#region ITicketEditorView Members


		public int[] AssignedTechniciansIds
		{
			get
			{
				var employees = Details.FindControl("_employees") as BaseDataBoundControl;
				if (employees is CheckBoxList)
				{
					var ids = (from item in ((CheckBoxList)employees).Items.Cast<ListItem>()
							   where item.Selected == true
							   select item.Value.ToInt32());
					return ids.ToArray();
				}
				else if (employees is BulletedList)
				{
					var ids = (from item in ((BulletedList)employees).Items.Cast<ListItem>()
							   select item.Value.ToInt32());
					return ids.ToArray();
				}

				return null;
			}
			set
			{
				var employees = Details.FindControl("_employees") as CheckBoxList;
				if (employees != null)
				{
					var items = employees.Items.Cast<ListItem>().Where(item => value.Contains(item.Value.ToInt32())).Select(item => item).ToArray();

					foreach (var item in items)
						item.Selected = true;
				}
			}
		}

		public int SelectedCustomerId
		{
			get
			{
				return GetControlPropertyValue(Details.FindControl("_customer"), 0, "Value", "SelectedValue");
			}
			set
			{
				SetControlPropertyValue(Details.FindControl("_customer"), value.ToString(), "Value", "SelectedValue");
			}
		}

		public TicketStatus[] TicketStatus
		{
			set
			{
				var status = Details.FindControl("_status") as DropDownList;
				if (status != null)
				{
					status.DataSource = value;
					status.DataBind();
				}
			}
		}

		public TicketStatusEnum SelectedTicketStatus
		{
			get
			{
				return (TicketStatusEnum)GetControlPropertyValue<int>(Details.FindControl("_status"), (int)TicketStatusEnum.Initial, "Value", "SelectedValue");
			}
			set
			{
				SetControlPropertyValue(Details.FindControl("_status"), ((int)value).ToString(), "Value", "SelectedValue");
			}
		}

		public ProductTicket[] Products
		{
			set
			{
				var products = Details.FindControl("_products") as ProductTicketSelectionGrid;
				if (products != null)
				{
					products.SelectedItems = value;
				}
			}
			get
			{
				var products = Details.FindControl("_products") as ProductTicketSelectionGrid;
				if (products != null)
				{
					return products.SelectedItems.ToArray();
				}
				return null;
			}
		}

		#endregion

		protected void _employeesValidator_ServerValidate(object sender, ServerValidateEventArgs e)
		{
			//Only validate when Approve is clicked
			if (!string.IsNullOrEmpty(Request.Form[_approve.UniqueID]))
				e.IsValid = _presenter.CanApprove();
			else if (Mode == EditorAction.Update && SelectedTicketStatus == TicketStatusEnum.Open)
				e.IsValid = _presenter.CanApprove();
			else
				e.IsValid = true;
		}

		protected void _customerArrivalVal_ServerValidate(object sender, ServerValidateEventArgs e)
		{
			DateTime date;
			e.IsValid = DateTime.TryParse(e.Value, out date);
		}

		protected void _approve_Click(object sender, EventArgs e)
		{
			Page.Validate();
			if (Page.IsValid)
			{
				_isApprove = true;
				if (Mode == EditorAction.Insert)
					((DetailsView)Details).InsertItem(true);
				else if (Mode == EditorAction.Update)
					((DetailsView)Details).UpdateItem(true);
				
				Response.Redirect(_commands.AcceptPostBackUrl);
			}
		}

		protected void _close_Click(object sender, EventArgs e)
		{
			UpdateTicketProgress();
			_presenter.CloseTicket();
			Response.Redirect(_commands.AcceptPostBackUrl);
		}

	}
}
