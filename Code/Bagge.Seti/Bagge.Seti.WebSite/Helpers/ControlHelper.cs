using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bagge.Seti.Helpers
{
	public static class ControlHelper
	{
		public static Control FindControlRecursive(Control root, string id)
		{
			if (root.ID == id)
				return root;

			foreach (Control ctrl in root.Controls)
			{
				Control foundControl = FindControlRecursive(ctrl, id);
				if (foundControl != null)
					return foundControl;
			}
			return null;
		}

		public static void SetReadOnlyControlHierarchy(ControlCollection controls)
		{
			EnableDisableControlHierarchy(true, false, controls);
		}

		public static void SetNoReadOnlyControlHierarchy(ControlCollection controls)
		{
			EnableDisableControlHierarchy(true, true, controls);
		}

		public static void EnableControlHierarchy(ControlCollection controls)
		{
			EnableDisableControlHierarchy(false, true, controls);
		}

		public static void DisableControlHierarchy(ControlCollection controls)
		{
			EnableDisableControlHierarchy(false, false, controls);
		}

		private static void EnableDisableControlHierarchy(bool useReadOnlyFirst, bool enabled, ControlCollection controls)
		{
			foreach (Control ctrl in controls)
			{
				if (useReadOnlyFirst && ctrl.HasProperty("ReadOnly"))
					ctrl.SetPropertyValue("ReadOnly", enabled);
				else if (ctrl.HasProperty("Enabled"))
					ctrl.SetPropertyValue("Enabled", enabled);

				EnableDisableControlHierarchy(useReadOnlyFirst, enabled, ctrl.Controls);
			}
		}


	}
}
