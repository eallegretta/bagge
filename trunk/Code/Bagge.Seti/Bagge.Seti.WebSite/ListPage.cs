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
using System.Web.UI;
using Bagge.Seti.Extensions;

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

			Grid.RowCommand += new GridViewCommandEventHandler(Grid_RowCommand);

			AssignTypeNameToSecureContainers(typeof(T).AssemblyQualifiedName);

			base.OnInit(e);
		}

		void Grid_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "Undelete")
				OnUndeleting((PK)Grid.DataKeys[e.CommandArgument.ToString().To<int>()].Value);
			DataBind();
		}

		void ObjectDataSource_Deleting(object sender, ObjectContainerDataSourceDeletingEventArgs e)
		{
			OnDeleting((PK)e.Keys["Id"]);
			e.Cancel = true;
			DataBind();
		}

		private void ObjectDataSource_Selecting(object sender, ObjectContainerDataSourceSelectingEventArgs e)
		{
			if (!IsPostBack)
				OnSelecting(e.Arguments.StartRowIndex, e.Arguments.MaximumRows, DefaultSortExpression);
			else
				OnSelecting(e.Arguments.StartRowIndex, e.Arguments.MaximumRows, e.Arguments.SortExpression);

			
		}

		public bool IsDelete
		{
			get
			{
				var keys = from key in HttpContext.Current.Request.Form.AllKeys
						   where key.Contains("$delete.x") || key.Contains("$delete.y")
						   select key;

				if (keys.ToArray().Length == 2)
					return true;

				return false;
			}
		}

		protected virtual void OnUndeleting(PK id)
		{
			Presenter.Undelete(id);
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
			set 
			{ 
				ObjectDataSource.TotalRowCount = value;
			}
		
		}


		#endregion
	}
}
