using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bagge.Seti.BusinessEntities;
using System.ComponentModel;
using Bagge.Seti.Common;
using Newtonsoft.Json;

namespace Bagge.Seti.WebSite.Controls
{
	public partial class ProductTicketSelectionGrid : System.Web.UI.UserControl
	{
		private class ProductTicketBindItem
		{
			public int Id { get; set; }
			public int ProductId { get; set; }
			public int ProviderId { get; set; }
			public string Product { get; set; }
			public string Provider { get; set; }
			public decimal? Quantity { get; set; }
			public decimal? UnitaryPrice { get; set; }
			public bool Deleted { get; set; }

			public ProductTicketBindItem()
			{

			}

			public ProductTicketBindItem(ProductTicket productTicket)
			{
				Id = productTicket.ProductProvider.Id;
				ProductId = productTicket.ProductProvider.Product.Id;
				ProviderId = productTicket.ProductProvider.Provider.Id;
				Product = productTicket.ProductProvider.Product.NameAndDescription;
				Provider = productTicket.ProductProvider.Provider.NameAndCUIT;
				UnitaryPrice = productTicket.ProductProvider.Price;
				Quantity = productTicket.EstimatedQuantity;
				Deleted = productTicket.ProductProvider.Product.Deleted || productTicket.ProductProvider.Provider.Deleted;
			}

			public static IList<ProductTicketBindItem> ToProductTicketBindItems(IList<ProductTicket> productTickets)
			{
				List<ProductTicketBindItem> items = new List<ProductTicketBindItem>();
				foreach (var productTicket in productTickets)
					items.Add(new ProductTicketBindItem(productTicket));
				return items;
			}

			public static IList<ProductTicket> ToProductTicketItems(IList<ProductTicketBindItem> productTicketBindItems)
			{
				List<ProductTicket> items = new List<ProductTicket>();
				foreach (var productTicketBindItem in productTicketBindItems)
				{
					items.Add(new ProductTicket
						{
							EstimatedQuantity = productTicketBindItem.Quantity,
							ProductProvider = new ProductProvider
							{
								Id = productTicketBindItem.Id,
								Product = new Product { Id = productTicketBindItem.ProductId, Name = productTicketBindItem.Product },
								Provider = new Provider { Id = productTicketBindItem.ProviderId, Name = productTicketBindItem.Provider }
							}
						});
				}
				return items;
			}
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
			LoadProducts();
			if (!IsPostBack)
			{
				if (ReadOnly)
				{
					_addControls.Visible = false;
					_items.Rows[0].Cells.RemoveAt(_items.Rows[0].Cells.Count - 1);
				}
			}
			else
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
				if (!Page.ClientScript.IsClientScriptIncludeRegistered("ProductTicketSelectionGrid"))
				{
					Page.ClientScript.RegisterClientScriptInclude("ProductTicketSelectionGrid", ResolveUrl("ProductTicketSelectionGrid.js"));
					Page.ClientScript.RegisterStartupScript(typeof(string), "ProductTicketSelectionGridObject",
						string.Format("var {0}_instance = new ProductTicketSelectionGrid('{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', {8}, '{9}');",
							ClientID, _items.ClientID, _add.ClientID, _selectedItems.ClientID, 
							_product.ClientID, _provider.ClientID, _quantity.ClientID, 
							GetDeleteImagePath(), ReadOnly.ToString().ToLower(), _totalQuantity.ClientID),
							true);
				}
			}
			else
			{
				string script = string.Format("var {0}_instance = new ProductTicketSelectionGrid('{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', {8}, '{9}');",
							ClientID, _items.ClientID, _add.ClientID, _selectedItems.ClientID,
							_product.ClientID, _provider.ClientID, _quantity.ClientID,
							GetDeleteImagePath(), ReadOnly.ToString().ToLower(), _totalQuantity.ClientID);
				ScriptManager.RegisterStartupScript(this,
					typeof(string),
					"ProductTicketSelectionGridObject",
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

		protected void _product_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(_product.SelectedValue))
				LoadProviders();
		}

		private void LoadProviders()
		{
			int productId = _product.SelectedValue.ToInt32();

			var product = IoCContainer.ProductManager.Get(productId);

			foreach (var provider in product.Providers)
			{
				ListItem item = new ListItem();
				item.Value = JsonConvert.SerializeObject(new { Id = provider.Id, ProviderId = provider.Provider.Id, Name = provider.Provider.NameAndCUIT, Price = provider.Price });
				item.Text = string.Format("{0}\t{1:C}", provider.Provider.NameAndCUIT, provider.Price);
				_provider.Items.Add(item);
			}
		}

		private void LoadProducts()
		{
			var selectedItems = SelectedItems;
			var selectedItemsCount = selectedItems.Count;

			ListItem firstItem = _product.Items[0];
			_product.Items.Clear();
			_product.Items.Add(firstItem);
			var products = IoCContainer.ProductManager.FindAllActiveOrdered("Name");
			foreach (var product in products)
			{
				// If is postback and the product was selected, do not put it into the drop down
				if (selectedItemsCount > 0 && selectedItems.FirstOrDefault(pt => pt.ProductProvider.Product.Id == product.Id) != null)
					continue;

				ListItem item = new ListItem();
				item.Value = product.Id.ToString();
				item.Text = product.NameAndDescription;
				_product.Items.Add(item);
			}
		}

		public bool ReadOnly
		{
			get;
			set;
		}


		[Bindable(true, BindingDirection.TwoWay)]
		public IList<ProductTicket> SelectedItems
		{
			get
			{
				return ProductTicketBindItem.ToProductTicketItems(
					JsonConvert.DeserializeObject<List<ProductTicketBindItem>>(_selectedItems.Value));
			}
			set
			{
				_selectedItems.Value = JsonConvert.SerializeObject(
						ProductTicketBindItem.ToProductTicketBindItems(value));
				LoadProducts();
				DataBind();
			}
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