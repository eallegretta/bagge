using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.WebSite.Presenters;
using Bagge.Seti.Common;

namespace Bagge.Seti.WebSite
{
	public partial class FunctionEditor : EditorPage<Function, int> , IFunctionEditorView
	{
		FunctionEditorPresenter _presenter;

		public FunctionEditor()
		{
			_presenter = new FunctionEditorPresenter(this, IoCContainer.FunctionManager, IoCContainer.RoleManager);
		}
		
		#region IFunctionEditorView Members



		public Role[] Roles
		{
			set { throw new NotImplementedException(); }
		}

		public int SelectedRoleId
		{
			get { throw new NotImplementedException(); }
		}

		public string[] Classnames
		{
			set { throw new NotImplementedException(); }
		}

		public string SelectedClassname
		{
			get { throw new NotImplementedException(); }
		}

		public AccessibilityType[] Accessibilities
		{
			set { throw new NotImplementedException(); }
		}

		public byte SelectedAccessibilityTypeId
		{
			get { throw new NotImplementedException(); }
		}

		public string[] Members
		{
			set { throw new NotImplementedException(); }
		}

		public string SelectedMember
		{
			get { throw new NotImplementedException(); }
		}

		public string[] ConstraintTypes
		{
			set { throw new NotImplementedException(); }
		}

		public string SelectedConstraintType
		{
			get { throw new NotImplementedException(); }
		}

		public bool ConstraintTypesVisible
		{
			set { throw new NotImplementedException(); }
		}

		public bool ConstraintValueVisible
		{
			set { throw new NotImplementedException(); }
		}

		public string SelectedConstraintValue
		{
			get { throw new NotImplementedException(); }
		}

		#endregion

		protected override Bagge.Seti.WebSite.Presenters.EditorPresenter<Function, int> Presenter
		{
			get { return _presenter; }
		}

		protected override CompositeDataBoundControl Details
		{
			get { throw new NotImplementedException(); }
		}

		protected override Microsoft.Practices.Web.UI.WebControls.ObjectContainerDataSource ObjectDataSource
		{
			get { throw new NotImplementedException(); }
		}
	}
}
