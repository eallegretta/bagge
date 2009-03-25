using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.BusinessLogic;
using System.Web.UI.WebControls;
using System.Web.UI;
using Bagge.Seti.BusinessLogic.Contracts;

namespace Bagge.Seti.WebSite.Presenters
{
	public class ProviderListPresenter: ListPresenter<Provider, int>
	{
		IProductManager _productManager;


		public ProviderListPresenter(IProviderListView view, 
			IProviderManager manager, IProductManager productManager): base(view, manager)
		{
			_productManager = productManager;
		}

		protected override void OnInit(object sender, EventArgs e)
		{
			base.OnInit(sender, e);
			if(!View.IsPostBack)
				GetView<IProviderListView>().Products = _productManager.FindAllOrdered("Name");
		}

		public override void Select(int pageIndex, int pageSize, string orderBy, IList<FilterPropertyValue> filters)
		{
			var filter = (from fil in filters
						  where fil.Property == "Products"
						  select fil).FirstOrDefault();

			if (filter != null)
				filter.Value = _productManager.Get(filter.Value.ToString().ToInt32());

			base.Select(pageIndex, pageSize, orderBy, filters);
		}
	}
}
