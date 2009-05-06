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
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.WebSite.Presenters
{
	public class EditorPresenter<T,PK> where T: PrimaryKeyDomainObject<T,PK>
	{
		IEditorView<PK> _view;
		IManager<T, PK> _manager;

		protected IManager<T, PK> Manager
		{
			get { return _manager; }
		}

		protected M GetManager<M>() where M : class
		{
			return _manager as M;
		}

		protected IEditorView<PK> View
		{
			get { return _view; }
		}

		protected V GetView<V>() where V : class
		{
			return View as V;
		}

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
			View.DataBind();
		}

		protected T SelectedEntity
		{
			get;
			set;
		}

		public virtual void Select()
		{
			if (View.Mode == EditorAction.Update || View.Mode == EditorAction.View)
			{
				SelectedEntity = _manager.Get(_view.PrimaryKey);
				_view.DataSource = SelectedEntity.ToSingleItemArray<T>();

				if (SelectedEntity is IAuditable)
				{
					_view.Timestamp = ((IAuditable)SelectedEntity).AuditTimeStamp;
				}
			}
		}

		public virtual void Save(T entity)
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
