using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.WebSite.Helpers;

namespace Bagge.Seti.WebSite
{
	public abstract class FilteredListPage<T, PK>: ListPage<T, PK>, IFilteredListView where T: PrimaryKeyDomainObject<T, PK>
	{
		protected virtual void AddCalendarFilterValue(Controls.Calendar calendar, string property,
			FilterPropertyValueType type, IList<FilterPropertyValue> filters)
		{
			FilterHelper.AddCalendarFilterValue(calendar, property, type, filters);
		}

		protected virtual void AddTextBoxFilterValue<TValue>(TextBox control, string property, 
			FilterPropertyValueType type, IList<FilterPropertyValue> filters) where TValue: IConvertible
		{
			FilterHelper.AddTextBoxFilterValue<TValue>(control, property, type, filters);
		}

		protected virtual void AddDropDownFilterValue<TValue>(DropDownList control, string property,
			FilterPropertyValueType type, IList<FilterPropertyValue> filters) where TValue : IConvertible
		{
			FilterHelper.AddDropDownFilterValue<TValue>(control, property, type, filters);
		}

		protected virtual void AddCheckBoxListFilterValue<TValue>(CheckBoxList control, string property,
			FilterPropertyValueType type, IList<FilterPropertyValue> filters) where TValue : IConvertible
		{
			FilterHelper.AddCheckBoxListFilterValue<TValue>(control, property, type, filters);
		}

		protected virtual void AddDeletedFilterValue(IList<FilterPropertyValue> filters)
		{
			AddDropDownFilterValue<bool>(DeletedDropDownList, "Deleted", FilterPropertyValueType.Equals, filters);
		}

		protected virtual void AddDeletedFilterItems(DropDownList deleted)
		{
			deleted.Items.Add(new ListItem(Resources.WebSite.YesText, "true"));
			deleted.Items.Add(new ListItem(Resources.WebSite.NoText, "false"));
			// Select No by default
			deleted.Items.FindByValue("false").Selected = true;
		}


		protected override void OnLoad(EventArgs e)
		{
			if (!IsPostBack)
			{
				if(DeletedDropDownList != null)
					AddDeletedFilterItems(DeletedDropDownList);
			}

			base.OnLoad(e);
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			FilterButton.Click += new EventHandler(FilterButton_Click);
		}

		void FilterButton_Click(object sender, EventArgs e)
		{
			OnFilter();
		}


		protected override void OnSelecting(int pageIndex, int pageSize, string orderBy)
		{
			Presenter.Select(pageIndex, pageSize, orderBy, Filters);
		}

		protected virtual void OnFilter()
		{
			Presenter.Select(0, Grid.PageSize, DefaultSortExpression, Filters);
		}

		#region IFilteredListView Members

		public abstract IList<Bagge.Seti.BusinessEntities.FilterPropertyValue> Filters { get; }
		

		#endregion

		protected abstract Button FilterButton
		{
			get; 
		}

		protected abstract DropDownList DeletedDropDownList
		{
			get;
		}

	}
}
