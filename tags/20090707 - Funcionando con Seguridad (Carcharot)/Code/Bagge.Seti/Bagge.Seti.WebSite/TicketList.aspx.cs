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
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.WebSite
{

	[SecurizableWeb("Securizable_TicketList", typeof(TicketList), FunctionAction.Retrieve | FunctionAction.Delete)]
	public partial class TicketList : FilteredListPage<Ticket, int>, ITicketListView
	{

		TicketListPresenter _presenter;

		public TicketList()
		{
			_presenter = new TicketListPresenter(this, IoCContainer.TicketManager,
				IoCContainer.TicketStatusManager, IoCContainer.EmployeeManager, IoCContainer.CustomerManager);
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			Grid.RowDataBound += new GridViewRowEventHandler(Grid_RowDataBound);
		}

		private void Grid_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				if (!_presenter.CanAdministerTicket(e.Row.DataItem as Ticket))
				{
					int count = e.Row.Cells.Count;
					if (count > 0)
					{
						for (int index = 0; index < count; index++)
						{
							TableCell cell = e.Row.Cells[index];
							if (Grid.Columns[index].HeaderText.In(
								this.GetLocalResourceObject("EditField.HeaderText").ToString(),
								this.GetLocalResourceObject("DeleteField.HeaderText").ToString()))
								cell.Visible = false;

						}
					}
				}

			}
		}


		public override IList<FilterPropertyValue> Filters
		{
			get
			{
				var filters = new List<FilterPropertyValue>();

				AddTextBoxFilterValue<int>(_id, "Id", FilterPropertyValueType.Equals, filters);
				AddDropDownFilterValue<int>(_status, "Status", FilterPropertyValueType.Equals, filters);
				AddTextBoxFilterValue<string>(_description, "Description", FilterPropertyValueType.Contains, filters);
				AddDropDownFilterValue<int>(_employees, "Employees", FilterPropertyValueType.In, filters);
				AddCalendarFilterValue(_creationDate, "CreationDate", FilterPropertyValueType.Equals, filters);
				AddCalendarFilterValue(_executionDate, "ExecutionDate", FilterPropertyValueType.Equals, filters);
				AddDropDownFilterValue<int>(_customer, "Customer", FilterPropertyValueType.Equals, filters);

				return filters;
			}
		}

		protected override Button FilterButton
		{
			get { return _filter; }
		}

		protected override DropDownList DeletedDropDownList
		{
			get { return null; }
		}

		protected override GridView Grid
		{
			get { return _tickets; }
		}

		protected override Bagge.Seti.WebSite.Presenters.ListPresenter<Ticket, int> Presenter
		{
			get { return _presenter; }
		}

		protected override Microsoft.Practices.Web.UI.WebControls.ObjectContainerDataSource ObjectDataSource
		{
			get { return _dataSource; }
		}


		#region ITicketListView Members

		public TicketStatus[] Status
		{
			set
			{
				_status.DataSource = value;
				_status.DataBind();
			}
		}

		public Employee[] Technicians
		{
			set
			{
				_employees.DataSource = value;
				_employees.DataBind();
			}
		}

		public Customer[] Customers
		{
			set
			{
				_customer.DataSource = value;
				_customer.DataBind();
			}
		}

		#endregion
	}
}
