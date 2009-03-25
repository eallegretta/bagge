using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.WebSite.Presenters;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.Common;

namespace Bagge.Seti.WebSite
{
	public partial class ProviderList : FilteredListPage<Provider, int>, IProviderListView
	{
		ProviderListPresenter _presenter;

		public ProviderList()
		{
			_presenter = new ProviderListPresenter(this, IoCContainer.ProviderManager, IoCContainer.ProductManager);
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
			filters.Add(new FilterPropertyValue { Property = "Name", Value = _name.Text, Type = FilterPropertyValueType.Like });
			filters.Add(new FilterPropertyValue { Property = "CUIT", Value = _cuit.Text, Type = FilterPropertyValueType.Like });
			filters.Add(new FilterPropertyValue { Property = "Address", Value = _address.Text, Type = FilterPropertyValueType.Like });
			filters.Add(new FilterPropertyValue { Property = "Phone", Value = _phone.Text, Type = FilterPropertyValueType.Like });
			if(!_products.SelectedValue.IsNullOrEmpty())
				filters.Add(new FilterPropertyValue { Property = "Products", Value = _products.SelectedValue, Type = FilterPropertyValueType.In });
			

			return filters;
		}

		protected override Button FilterButton
		{
			get { return _filter; }
		}

		protected override GridView Grid
		{
			get { return _providers; }
		}

		protected override Bagge.Seti.WebSite.Presenters.ListPresenter<Provider, int> Presenter
		{
			get { return _presenter; }
		}

		protected override Microsoft.Practices.Web.UI.WebControls.ObjectContainerDataSource ObjectDataSource
		{
			get { return _dataSource; }
		}

		#region IProviderListView Members

		public Product[] Products
		{
			set
			{
				_products.DataSource = value;
				_products.DataBind();
			}
		}

		#endregion
	}
}
