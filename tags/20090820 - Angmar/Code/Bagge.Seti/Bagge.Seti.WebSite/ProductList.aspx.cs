using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.WebSite.Presenters;
using Bagge.Seti.Common;
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.WebSite
{
	[SecurizableCrud("Securizable_ProductList", typeof(ProductList), FunctionAction.Retrieve | FunctionAction.Delete)]
	public partial class ProductList : FilteredListPage<Product, int>, IProductListView
	{
		ProductListPresenter _presenter;

		public ProductList()
		{
			_presenter = new ProductListPresenter(this, IoCContainer.ProductManager, IoCContainer.ProviderManager);
		}

		public override IList<FilterPropertyValue> Filters
		{
			get { return GetFiltersFromControls(); }
		}

		private IList<FilterPropertyValue> GetFiltersFromControls()
		{
			List<FilterPropertyValue> filters = new List<FilterPropertyValue>();
			filters.Add(new FilterPropertyValue { Property = "Name", Value = _name.Text, Type = FilterPropertyValueType.Like });
			filters.Add(new FilterPropertyValue { Property = "Description", Value = _description.Text, Type = FilterPropertyValueType.Like });

			AddDeletedFilterValue(filters);

			if (!_providers.SelectedValue.IsNullOrEmpty())
				filters.Add(new FilterPropertyValue { Property = "Providers", Value = _providers.SelectedValue.ToInt32(), Type = FilterPropertyValueType.In });


			return filters;
		}

		protected override Button FilterButton
		{
			get { return _filter; }
		}

		protected override GridView Grid
		{
			get { return _products; }
		}

		protected override Bagge.Seti.WebSite.Presenters.ListPresenter<Product, int> Presenter
		{
			get { return _presenter; }
		}

		protected override Microsoft.Practices.Web.UI.WebControls.ObjectContainerDataSource ObjectDataSource
		{
			get { return _dataSource; }
		}

		#region IProductListView Members

		public Provider[] Providers
		{
			set
			{
				_providers.DataSource = value;
				_providers.DataBind();
			}
		}

		#endregion

		protected override DropDownList DeletedDropDownList
		{
			get { return _isDeleted; }
		}
	}
}
