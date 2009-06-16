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
			View.DataBound += new EventHandler(View_DataBound);
		}

		void View_DataBound(object sender, EventArgs e)
		{
			if (View.Mode == EditorAction.Update || View.Mode == EditorAction.View)
			{
				View.Providers = SelectedEntity.Providers.ToArray();
			}
		}


		public override void Save(Product entity)
		{
			entity.Providers = View.Providers;

			base.Save(entity);
		}
	}
}
