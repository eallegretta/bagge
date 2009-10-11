using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Bagge.Seti.Helpers
{
	public static class ControlHelper
	{
		public static string GetControlAsHtml(Control control)
		{
			if (control is GridView)
				return GetGridViewAsHtml(control as GridView);

			using (StringWriter sw = new StringWriter())
			{
				using (HtmlTextWriter tw = new HtmlTextWriter(sw))
				{
					control.RenderControl(tw);
					return sw.ToString();
				}
			}
		}

		public static string GetGridViewAsHtml(GridView gv)
		{

			using (StringWriter sw = new StringWriter())
			{
				using (HtmlTextWriter htw = new HtmlTextWriter(sw))
				{
					//  Create a table to contain the grid
					Table table = new Table();

					//  include the gridline settings
					table.GridLines = gv.GridLines;

					//  add the header row to the table
					if (gv.HeaderRow != null)
					{
						PrepareControlForExport(gv.HeaderRow);
						table.Rows.Add(gv.HeaderRow);
					}

					//  add each of the data rows to the table
					foreach (GridViewRow row in gv.Rows)
					{
						PrepareControlForExport(row);
						table.Rows.Add(row);
					}

					//  add the footer row to the table
					if (gv.FooterRow != null)
					{
						PrepareControlForExport(gv.FooterRow);
						table.Rows.Add(gv.FooterRow);
					}

					//  render the table into the htmlwriter
					table.RenderControl(htw);

					return sw.ToString();
				}
			}
		}

		/// <summary>
		/// Replace any of the contained controls with literals
		/// </summary>
		/// <param name="control"></param>
		private static void PrepareControlForExport(Control control)
		{
			for (int i = 0; i < control.Controls.Count; i++)
			{
				Control current = control.Controls[i];
				if (current is LinkButton)
				{
					control.Controls.Remove(current);
					control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
				}
				else if (current is ImageButton)
				{
					control.Controls.Remove(current);
					control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
				}
				else if (current is HyperLink)
				{
					control.Controls.Remove(current);
					control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
				}
				else if (current is DropDownList)
				{
					control.Controls.Remove(current);
					control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
				}
				else if (current is CheckBox)
				{
					control.Controls.Remove(current);
					control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
				}

				if (current.HasControls())
				{
					PrepareControlForExport(current);
				}
			}
		}

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