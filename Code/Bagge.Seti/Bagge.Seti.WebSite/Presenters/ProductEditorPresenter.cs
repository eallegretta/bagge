using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.BusinessLogic.Contracts;

namespace Bagge.Seti.WebSite.Presenters
{
	public class ProductEditorPresenter: EditorPresenter<Product, int>
	{

		public ProductEditorPresenter(IProductEditorView view, IProductManager manager)
			: base(view, manager)
		{
		}

		protected new IProductEditorView View
		{
			get { return GetView<IProductEditorView>(); }
		}

		protected override void OnInit(object sender, EventArgs e)
		{
			base.OnInit(sender, e);

		}

		public override void Save(Product entity)
		{
			entity.Providers = View.Providers;

			base.Save(entity);
		}
	}
}
