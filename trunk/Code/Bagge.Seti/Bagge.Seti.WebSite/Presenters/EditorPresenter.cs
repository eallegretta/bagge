using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.BusinessLogic;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Bagge.Seti.WebSite.Presenters
{
	public class EditorPresenter<T,PK> where T: PrimaryKeyDomainObject<T,PK>
	{
		IEditorView<PK> _view;
		IManager<T, PK> _manager;

		public EditorPresenter(IEditorView<PK> view, IManager<T, PK> manager)
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


		public void Select()
		{
			_view.DataSource = _manager.Get(_view.PrimaryKey).ToSingleItemArray<T>();
		}

		public void Save(T entity)
		{
			if (_view.Mode == EditorAction.View)
				throw new Exception("EditorAction cannot be View");

			if (_view.Mode == EditorAction.Insert)
				_manager.Create(entity);
			else if (_view.Mode == EditorAction.Update)
				_manager.Update(entity);
		}


	}
}
