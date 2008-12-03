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
	public class ListPresenter<T,PK> where T: PrimaryKeyDomainObject<T,PK>
	{
		IListView _view;
		IManager<T, PK> _manager;



		public ListPresenter(IListView view, IManager<T, PK> manager)
		{
			_view = view;
			_view.Init += new EventHandler(OnInit);
			_view.Load += new EventHandler(OnLoad);
			_manager = manager;
		}

		protected virtual void OnInit(object sender, EventArgs e)
		{
		}

		protected virtual void OnLoad(object sender, EventArgs e)
		{
			
		}

		public virtual void Select(int pageIndex, int pageSize, string orderBy)
		{
			_view.TotalRows = _manager.Count();
			if (string.IsNullOrEmpty(orderBy))
				_view.DataSource = _manager.SlicedFindAll(pageIndex, pageSize);
			else
			{
				if (orderBy.Contains("DESC"))
				{
					orderBy = orderBy.Replace("DESC", string.Empty);
					_view.DataSource = _manager.SlicedFindAllOrdered(pageIndex, pageSize, orderBy.Trim(), false);
				}
				else
				{
					orderBy = orderBy.Trim();
					_view.DataSource = _manager.SlicedFindAllOrdered(pageIndex, pageSize, orderBy.Trim());
				}
			}
			
		}
		public virtual void Delete(PK id)
		{
			_manager.Delete(id);
		}
	}
}
