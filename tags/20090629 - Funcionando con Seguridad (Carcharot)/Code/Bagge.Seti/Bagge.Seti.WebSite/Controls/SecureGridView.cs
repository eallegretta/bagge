using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Bagge.Seti.Security.BusinessEntities;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Reflection;
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.Common;

namespace Bagge.Seti.WebSite.Controls
{


	public class SecureGridView : GridView, ISecureControlContainer
	{
		public SecureGridView()
		{

		}

		private class PagerLinkButton : LinkButton
		{
			public PagerLinkButton(IPostBackContainer container)
			{
				_container = container;
			}

			public void EnableCallback(string argument)
			{
				_enableCallback = true;
				_callbackArgument = argument;
			}

			public override bool CausesValidation
			{
				get { return false; }
				set { throw new ApplicationException("Cannot set validation on pager buttons"); }
			}

			protected override void Render(HtmlTextWriter writer)
			{
				SetCallbackProperties();
				base.Render(writer);
			}

			private void SetCallbackProperties()
			{
				if (_enableCallback)
				{
					ICallbackContainer container = _container as ICallbackContainer;
					if (container != null)
					{
						string callbackScript = container.GetCallbackScript(this, _callbackArgument);
						if (!string.IsNullOrEmpty(callbackScript)) OnClientClick = callbackScript;
					}
				}
			}

			#region Private fields

			private readonly IPostBackContainer _container;
			private bool _enableCallback;
			private string _callbackArgument;

			#endregion
		}

		protected override void InitializePager(GridViewRow row, int columnSpan, PagedDataSource pagedDataSource)
		{
			base.InitializePager(row, columnSpan, pagedDataSource);

			Table table = (Table)row.Cells[0].Controls[0];

			TableCell firstCell = new TableCell();
			table.Rows[0].Cells.AddAt(0, firstCell);
			firstCell.CssClass = "firstCell";
			firstCell.Text = Resources.WebSite.Controls_Pager_PageClause;

			TableCell lastCell = new TableCell();
			lastCell.CssClass = "lastCell";

			int startRecord = pagedDataSource.CurrentPageIndex * pagedDataSource.PageSize + 1;
			int endRecord = (startRecord + pagedDataSource.PageSize - 1 < pagedDataSource.DataSourceCount) ? startRecord + pagedDataSource.PageSize - 1 : pagedDataSource.DataSourceCount;

			lastCell.Text = string.Format(Resources.WebSite.Controls_Pager_ItemsCount, startRecord, endRecord, pagedDataSource.DataSourceCount);

			table.Rows[0].Cells.Add(lastCell);

		}

		protected virtual void CreateCustomPager(GridViewRow row, int columnSpan, PagedDataSource pagedDataSource)
		{
			int pageCount = pagedDataSource.PageCount;
			int pageIndex = pagedDataSource.CurrentPageIndex + 1;
			int pageButtonCount = PagerSettings.PageButtonCount;

			TableCell cell = new TableCell();
			row.Cells.Add(cell);
			if (columnSpan > 1) cell.ColumnSpan = columnSpan;

			if (pageCount > 1)
			{
				HtmlGenericControl pager = new HtmlGenericControl("div");
				pager.Attributes["class"] = "pagination";
				cell.Controls.Add(pager);

				int min = pageIndex - pageButtonCount;
				int max = pageIndex + pageButtonCount;

				if (max > pageCount)
					min -= max - pageCount;
				else if (min < 1)
					max += 1 - min;

				// Create "previous" button
				Control page = pageIndex > 1
								? BuildLinkButton(pageIndex - 2, PagerSettings.PreviousPageText, "Page", "Prev")
								: BuildSpan(PagerSettings.PreviousPageText, "disabled");
				pager.Controls.Add(page);

				// Create page buttons
				bool needDiv = false;
				for (int i = 1; i <= pageCount; i++)
				{
					if (i <= 2 || i > pageCount - 2 || (min <= i && i <= max))
					{
						string text = i.ToString(NumberFormatInfo.InvariantInfo);
						page = i == pageIndex
								? BuildSpan(text, "current")
								: BuildLinkButton(i - 1, text, "Page", text);
						pager.Controls.Add(page);
						needDiv = true;
					}
					else if (needDiv)
					{
						page = BuildSpan("&hellip;", null);
						pager.Controls.Add(page);
						needDiv = false;
					}
				}

				// Create "next" button
				page = pageIndex < pageCount
						? BuildLinkButton(pageIndex, PagerSettings.NextPageText, "Page", "Next")
						: BuildSpan(PagerSettings.NextPageText, "disabled");
				pager.Controls.Add(page);
			}
		}

		private string ParentBuildCallbackArgument(int pageIndex)
		{
			MethodInfo m =
				typeof(GridView).GetMethod("BuildCallbackArgument", BindingFlags.NonPublic | BindingFlags.Instance, null,
											new Type[] { typeof(int) }, null);
			return (string)m.Invoke(this, new object[] { pageIndex });
		}

		private Control BuildLinkButton(int pageIndex, string text, string commandName, string commandArgument)
		{
			PagerLinkButton link = new PagerLinkButton(this);
			link.Text = text;
			link.EnableCallback(ParentBuildCallbackArgument(pageIndex));
			link.CommandName = commandName;
			link.CommandArgument = commandArgument;
			return link;
		}

		private Control BuildSpan(string text, string cssClass)
		{
			HtmlGenericControl span = new HtmlGenericControl("span");
			if (!String.IsNullOrEmpty(cssClass)) span.Attributes["class"] = cssClass;
			span.InnerHtml = text;
			return span;
		}




		#region ISecureControl Members

		public string SecureTypeName
		{
			get;
			set;
		}

		public void ApplySecurityRestrictions(IList<Function> functions)
		{
			IUser user = Page.User.Identity as IUser;
			foreach (DataControlField field in Columns)
			{
				if (field is IPropertySecureControl)
				{
					var secureField = (IPropertySecureControl)field;

					if (user == null)
					{
						secureField.Visible = false;
						continue;
					}
					
					switch (IoCContainer.AccessibilityTypeManager.GetUserAccessibilityForProperty(
						user, Type.GetType(SecureTypeName), secureField.PropertyName))
					{
						case AccessibilityTypes.View:
							secureField.ReadOnly = false;
							secureField.Visible = true;
							break;
						case AccessibilityTypes.Edit:
							secureField.ReadOnly = false;
							secureField.Visible = true;
							break;
						case AccessibilityTypes.None:
							secureField.Visible = false;
							break;
					}
				}
				if (field is IMethodSecureControl)
				{
					var secureField = (IMethodSecureControl)field;

					if (user == null)
					{
						secureField.Visible = false;
						continue;
					}

					switch (IoCContainer.AccessibilityTypeManager.GetUserAccessibilityForMethod(
						user, Type.GetType(SecureTypeName), secureField.MethodName))
					{
						case AccessibilityTypes.None:
							secureField.Visible = false;
							break;
						case AccessibilityTypes.Execute:
							secureField.Visible = true;
							break;
					}
				}
			}
		}

		#endregion
	}
}
