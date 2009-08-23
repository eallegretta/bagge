﻿using System;
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
using Bagge.Seti.Security.BusinessEntities;

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
			Grid.DataBinding += new EventHandler(Grid_DataBinding);
			Grid.RowDataBound += new GridViewRowEventHandler(Grid_RowDataBound);

			AssignTypeNameToSecureContainers(typeof(T).AssemblyQualifiedName);

			base.OnInit(e);
		}

		void Grid_DataBinding(object sender, EventArgs e)
		{
			SetEditIndexFieldValue();
		}

		int? _editFieldIndex = null;

		void Grid_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				var auditable = e.Row.DataItem as IAuditable;
				if (auditable != null)
				{
					if (_editFieldIndex.HasValue && auditable.Deleted)
						e.Row.Cells[_editFieldIndex.Value].Visible = false;
				}
			}
		}

		private void SetEditIndexFieldValue()
		{
			for (int index = 0; index < Grid.Columns.Count && !_editFieldIndex.HasValue; index++)
			{
				var field = Grid.Columns[index] as IMethodSecureControl;
				if (field != null)
				{
					if (field.MethodName == "Update")
						_editFieldIndex = index;
				}
			}
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
			{
				if(string.IsNullOrEmpty(e.Arguments.SortExpression))
					OnSelecting(e.Arguments.StartRowIndex, e.Arguments.MaximumRows, DefaultSortExpression);
				else
					OnSelecting(e.Arguments.StartRowIndex, e.Arguments.MaximumRows, e.Arguments.SortExpression);
			}
		}

		public bool IsDelete
		{
			get
			{
				var keys = (from key in HttpContext.Current.Request.Form.AllKeys
						   where key != null && (key.Contains("$delete.x") || key.Contains("$delete.y"))
						   select key);

				
				if (keys != null && keys.ToArray().Length == 2)
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
				if(typeof(PrimaryKeyWithNameDomainObject<T, PK>).IsAssignableFrom(typeof(T)))
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