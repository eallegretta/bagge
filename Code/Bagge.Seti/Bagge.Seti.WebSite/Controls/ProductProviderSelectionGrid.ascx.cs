using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bagge.Seti.Common;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.BusinessEntities.Exceptions;

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
				SelectedItems = new List<ProductProvider>();
				if (ReadOnly)
					_addControls.Visible = false;
			}

		}

		protected void _items_DataBound(object sender, EventArgs e)
		{
			if (SourceType == ProductProviderSelectionGridSourceType.Product)
				_items.Columns[0].Visible = false;
			else
				_items.Columns[1].Visible = false;
			if (ReadOnly)
				_items.Columns[2].Visible = false;
		}


		public bool ReadOnly
		{
			get;
			set;
		}
		

		private List<ProductProvider> SelectedItems
		{
			get { return ViewState["SelectedItems"] as List<ProductProvider>; }
			set 
			{
				ViewState["SelectedItems"] = value;
				BindItems();
			}
		}

		public string[] GetItems(string prefixText, int count)
		{
			if (SourceType == ProductProviderSelectionGridSourceType.Product)
			{
				var productNames = from product in IoCContainer.ProductManager.FindAllByName(prefixText, count)
								   select product.Name;
				return productNames.ToArray();
			}
			else
			{
				var providerNames = from provider in IoCContainer.ProviderManager.FindAllByName(prefixText, count)
									select provider.Name;
				return providerNames.ToArray();
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
			Provider provider = null;
			try
			{
				provider = IoCContainer.ProviderManager.GetByName(_name.Text);
			}
			catch (ObjectNotFoundException)
			{
			}
			if (provider != null)
			{
				SelectedItems.Add(new ProductProvider { Provider = provider });
				BindItems();
			}
		}

		private void BindItems()
		{
			_items.DataSource = SelectedItems;
			_items.DataBind();
		}



		private void AddSelectedProduct()
		{
			Product product = null;
			try
			{
				product = IoCContainer.ProductManager.GetByName(_name.Text);
			}
			catch (ObjectNotFoundException)
			{
			}
			if (product != null)
			{
				SelectedItems.Add(new ProductProvider { Product = product } );
				BindItems();
			}
		}

		protected void _items_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "Delete")
			{
			}
		}
	}
}