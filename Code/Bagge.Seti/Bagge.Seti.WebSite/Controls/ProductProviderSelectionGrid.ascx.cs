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
			if (SourceType == ProductProviderSelectionGridSourceType.Product)
				_nameExt.ServiceMethod = "GetProducts";
			else
				_nameExt.ServiceMethod = "GetProviders";

			if (!IsPostBack)
			{
				SelectedItems = new List<ProductProvider>();
				if (ReadOnly)
					_addControls.Visible = false;

				_legendProvider.Visible = _legendProduct.Visible = false;
				if (SourceType == ProductProviderSelectionGridSourceType.Product)
					_legendProduct.Visible = true;
				else
					_legendProvider.Visible = true;
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


		public List<ProductProvider> SelectedItems
		{
			get { return ViewState["SelectedItems"] as List<ProductProvider>; }
			set
			{
				ViewState["SelectedItems"] = value;
				BindItems();
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
			decimal price = 0;
			try
			{
				provider = IoCContainer.ProviderManager.GetByName(_name.Text);
				price = decimal.Parse(_price.Text);
			}
			catch (ObjectNotFoundException)
			{
				return;
			}
			catch (FormatException)
			{
				return;
			}
			if (provider != null)
			{
				if (SelectedItems.Where(p => p.Provider.Equals(provider)).Select(p => p).Count() == 0)
				{
					SelectedItems.Add(new ProductProvider { Provider = provider, Price = price });
				}
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
			decimal price = 0;
			try
			{
				product = IoCContainer.ProductManager.GetByName(_name.Text);
				price = decimal.Parse(_price.Text);
			}
			catch (ObjectNotFoundException)
			{
				return;
			}
			catch (FormatException)
			{
				return;
			}
			if (product != null)
			{
				if (SelectedItems.Where(p => p.Product.Equals(product)).Select(p => p).Count() == 0)
				{
					SelectedItems.Add(new ProductProvider { Product = product, Price = price });
				}
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