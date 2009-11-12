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
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using Bagge.Seti.BusinessLogic.Contracts;
using System.Globalization;


namespace Bagge.Seti.WebSite.Controls
{

	public enum ProductProviderSelectionGridSourceType
	{
		Product,
		Provider
	}

	public partial class ProductProviderSelectionGrid : System.Web.UI.UserControl
	{
		private class ProductProviderBindItem
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public decimal? Price { get; set; }

			public ProductProviderBindItem()
			{

			}

			public ProductProviderBindItem(ProductProvider productProvider,
				ProductProviderSelectionGridSourceType sourceType)
			{
				if (sourceType == ProductProviderSelectionGridSourceType.Product)
				{
					Id = productProvider.Product.Id;
					Name = productProvider.Product.NameAndDescription;
				}
				else
				{
					Id = productProvider.Provider.Id;
					Name = productProvider.Provider.NameAndCUIT;
				}
				Price = productProvider.Price;
			}

			public static IList<ProductProviderBindItem> ToProductProviderBindItems(IList<ProductProvider> productProviders,
				ProductProviderSelectionGridSourceType sourceType)
			{
				List<ProductProviderBindItem> items = new List<ProductProviderBindItem>();
				foreach (var productProvider in productProviders)
					items.Add(new ProductProviderBindItem(productProvider, sourceType));
				return items;
			}

			public static IList<ProductProvider> ToProductProviderItems(IList<ProductProviderBindItem> productProviderBindItems,
				ProductProviderSelectionGridSourceType sourceType)
			{
				List<ProductProvider> items = new List<ProductProvider>();
				foreach (var productProviderBindItem in productProviderBindItems)
				{
					if (sourceType == ProductProviderSelectionGridSourceType.Product)
						items.Add(new ProductProvider
									{
										Price = productProviderBindItem.Price,
										Product = new Product
													{
														Id = productProviderBindItem.Id,
														Name = productProviderBindItem.Name
													}
									});
					else
						items.Add(new ProductProvider
									{
										Price = productProviderBindItem.Price,
										Provider = new Provider
													{
														Id = productProviderBindItem.Id,
														Name = productProviderBindItem.Name
													}
									});
				}
				return items;
			}
		}

		public ProductProviderSelectionGridSourceType SourceType
		{
			get;
			set;
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			if (IsPostBack)
			{
				string value = Request.Form[_selectedItems.UniqueID];
				if (!string.IsNullOrEmpty(value))
					_selectedItems.Value = value;
			}
		}


		protected void Page_Load(object sender, EventArgs e)
		{
			/*if (!IsPostBack)
			{*/
			_legendProvider.Visible = _legendProduct.Visible = false;
			_productField.Visible = _providerField.Visible = false;
			if (SourceType == ProductProviderSelectionGridSourceType.Product)
			{
				LoadProducts();
				_productField.Visible = _legendProduct.Visible = true;
			}
			else
			{
				LoadProviders();
				_providerField.Visible = _legendProvider.Visible = true;
			}

			if (ReadOnly)
			{
				_addControls.Visible = false;
				_items.Rows[0].Cells.RemoveAt(2);
			}
			/*}
			else*/
			if (IsPostBack)
				DataBind();

			EnsureRegisterJavascripts();

		}

		private bool _isJavascriptRendered = false;

		private void EnsureRegisterJavascripts()
		{
			if (!_isJavascriptRendered)
				RegisterJavascripts();
		}

