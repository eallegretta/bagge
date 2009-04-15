using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.WebSite.Views;

namespace Bagge.Seti.WebSite.Presenters
{
	public class ProductListPresenter : ListPresenter<Product, int>
	{
		IProviderManager _providerManager;


		public ProductListPresenter(IProductListView view,
			IProductManager manager, IProviderManager providerManager)
			: base(view, manager)
		{
			_providerManager = providerManager;
		}

		protected override void OnInit(object sender, EventArgs e)
		{
			base.OnInit(sender, e);
			if (!View.IsPostBack)
				GetView<IProductListView>().Providers = _providerManager.FindAllOrdered("Name");
		}

		public override void Select(int pageIndex, int pageSize, string orderBy, IList<FilterPropertyValue> filters)
		{
			var filter = (from fil in filters
						  where fil.Property == "Providers"
						  select fil).FirstOrDefault();

			if (filter != null)
				filter.Value = _providerManager.Get(filter.Value.ToString().ToInt32());

			base.Select(pageIndex, pageSize, orderBy, filters);
		}
	}
}
