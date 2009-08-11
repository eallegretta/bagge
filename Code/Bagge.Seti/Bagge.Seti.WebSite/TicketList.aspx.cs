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
using Bagge.Seti.WebSite.Controls;

namespace Bagge.Seti.WebSite
{

	[SecurizableCrud("Securizable_TicketList", typeof(TicketList), FunctionAction.Retrieve | FunctionAction.Delete)]
	public partial class TicketList : FilteredListPage<Ticket, int>, ITicketListView
	{
		public override string DefaultSortExpression
		{
			get
			{
				return "ExecutionDateTime DESC";
			}
		}


		TicketListPresenter _presenter;
		IUser _user;


		public TicketList()
		{
			_user = IoCContainer.User.Identity as IUser;

			_presenter = new TicketListPresenter(this, IoCContainer.TicketManager,
				IoCContainer.TicketStatusManager, IoCContainer.EmployeeManager, IoCContainer.CustomerManager,
				_user);
		}


		int? _editFieldIndex;
		int? _updateProgressFieldIndex;


		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			SetFieldIndices();

			if (IsTechnicianView)
			{
				_employeesLiteral.Visible = _employees.Visible = false;
				_new.Visible = false;
			}

			if (_editFieldIndex.HasValue)
			{
				Grid.RowDataBound += new GridViewRowEventHandler(Grid_RowDataBound);
				Grid.PreRender += new EventHandler(Grid_PreRender);
			}
			

			
		}

		void Grid_PreRender(object sender, EventArgs e)
		{
			if ((_editFieldIndex.HasValue && IsTechnicianView) || _allEditRowsHidden)
				Grid.Columns[_editFieldIndex.Value].Visible = false;

			if (_updateProgressFieldIndex.HasValue && _allUpdateProgressRowsHidden)
				Grid.Columns[_updateProgressFieldIndex.Value].Visible = false;
		}

		public bool IsTechnicianView
		{
			set;
			private get;
		}


		private void SetFieldIndices()
		{
			for (int index = 0; index < Grid.Columns.Count; index++)
			{
				var field = Grid.Columns[index] as IMethodSecureControl;
				if (field != null)
				{
					if (field.MethodName == "Update")
						_editFieldIndex = index;
					else if (field.MethodName == "UpdateProgress")
						_updateProgressFieldIndex = index;
				}
			}
		}


		bool _allEditRowsHidden = true;
		bool _allUpdateProgressRowsHidden = true;

		private void Grid_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				if (_editFieldIndex.HasValue && !_presenter.CanAdministerTicket(e.Row.DataItem as Ticket))
					e.Row.Cells[_editFieldIndex.Value].Visible = false;
				else
					_allEditRowsHidden = false;

				if (_updateProgressFieldIndex.HasValue && !_presenter.CanUpdateProgress(e.Row.DataItem as Ticket))
					e.Row.Cells[_updateProgressFieldIndex.Value].Visible = false;
				else
					_allUpdateProgressRowsHidden = false;
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
				if (!IsTechnicianView)
					AddDropDownFilterValue<int>(_employees, "Employees", FilterPropertyValueType.In, filters);
				else
					filters.Add("Employees", FilterPropertyValueType.In, _user.Id);
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
