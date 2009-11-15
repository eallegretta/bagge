using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.Common;
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.DesignByContract;
using Bagge.Seti.BusinessEntities.Exceptions;

namespace Bagge.Seti.WebSite.Presenters
{
	public class TicketEditorPresenter: EditorPresenter<Ticket, int>
	{
		IEmployeeManager _employeeManager;
		ICustomerManager _customerManager;
		ITicketStatusManager _ticketStatusManager;
		IProductManager _productManager;
		IUser _loggedUser;

		public TicketEditorPresenter(ITicketEditorView view, 
			ITicketManager ticketManager,
			IEmployeeManager employeeManager,
			ICustomerManager customerManager,
			ITicketStatusManager ticketStatusManager,
			IProductManager productManager,
			IUser loggedUser): base(view, ticketManager)
		{
			_employeeManager = employeeManager;
			_customerManager = customerManager;
			_ticketStatusManager = ticketStatusManager;
			_productManager = productManager;
			_loggedUser = loggedUser;
		}

		protected new ITicketEditorView View
		{
			get { return GetView<ITicketEditorView>(); }
		}



		protected override void OnInit(object sender, EventArgs e)
		{
			base.OnInit(sender, e);
			GetManager<ITicketManager>().EmailUrl = View.EmailUrl;
			View.DataBound += new EventHandler(View_DataBound);
		}

		void View_DataBound(object sender, EventArgs e)
		{
			switch (View.Mode)
			{
				case EditorAction.Insert:
				case EditorAction.Update:
					View.Customers = _customerManager.FindAllActiveOrdered("Name");
					if(!View.IsUpdateProgress)
						View.Technicians = _employeeManager.FindAllActiveTechnicians();

					if (View.Mode == EditorAction.Insert)
						View.TicketStatus = _ticketStatusManager.Get(TicketStatusEnum.PendingAproval).ToSingleItemArray();
					else
					{
						if (SelectedEntity.Status == TicketStatusEnum.CanceledBySystem)
							View.TicketStatus = _ticketStatusManager.FindAll();
						else
							View.TicketStatus = _ticketStatusManager.FindAllWithoutCanceledBySystem();
					}

					if (View.Mode == EditorAction.Update)
					{
						if (SelectedEntity.Employees != null && !View.IsUpdateProgress)
							View.AssignedTechniciansIds = SelectedEntity.Employees.Select(emp => emp.Id).ToArray();

						if (View.IsUpdateProgress)
						{
							View.Technicians = SelectedEntity.Employees.ToArray();
							if (SelectedEntity.Status == TicketStatusEnum.Closed)
								throw new BusinessRuleException(Properties.Resources.CannotUpdateStatusClosedTicketErrorMessage);

							View.ShowCloseButton = true;
						}
						else
							View.ShowCloseButton = false;
						View.ShowApproveButton = false;
						
					}
					else
					{
						View.ShowApproveButton = true;
						View.ShowCloseButton = false;
					}


					break;
				case EditorAction.View:
					View.Technicians = SelectedEntity.Employees.ToArray();
					View.ShowApproveButton = View.ShowCloseButton = false;
					break;
			}
			if (!View.IsPostBack && View.Mode.In(EditorAction.Update, EditorAction.View))
			{
				View.Products = SelectedEntity.Products.ToArray();
				View.SelectedCustomerId = SelectedEntity.Customer.Id;
				
				View.SelectedTicketStatus = (TicketStatusEnum)SelectedEntity.Status.Id;
			}
		}

		public void CloseTicket()
		{
			GetManager<ITicketManager>().Close(View.PrimaryKey);
		}

		public bool CanApprove()
		{
			if (View.AssignedTechniciansIds == null)
				return false;

			return View.AssignedTechniciansIds.Length > 0;
		}

		public bool BudgetIsRequired()
		{
			var customer = _customerManager.Get(View.SelectedCustomerId);
			return !(customer.Subscription);
		}

		public void UpdateProgress(decimal? duration, string notes)
		{
			GetManager<ITicketManager>().UpdateProgress(View.PrimaryKey, duration == null ? decimal.MinValue : duration.Value, notes);
		}

		public void ApproveTicket(Ticket entity)
		{
			Check.Require(View.Mode == EditorAction.Insert);

			entity.Customer = _customerManager.Get(View.SelectedCustomerId);
			entity.Employees = new List<Employee>();
			
			foreach (var technicianId in View.AssignedTechniciansIds)
				entity.Employees.Add(_employeeManager.Get(technicianId));

			entity.Products = View.Products;

			entity.Status = _ticketStatusManager.Get(TicketStatusEnum.Open);

			entity.Creator = _employeeManager.GetByUsername(_loggedUser.Username);

			GetManager<ITicketManager>().CreateApproved(entity);
		}

		public override void Save(Ticket entity)
		{
			entity.Customer = _customerManager.Get(View.SelectedCustomerId);
			entity.Employees = new List<Employee>();
			foreach (var technicianId in View.AssignedTechniciansIds)
				entity.Employees.Add(_employeeManager.Get(technicianId));
			entity.Status = _ticketStatusManager.Get(View.SelectedTicketStatus);
			entity.Products = View.Products;

			if (View.Mode == EditorAction.Insert)
				entity.Creator = _employeeManager.GetByUsername(_loggedUser.Username);
			else
				entity.Creator = new Employee();


			base.Save(entity);
		}

	}
}
