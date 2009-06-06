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
			public string Name { get; set; }
			public decimal? Quantity { get; set; }
			public decimal? Price { get; set; }

			public ProductTicketBindItem()
			{

			}

			public ProductTicketBindItem(ProductTicket productTicket)
			{
				Id = productTicket.Id;
				Name = productTicket.Product.NameAndDescription;
				Quantity = productTicket.EstimatedQuantity;
			}

			public static IList<ProductTicketBindItem> ToProductProviderBindItems(IList<ProductTicket> productTickets)
			{
				List<ProductTicketBindItem> items = new List<ProductTicketBindItem>();
				foreach (var productTicket in productTickets)
					items.Add(new ProductTicketBindItem(productTicket));
				return items;
			}

			public static IList<ProductTicket> ToProductProviderItems(IList<ProductTicketBindItem> productTicketBindItems)
			{
				List<ProductTicket> items = new List<ProductTicket>();
				foreach (var productTicketBindItem in productTicketBindItems)
				{
					items.Add(new ProductTicket
						{
							EstimatedQuantity = productTicketBindItem.Quantity,
							Product = new Product
							{
								Id = productTicketBindItem.Id,
								Name = productTicketBindItem.Name
							}
						});
				}
				return items;
			}
		}


		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				LoadProducts();
				

				if (ReadOnly)
				{
					_addControls.Visible = false;
					_items.Rows[0].Cells.RemoveAt(2);
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
						string.Format("var {0}_instance = new ProductTicketSelectionGrid('{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7}, '{8}', '{9}');",
							ClientID, _items.ClientID, _add.ClientID, _selectedItems.ClientID, _name.ClientID, _quantity.ClientID,
							GetDeleteImagePath(), ReadOnly.ToString().ToLower(), _totalQuantity.ClientID, _totalPrice.ClientID),
							true);
				}
			}
			else
			{
				string script = string.Format("var {0}_instance = new ProductTicketSelectionGrid('{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7}, '{8}', '{9}');",
							ClientID, _items.ClientID, _add.ClientID, _selectedItems.ClientID, _name.ClientID, _quantity.ClientID,
							GetDeleteImagePath(), ReadOnly.ToString().ToLower(), _totalQuantity.ClientID, _totalPrice.ClientID);
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
				return ProductTicketBindItem.ToProductProviderItems(
					JsonConvert.DeserializeObject<List<ProductTicketBindItem>>(_selectedItems.Value));
			}
			set
			{
				_selectedItems.Value = JsonConvert.SerializeObject(
						ProductTicketBindItem.ToProductProviderBindItems(value));
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