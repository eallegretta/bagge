using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.Security.BusinessEntities;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.Common;
using Bagge.Seti.BusinessEntities.Exceptions;

namespace Bagge.Seti.WebSite.Security
{
	public static class AuthorizationManager
	{
		public static bool UserHasAccess(IHttpHandler handler)
		{
			var page = handler;
			var type = page.GetType().BaseType;
			var user = HttpContext.Current.User.Identity as IUser;

			if (user == null)
				return false;

			if (type.IsDefined(typeof(SecurizableCrudAttribute), true))
			{
				var attr = type.GetCustomAttributes(typeof(SecurizableCrudAttribute), true);


				FunctionAction action = FunctionAction.NotSet;
				if (page is IListView)
				{
					var view = page as IListView;
					if (view.IsDelete)
						action = FunctionAction.Delete;
					else
						action = FunctionAction.Retrieve;
				}
				else if (page is IEditorView)
				{
					var view = page as IEditorView;
					if (view.Mode == EditorAction.Insert)
						action = FunctionAction.Create;
					else if (view.Mode == EditorAction.Update)
						action = FunctionAction.Update;
					else
						action = FunctionAction.Retrieve;
				}
				else if (page is IReportView)
				{
					action = FunctionAction.Retrieve;
				}

				if (action != FunctionAction.NotSet)
				{
					var function = IoCContainer.FunctionManager.Get(type, action);

					IoCContainer.Storage.SetData(typeof(Function), function);

					return IoCContainer.FunctionManager.UserHasAccessToFunction(user, function);
				}

				return true;
			}
			return true;
		}
	}
}
