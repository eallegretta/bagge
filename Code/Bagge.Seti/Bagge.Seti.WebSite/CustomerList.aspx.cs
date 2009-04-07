using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.WebSite.Presenters;
using Bagge.Seti.BusinessLogic;
using Spring.Context.Support;
using Bagge.Seti.Common;
using Microsoft.Practices.Web.UI.WebControls;

namespace Bagge.Seti.WebSite
{
	public partial class CustomerList : FilteredListPage<Customer, int>
	{
		ListPresenter<Customer, int> _presenter;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				AddDeletedFilterItems();
			}
		}

		private void AddDeletedFilterItems()
		{
			_isDeleted.Items.Add(new ListItem(Resources.WebSite.YesText, "true"));
			_isDeleted.Items.Add(new ListItem(Resources.WebSite.NoText, "false"));
		}

		public CustomerList()
		{
			_presenter = new ListPresenter<Customer, int>(this, IoCContainer.CustomerManager);
		}

		protected override ObjectContainerDataSource ObjectDataSource
		{
			get { return _dataSource; }
		}

		protected override ListPresenter<Customer, int> Presenter
		{
			get { return _presenter; }
		}

		protected override GridView Grid
		{
			get { return _customers; }
		}

		protected override Button FilterButton
		{
			get { return _filter; }
		}

		public override IList<FilterPropertyValue> Filters
		{
			get
			{
				return GetFiltersFromControls();
			}
		}

		private IList<FilterPropertyValue> GetFiltersFromControls()
		{
			List<FilterPropertyValue> filters = new List<FilterPropertyValue>();
			if(!string.IsNullOrEmpty(_name.Text))
				filters.Add(new FilterPropertyValue { Property = "Name", Value = _name.Text, Type = FilterPropertyValueType.Like });
			if(!string.IsNullOrEmpty(_cuit.Text))
				filters.Add(new FilterPropertyValue { Property = "CUIT", Value = _cuit.Text, Type = FilterPropertyValueType.Like });
			if(!string.IsNullOrEmpty(_address.Text))
				filters.Add(new FilterPropertyValue { Property = "Address", Value = _address.Text, Type = FilterPropertyValueType.Like });
			if(!string.IsNullOrEmpty(_phone.Text))
				filters.Add(new FilterPropertyValue { Property = "Phone", Value = _phone.Text, Type = FilterPropertyValueType.Like });
			if (!string.IsNullOrEmpty(_isDeleted.SelectedValue))
				filters.Add(new FilterPropertyValue { Property = "Deleted", Value = _isDeleted.SelectedValue.ToBoolean(), Type = FilterPropertyValueType.Equals });
			
			return filters;
		}
	}
}
