﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Bagge.Seti.WebSite.Controls;
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.Helpers;
using Bagge.Seti.DesignByContract;

namespace Bagge.Seti.WebSite
{
	public class Page: System.Web.UI.Page
	{
		protected T GetControlPropertyValue<T>(Control control, T defaultValue, params string[] propertiesToSearch) where T: IConvertible
		{
			Check.Require(control != null);

			foreach (string propertyName in propertiesToSearch)
			{
				object value = control.GetPropertyValue(propertyName);
				if (value != null && !string.IsNullOrEmpty(value.ToString()))
				{
					try
					{
						return (T)Convert.ChangeType(value, typeof(T));
					}
					catch
					{
						return defaultValue;
					}
				}
			}
			return defaultValue;
		}

		protected void SetControlPropertyValue(Control control, object value, params string[] propertiesToSearch)
		{
			Check.Require(control != null);
			Check.Require(value != null);

			foreach (string propertyName in propertiesToSearch)
			{
				if (control.SetPropertyValue(propertyName, value))
					return;
			}
		}


		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			ApplySecurityRestrictions(Page.Controls);
		}

		protected void AssignTypeNameToSecureContainers(string typeName)
		{
			AssignTypeNameToSecureContainers(typeName, Page);
		}

		private void AssignTypeNameToSecureContainers(string typeName, Control root)
		{
			foreach (Control ctrl in root.Controls)
			{
				if (ctrl is ISecureControlContainer)
					((ISecureControlContainer)ctrl).SecureTypeName = typeName;

				AssignTypeNameToSecureContainers(typeName, ctrl);
			}
		}

		private void ApplySecurityRestrictions(System.Web.UI.ControlCollection controlCollection)
		{
			foreach (Control ctrl in controlCollection)
			{
				if (ctrl is ISecureControlContainer)
					((ISecureControlContainer)ctrl).ApplySecurityRestrictions(((IUser)Page.User.Identity).Functions);
				ApplySecurityRestrictions(ctrl.Controls);
			}
		}

	}
}
