using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.WebSite.Presenters;
using Bagge.Seti.Common;

namespace Bagge.Seti.WebSite
{
	public partial class EditProfile : EditorPage<Employee, int>, IEditProfileView 
	{
		EditProfilePresenter _presenter;

		public EditProfile()
		{
			_presenter = new EditProfilePresenter(this, IoCContainer.EmployeeManager, IoCContainer.User.Identity as IUser);
		}

		protected override Bagge.Seti.WebSite.Presenters.EditorPresenter<Employee, int> Presenter
		{
			get { return _presenter; }
		}

		protected override CompositeDataBoundControl Details
		{
			get { return _details; }
		}

		protected override Microsoft.Practices.Web.UI.WebControls.ObjectContainerDataSource ObjectDataSource
		{
			get { return _dataSource; }
		}

		protected override bool ShowRequiredInformationLabel
		{
			get { return true; }
		}

		public override EditorAction Mode
		{
			get
			{
				return EditorAction.Update;
			}
		}

		public override int PrimaryKey
		{
			get
			{
				return ((IUser)IoCContainer.User.Identity).Id;
			}
		}

		

		public string Password
		{
			get
			{
				var password = Details.FindControl("_password") as TextBox;
				if (password != null)
					return password.Text;
				return null;
			}
		}

		public string ConfirmPassword
		{
			get
			{
				var confirmPassword = Details.FindControl("_confirmPassword") as TextBox;
				if (confirmPassword != null)
					return confirmPassword.Text;
				return null;
			}
		}
	}
}
