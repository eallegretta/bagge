using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bagge.Seti.Common;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.BusinessEntities.Exceptions;
using System.Web.Services;
using System.Web.Script.Services;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Bagge.Seti.WebSite.Controls
{
	public enum ProductProviderSelectionGridSourceType
	{
		Product,
		Provider
	}

	public partial class ProductProviderSelectionGrid : System.Web.UI.UserControl
	{
		public ProductProviderSelectionGridSourceType SourceType
		{
			get;
			set;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				_legendProvider.Visible = _legendProduct.Visible = false;
				if (SourceType == ProductProviderSelectionGridSourceType.Product)
				{
					LoadProducts();
					_legendProduct.Visible = true;
				}
				else
				{
					LoadProviders();
					_legendProvider.Visible = true;
				}

				SelectedItems = new List<ProductProvider>();

				if (ReadOnly)
					_addControls.Visible = false;
			}
			SetAddButtonClickBehaviour();
		}

		private void SetAddButtonClickBehaviour()
		{
			_add.OnClientClick = string.Format(
				"addSelectedItem('{0}', '{1}', '{2}', '{3}', '{4}')",
				_items.ClientID, _selectedItems.ClientID, 
				_name.ClientID, _price.ClientID,
				SourceType.ToString()
			);
		}

		protected void _items_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			SelectedItems.RemoveAt(e.RowIndex);
			//BindItems();
		}

		private void LoadProviders()
		{
			var providers = IoCContainer.ProviderManager.FindAllActiveOrdered("Name");
			foreach (var provider in providers)
			{
				ListItem item = new ListItem();
				item.Value = provider.Id.ToString();
				item.Text = provider.NameAndCUIT;
				_name.Items.Add(item);
			}
		}

		private void LoadProducts()
		{
			var products = IoCContainer.ProductManager.FindAllActiveOrdered("Name");
			foreach (var product in products)
			{
				ListItem item = new ListItem();
				item.Value = product.Id.ToString();
				item.Text = product.NameAndDescription;
				_name.Items.Add(item);
			}
		}

		//protected void _items_DataBound(object sender, EventArgs e)
		//{
		//    if (SourceType == ProductProviderSelectionGridSourceType.Product)
		//        _items.Columns[0].Visible = false;
		//    else
		//        _items.Columns[1].Visible = false;
		//    if (ReadOnly)
		//        _items.Columns[3].Visible = false;
		//}


		public bool ReadOnly
		{
			get;
			set;
		}


		[Bindable(true, BindingDirection.TwoWay)]
		public IList<ProductProvider> SelectedItems
		{
			get 
			{
				return JavaScriptConvert.DeserializeObject<List<ProductProvider>>(_selectedItems.Value);
			}
			set
			{
				_selectedItems.Value = JavaScriptConvert.SerializeObject(value);
			}
		}

		

		protected void _add_Click(object sender, EventArgs e)
		{
			if (SourceType == ProductProviderSelectionGridSourceType.Product)
			{
				AddSelectedProduct();
			}
			else
			{
				AddSelectedProvider();
			}

		}

		private void AddSelectedProvider()
		{
			Provider provider = new Provider();
			provider.Id = _name.SelectedValue.ToInt32();
			provider.Name = _name.SelectedItem.Text;
			decimal price = 0;
			try
			{
				price = decimal.Parse(_price.Text);
			}
			catch (FormatException)
			{
				return;
			}
			if (SelectedItems.Where(p => p.Provider.Equals(provider)).Select(p => p).Count() == 0)
			{
				SelectedItems.Add(new ProductProvider { Provider = provider, Price = price });
			}
			//BindItems();
		}

		//private void BindItems()
		//{
		//    _items.DataSource = SelectedItems;
		//    _items.DataBind();
		//}



		private void AddSelectedProduct()
		{
			Product product = new Product();
			product.Id = _name.SelectedValue.ToInt32();
			product.Name = _name.SelectedItem.Text;
			decimal price = 0;
			try
			{
				price = decimal.Parse(_price.Text);
			}
			catch (FormatException)
			{
				return;
			}
			if (SelectedItems.Where(p => p.Product.Equals(product)).Select(p => p).Count() == 0)
			{
				SelectedItems.Add(new ProductProvider { Product = product, Price = price });
			}
			//BindItems();
		}
	}
}