using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

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
	}
}
