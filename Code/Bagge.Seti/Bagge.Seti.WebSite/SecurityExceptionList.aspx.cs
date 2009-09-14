using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.WebSite.Model;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.WebSite.Presenters;
using Bagge.Seti.Common;

namespace Bagge.Seti.WebSite
{
	[SecurizableCrud("Securizable_SecurityExceptionList", typeof(SecurityExceptionList), FunctionAction.List | FunctionAction.Delete)]
	public partial class SecurityExceptionList : Page, ISecurityExceptionListView, IListView
	{
		#region ISecurityExceptionListView Members

		SecurityExceptionListPresenter _presenter;

		public SecurityExceptionList()
		{
			_presenter = new SecurityExceptionListPresenter(this,
				IoCContainer.SecurityManager,
				IoCContainer.RoleManager);
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			_securityExceptions.RowDeleting += new GridViewDeleteEventHandler(_securityExceptions_RowDeleting);
		}

		void _securityExceptions_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			if (_securityExceptions.DataKeys.Count > 0 && e.RowIndex > -1)
			{
				int key = (int)_securityExceptions.DataKeys[e.RowIndex].Value;
				_presenter.Delete(key);
				_presenter.Select();
			}
		}

		public Role[] Roles
		{
			set
			{
				_role.DataSource = value;
				_role.DataBind();
			}
		}

		public int? SelectedRoleId
		{
			get
			{
				if (!string.IsNullOrEmpty(_role.SelectedValue))
					return _role.SelectedValue.ToInt32();
				return null;
			}
		}

		public bool ShowFunctions
		{
			set
			{
				_functionHolder.Visible = value;
			}
		}

		public Function[] Functions
		{
			set
			{
				var selectItem = _function.Items[0];
				_function.Items.Clear();
				_function.Items.Add(selectItem);
				_function.DataSource = value;
				_function.DataBind();
			}
		}

		public int? SelectedFunctionId
		{
			get
			{
				if (!string.IsNullOrEmpty(_function.SelectedValue))
					return _function.SelectedValue.ToInt32();
				return null;
			}
		}

		public object DataSource
		{
			set
			{
				_securityExceptions.DataSource = value;
			}
		}

		public event EventHandler SelectedRoleChanged
		{
			add
			{
				_role.SelectedIndexChanged += value;
			}
			remove
			{
				_role.SelectedIndexChanged -= value;
			}
		}

		public event EventHandler SelectedFunctionChanged
		{
			add
			{
				_function.SelectedIndexChanged += value;
			}
			remove
			{
				_function.SelectedIndexChanged -= value;
			}
		}
		#endregion

		#region IDeleteView Members

		public bool IsDelete
		{
			get
			{
				var keys = (from key in HttpContext.Current.Request.Form.AllKeys
							where key != null && (key.Contains("$delete.x") || key.Contains("$delete.y"))
							select key);


				if (keys != null && keys.ToArray().Length == 2)
					return true;

				return false;
			}
		}

		public int TotalRows
		{
			set { }
		}

		public string DefaultSortExpression
		{
			get { return string.Empty; }
		}

		#endregion
	}
}
