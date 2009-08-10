using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.BusinessEntities.Security;

namespace Bagge.Seti.WebSite.Presenters
{
	public class EditProfilePresenter : EditorPresenter<Employee, int>
	{
		IUser _loggedUser;

		public EditProfilePresenter(IEditProfileView view,
			IEmployeeManager employeeManager,
			IUser loggedUser)
			: base(view, employeeManager)
		{
			_loggedUser = loggedUser;
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
