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

namespace Bagge.Seti.WebSite
{
	public partial class TicketEditor : EditorPage<Ticket, int>, ITicketEditorView
	{
		TicketEditorPresenter _presenter;

		public TicketEditor()
		{
			_presenter = new TicketEditorPresenter(this, IoCContainer.TicketManager,
				IoCContainer.EmployeeManager, IoCContainer.CustomerManager,
				IoCContainer.TicketStatusManager, IoCContainer.ProductManager, IoCContainer.User.Identity as IUser);

			

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
				else if(employees is  BulletedList)
				{
				}

				return null; 
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
				SetControlPropertyValue(Details.FindControl("_customer"), value, "Value", "SelectedValue");
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
				return GetControlPropertyValue<TicketStatusEnum>(Details.FindControl("_status"), TicketStatusEnum.Initial, "Value", "SelectedValue");
			}
			set
			{
				SetControlPropertyValue(Details.FindControl("_status"), (int)value, "Value", "SelectedValue");
			}
		}

		public ProductTicket[] Products
		{
			set { }
			get { return null; }
		}

		#endregion
	}
}
