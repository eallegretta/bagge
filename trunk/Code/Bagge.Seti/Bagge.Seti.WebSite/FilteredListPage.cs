﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.WebSite
{
	public abstract class FilteredListPage<T, PK>: ListPage<T, PK>, IFilteredListView where T: PrimaryKeyDomainObject<T, PK>
	{

		protected virtual void AddDeletedFilterValue(IList<FilterPropertyValue> filters)
		{
			if (!string.IsNullOrEmpty(DeletedDropDownList.SelectedValue))
				filters.Add(new FilterPropertyValue { Property = "Deleted", Value = DeletedDropDownList.SelectedValue.ToBoolean(), Type = FilterPropertyValueType.Equals });
		}

		protected virtual void AddDeletedFilterItems(DropDownList deleted)
		{
			deleted.Items.Add(new ListItem(Resources.WebSite.YesText, "true"));
			deleted.Items.Add(new ListItem(Resources.WebSite.NoText, "false"));
		}


		protected override void OnLoad(EventArgs e)
		{
			if (!IsPostBack)
			{
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
