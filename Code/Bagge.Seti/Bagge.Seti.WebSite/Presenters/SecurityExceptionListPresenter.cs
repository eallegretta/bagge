using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.DesignByContract;
using Bagge.Seti.WebSite.Model;
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.WebSite.Presenters
{
	public class SecurityExceptionListPresenter
	{
		ISecurityExceptionListView _view;
		ISecurityManager _manager;
		IRoleManager _roleManager;

		public SecurityExceptionListPresenter(ISecurityExceptionListView view,
			ISecurityManager manager,
			IRoleManager roleManager)
	    {
			Check.Require(view != null);
			Check.Require(manager != null);
			Check.Require(roleManager != null);

			_view = view;
			_manager = manager;
			_roleManager = roleManager;
			_view.Init += new EventHandler(OnInit);
			_view.Load += new EventHandler(OnLoad);
	    }

		void OnLoad(object sender, EventArgs e)
		{
			if (!_view.IsPostBack)
				_view.Roles = _roleManager.FindAllActiveOrdered("Name").Where(r => r.Id != Role.SuperAdministratorId).ToArray();
			else
				Select();
		}

		void _view_SelectedFunctionChanged(object sender, EventArgs e)
		{
			Select();
		}

		public void Select()
		{
			if (_view.SelectedRoleId.HasValue &&
						 _view.SelectedFunctionId.HasValue)
				_view.DataSource = GetSecurityExceptions();
			else
				_view.DataSource = null;
			_view.DataBind();
		}

		private IList<SecurityExceptionViewModel> GetSecurityExceptions()
		{
			var securityExceptions = _manager.FindAllSecurityExceptions(
							_view.SelectedRoleId.Value,
							_view.SelectedFunctionId.Value);

			var securityExceptionsViewModel = new List<SecurityExceptionViewModel>();
			foreach (var securityException in securityExceptions)
			{
				securityExceptionsViewModel.Add(
					new SecurityExceptionViewModel(securityException));
			}
			return securityExceptionsViewModel;
		}

		void _view_SelectedRoleChanged(object sender, EventArgs e)
		{
			if (_view.SelectedRoleId.HasValue)
			{
				var role = _roleManager.Get(_view.SelectedRoleId.Value);
				var functions = from f in role.Functions
								orderby f.Name
								select f;
				_view.ShowFunctions = true;
				_view.Functions = functions.ToArray();
			}
			else
			{
				_view.ShowFunctions = false;
			}
		}

	    protected void OnInit(object sender, EventArgs e)
	    {
			_view.SelectedRoleChanged += new EventHandler(_view_SelectedRoleChanged);
			_view.SelectedFunctionChanged += new EventHandler(_view_SelectedFunctionChanged);
		}


		public void Delete(int id)
		{
			_manager.Delete(id);
		}
	}
}
