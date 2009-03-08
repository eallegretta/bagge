using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.WebSite.Presenters;
using Bagge.Seti.BusinessLogic;
using System.Collections;
using System.ComponentModel;
using Microsoft.Practices.Web.UI.WebControls;
using Microsoft.Practices.Web.UI.WebControls.Utility;

namespace Bagge.Seti.WebSite
{
	public abstract class EditorPage<T, PK> : System.Web.UI.Page, IEditorView<PK> where T: PrimaryKeyDomainObject<T, PK>
	{

		protected override void OnInit(EventArgs e)
		{
			ObjectDataSource.Selecting += new EventHandler<ObjectContainerDataSourceSelectingEventArgs>(ObjectDataSource_Selecting);
			ObjectDataSource.Inserting += new EventHandler<ObjectContainerDataSourceInsertingEventArgs>(ObjectDataSource_Inserting);
			ObjectDataSource.Updating += new EventHandler<ObjectContainerDataSourceUpdatingEventArgs>(ObjectDataSource_Updating);
			ObjectDataSource.DataObjectTypeName = typeof(T).FullName;

			base.OnInit(e);
		}

		void ObjectDataSource_Selecting(object sender, ObjectContainerDataSourceSelectingEventArgs e)
		{
			OnSelecting();
		}

		void ObjectDataSource_Updating(object sender, ObjectContainerDataSourceUpdatingEventArgs e)
		{
			T instance = Activator.CreateInstance<T>();
			TypeDescriptionHelper.BuildInstance(e.NewValues, instance);
			instance.Id = PrimaryKey;
			OnUpdating(instance);
		}

		void ObjectDataSource_Inserting(object sender, ObjectContainerDataSourceInsertingEventArgs e)
		{
			T instance = Activator.CreateInstance<T>();
			TypeDescriptionHelper.BuildInstance(e.NewValues, instance);
			OnInserting(instance);
		}
		protected override void OnLoad(EventArgs e)
		{
			if (Details is DetailsView)
			{
				DetailsViewMode mode;
				switch (Mode)
				{
					case EditorAction.Insert: mode = DetailsViewMode.Insert; break;
					case EditorAction.Update: mode = DetailsViewMode.Edit; break;
					default: mode = DetailsViewMode.ReadOnly; break;
				}
				((DetailsView)Details).ChangeMode(mode);
			}
			else if (Details is FormView)
			{
				FormViewMode mode;
				switch (Mode)
				{
					case EditorAction.Insert: mode = FormViewMode.Insert; break;
					case EditorAction.Update: mode = FormViewMode.Edit; break;
					default: mode = FormViewMode.ReadOnly; break;
				}
				((FormView)Details).ChangeMode(mode);
			}

			base.OnLoad(e);
			
		}

		

		protected virtual void OnInserting(T instance)
		{
			Presenter.Save(instance);
		}
		protected virtual void OnUpdating(T instance)
		{
			Presenter.Save(instance);
		}
		protected virtual void OnSelecting()
		{
			Presenter.Select();
		}


		protected abstract EditorPresenter<T, PK> Presenter
		{
			get;
		}
		protected abstract CompositeDataBoundControl Details
		{
			get;
		}
		protected abstract ObjectContainerDataSource ObjectDataSource
		{
			get;
		}



		#region IListView Members

		
		#endregion

		#region IView Members



		private bool IsDataSourceEmpty
		{
			get;
			set;
		}

		public object DataSource
		{
			set
			{
				ObjectDataSource.DataSource = value;
			}
		}

		#endregion

		#region IEditorView<PK> Members

		public PK PrimaryKey
		{
			get
			{
				if (ViewState["Id"] == null)
					return default(PK);
				return (PK)Convert.ChangeType(ViewState["Id"], typeof(PK));
			}
		}

		public EditorAction Mode
		{
			get 
			{
				if (!IsPostBack)
				{
					if (Request.QueryString["Id"] == null)
						ViewState["Mode"] = EditorAction.Insert;
					else if (Request.QueryString["Action"] == null || Request.QueryString["Action"].ToUpperInvariant() == "VIEW")
					{
						ViewState["Id"] = Request.QueryString["Id"];
						ViewState["Mode"] = EditorAction.Update;
					}
					else
					{
						ViewState["Id"] = Request.QueryString["Id"];
						ViewState["Mode"] = EditorAction.View;
					}
				}
				return (EditorAction)ViewState["Mode"];
			}
		}

		#endregion
	}
}
