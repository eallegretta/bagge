﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.WebSite.Presenters;
using Bagge.Seti.Common;
using Microsoft.Practices.Web.UI.WebControls;
using Bagge.Seti.WebSite.Controls;
using Bagge.Seti.BusinessEntities.Security;
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.WebSite
{
	[SecurizableWeb("Securizable_ProductEditor", typeof(ProductEditor), FunctionAction.Retrieve | FunctionAction.Create | FunctionAction.Update)]
	public partial class ProductEditor : EditorPage<Product, int>, IProductEditorView 
	{
		ProductEditorPresenter _presenter;

		public ProductEditor()
		{
			_presenter = new ProductEditorPresenter(this, IoCContainer.ProductManager);
		}

		protected override bool ShowRequiredInformationLabel
		{
			get { return true; }
		}

		protected override EditorPresenter<Product, int> Presenter
		{
			get { return _presenter; }
		}

		protected override CompositeDataBoundControl Details
		{
			get { return _details; }
		}

		protected override ObjectContainerDataSource ObjectDataSource
		{
			get { return _dataSource; }
		}


		
		public ProductProvider[] Providers
		{
			get
			{
				var providers = ((ProductProviderSelectionGrid)Details.FindControl("_providers"));
				if(providers != null)
					return providers.SelectedItems.ToArray();
				return null;
			}
			set
			{
				var providers = ((ProductProviderSelectionGrid)Details.FindControl("_providers"));
				if(providers != null)
					providers.SelectedItems = value.ToList();
			}
		}


	}
}
