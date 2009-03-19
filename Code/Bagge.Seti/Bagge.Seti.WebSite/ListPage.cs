using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.WebSite.Presenters;
using Bagge.Seti.BusinessLogic;
using Microsoft.Practices.Web.UI.WebControls;
using Bagge.Seti.WebSite.Controls;

namespace Bagge.Seti.WebSite
{
	public abstract class ListPage<T,PK> :  Page, IListView where T: PrimaryKeyDomainObject<T, PK>
	{
		protected override void OnInit(EventArgs e)
		{
			ObjectDataSource.Selecting += new EventHandler<ObjectContainerDataSourceSelectingEventArgs>(ObjectDataSource_Selecting);
			ObjectDataSource.Deleting += new EventHandler<ObjectContainerDataSourceDeletingEventArgs>(ObjectDataSource_Deleting);
			ObjectDataSource.DataObjectTypeName = typeof(T).FullName;
			ObjectDataSource.UsingServerPaging = true;

			if (Grid is SecureGridView)
				((SecureGridView)Grid).SecureTypeName = typeof(T).AssemblyQualifiedName;

			base.OnInit(e);
		}

		void ObjectDataSource_Deleting(object sender, ObjectContainerDataSourceDeletingEventArgs e)
		{
			OnDeleting((PK)e.Keys["Id"]);
			e.Cancel = true;
		}

		private void ObjectDataSource_Selecting(object sender, ObjectContainerDataSourceSelectingEventArgs e)
		{
			if(!IsPostBack)
				OnSelecting(e.Arguments.StartRowIndex, e.Arguments.MaximumRows, DefaultSortExpression);
			else
				OnSelecting(e.Arguments.StartRowIndex, e.Arguments.MaximumRows, e.Arguments.SortExpression);
		}

		protected virtual void OnDeleting(PK id)
		{
			Presenter.Delete(id);
		}

		bool _selected = false;

		protected virtual void OnSelecting(int pageIndex, int pageSize, string orderBy)
		{
			if (!_selected)
			{
				Presenter.Select(pageIndex, pageSize, orderBy);
				_selected = true;
			}
		}

		protected abstract GridView Grid
		{
			get;
		}

		protected abstract ListPresenter<T, PK> Presenter
		{
			get;
		}

		protected abstract ObjectContainerDataSource ObjectDataSource
		{
			get;
		}

		public virtual string DefaultSortExpression
		{
			get
			{
				if (typeof(T).BaseType.Equals(typeof(PrimaryKeyWithNameDomainObject<T, PK>)))
					return "Name";
				return string.Empty;
			}
		}


		#region IView Members


		public object DataSource
		{
			set
			{
				ObjectDataSource.DataSource = value;
			}
		}

		public virtual int TotalRows
		{
			set { ObjectDataSource.TotalRowCount = value; }
		
		}


		#endregion
	}
}
