using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.Common;
using Bagge.Seti.WebSite.Security;
using Bagge.Seti.BusinessEntities.Exceptions;

namespace Bagge.Seti.WebSite.Presenters
{
	public class EditProfilePresenter : EditorPresenter<Employee, int>
	{
		IUser _loggedUser;
		IAuthenticator _authenticationProvider;

		public EditProfilePresenter(IEditProfileView view,
			IEmployeeManager employeeManager,
			IUser loggedUser,
			IAuthenticator authenticationProvider)
			: base(view, employeeManager)
		{
			_loggedUser = loggedUser;
			_authenticationProvider = authenticationProvider;
		}

		protected override void OnInit(object sender, EventArgs e)
		{
			base.OnInit(sender, e);

			if (AuthenticationManager.IsWindowsAuthentication(_authenticationProvider))
				throw new AccessDeniedException();

		}

		public override object GetSelectedEntity()
		{
			return GetManager<IEmployeeManager>().Get(_loggedUser.Id);
		}

		public override void Select()
		{
			SelectedEntity = GetManager<IEmployeeManager>().Get(_loggedUser.Id);
			var view = GetView<IEditProfileView>();
			view.DataSource = SelectedEntity.ToSingleItemArray();
			view.Timestamp = SelectedEntity.AuditTimeStamp;
		}

		public override void Save(Employee entity)
		{
			entity.Password = GetView<IEditProfileView>().Password;
			entity.Username = _loggedUser.Name;
			GetManager<IEmployeeManager>().UpdateProfile(entity);
		}
	}
}