		private void RegisterJavascripts()
		{
			if (!ScriptManager.GetCurrent(Page).IsInAsyncPostBack)
			{
				if (!Page.ClientScript.IsClientScriptIncludeRegistered("ProductProviderSelectionGrid"))
				{
					Page.ClientScript.RegisterClientScriptInclude("ProductProviderSelectionGrid", ResolveUrl("ProductProviderSelectionGrid.js"));
					Page.ClientScript.RegisterStartupScript(typeof(string), "ProductProviderSelectionGridObject",
						string.Format("var {0}_instance = new ProductProviderSelectionGrid('{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7}, '{8}', '{9}');",
							ClientID, _items.ClientID, _add.ClientID, _selectedItems.ClientID, _name.ClientID, _price.ClientID,
							GetDeleteImagePath(), ReadOnly.ToString().ToLower(), CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator,
							CultureInfo.CurrentUICulture.NumberFormat.CurrencySymbol),
							true);
				}
			}
			else
			{
				string script = string.Format("var {0}_instance = new ProductProviderSelectionGrid('{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7}, '{8}', '{9}');",
							ClientID, _items.ClientID, _add.ClientID, _selectedItems.ClientID, _name.ClientID, _price.ClientID,
							GetDeleteImagePath(), ReadOnly.ToString().ToLower(), CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator,
							CultureInfo.CurrentUICulture.NumberFormat.CurrencySymbol);
				ScriptManager.RegisterStartupScript(this,
					typeof(string),
					"ProductProviderSelectionGridObject",
					script,
					true);
			}
			_isJavascriptRendered = true;
		}

		private string GetDeleteImagePath()
		{
			return ResolveUrl(string.Format("~/App_Themes/{0}/images/iconDelete.gif", Page.Theme));
		}

		protected void _items_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			SelectedItems.RemoveAt(e.RowIndex);
			//BindItems();
		}

		private void LoadProviders()
		{
			var selectedItems = SelectedItems;
			var selectedItemsCount = selectedItems.Count;
			ListItem firstItem = _name.Items[0];
			_name.Items.Clear();
			_name.Items.Add(firstItem);
			var providers = IoCContainer.ProviderManager.FindAllActiveOrdered("Name");
			foreach (var provider in providers)
			{
				if (selectedItemsCount > 0 && selectedItems.FirstOrDefault(pp => pp.Provider.Id == provider.Id) != null)
					continue;

				ListItem item = new ListItem();
				item.Value = provider.Id.ToString();
				item.Text = provider.NameAndCUIT;
				_name.Items.Add(item);
			}
		}

		private void LoadProducts()
		{
			var selectedItems = SelectedItems;
			var selectedItemsCount = selectedItems.Count;
			ListItem firstItem = _name.Items[0];
			_name.Items.Clear();
			_name.Items.Add(firstItem);
			var products = IoCContainer.ProductManager.FindAllActiveOrdered("Name");
			foreach (var product in products)
			{
				if (selectedItemsCount > 0 && selectedItems.FirstOrDefault(pp => pp.Product.Id == product.Id) != null)
					continue;

				ListItem item = new ListItem();
				item.Value = product.Id.ToString();
				item.Text = product.NameAndDescription;
				_name.Items.Add(item);
			}
		}

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
				return ProductProviderBindItem.ToProductProviderItems(
					JsonConvert.DeserializeObject<List<ProductProviderBindItem>>(_selectedItems.Value),
						SourceType);
			}
			set
			{
				_selectedItems.Value = JsonConvert.SerializeObject(
						ProductProviderBindItem.ToProductProviderBindItems(value,
						SourceType));
				RemoveDropDownItems(value);
				DataBind();
			}
		}

		private void RemoveDropDownItems(IList<ProductProvider> productProviders)
		{
			foreach (var productProvider in productProviders)
			{
				ListItem item;
				if (SourceType == ProductProviderSelectionGridSourceType.Product)
					item = _name.Items.FindByValue(productProvider.Product.Id.ToString());
				else
					item = _name.Items.FindByValue(productProvider.Provider.Id.ToString());
				if (item != null)
					_name.Items.Remove(item);
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

		public override void DataBind()
		{
			if (SelectedItems.Count > 0)
			{
				EnsureRegisterJavascripts();
				string script = string.Format("{0}_instance.refresh();", ClientID);
				if (ScriptManager.GetCurrent(Page).IsInAsyncPostBack)
				{
					ScriptManager.RegisterStartupScript(this, typeof(string), Guid.NewGuid().ToString(), script, true);
				}
				else
					Page.ClientScript.RegisterStartupScript(typeof(string), Guid.NewGuid().ToString(), script, true);
			}
		}
	}
}