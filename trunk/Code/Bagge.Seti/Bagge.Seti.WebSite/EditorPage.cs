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
using Bagge.Seti.WebSite.Controls;
using System.Web.UI;
using Bagge.Seti.Security.BusinessEntities;

namespace Bagge.Seti.WebSite
{
	public abstract class EditorPage<T, PK> : Page, IEditorView<PK> where T: PrimaryKeyDomainObject<T, PK>
	{

		protected override void OnInit(EventArgs e)
		{
			ObjectDataSource.DataBinding += new EventHandler(ObjectDataSource_DataBinding);

			ObjectDataSource.Selecting += new EventHandler<ObjectContainerDataSourceSelectingEventArgs>(ObjectDataSource_Selecting);
			ObjectDataSource.Inserting += new EventHandler<ObjectContainerDataSourceInsertingEventArgs>(ObjectDataSource_Inserting);
			ObjectDataSource.Updating += new EventHandler<ObjectContainerDataSourceUpdatingEventArgs>(ObjectDataSource_Updating);

			ObjectDataSource.DataObjectTypeName = typeof(T).FullName;

			((Editor)Master).RequiredInformation.Visible = ShowRequiredInformationLabel;

			AssignTypeNameToSecureContainers(typeof(T).AssemblyQualifiedName);

			base.OnInit(e);
		}

		void ObjectDataSource_DataBinding(object sender, EventArgs e)
		{
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
			if (instance is IAuditable)
			{
				((IAuditable)instance).AuditTimeStamp = Timestamp;
			}
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
			if (!IsPostBack)
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
					((DetailsView)Details).DefaultMode = mode;
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
					((FormView)Details).DefaultMode = mode;
				}
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

		protected abstract bool ShowRequiredInformationLabel
		{
			get;
		}

		/*public override void DataBind()
		{
			Details.DataBind();
		}*/





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

		public virtual PK PrimaryKey
		{
			get
			{
				if (HttpContext.Current.Request.QueryString["Id"] == null)
					return default(PK);
				return (PK)Convert.ChangeType(HttpContext.Current.Request.QueryString["Id"], typeof(PK));
			}
		}

		public virtual byte[] Timestamp
		{
			get { return ViewState["Timestamp"] as byte[]; }
			set { ViewState["Timestamp"] = value; }
		}

		public virtual EditorAction Mode
		{
			get 
			{
				if (HttpContext.Current.Request.QueryString["Id"] == null)
					return EditorAction.Insert;
				else if (HttpContext.Current.Request.QueryString["Action"] == null || HttpContext.Current.Request.QueryString["Action"].ToUpperInvariant() == "EDIT")
					return EditorAction.Update;
				else
					return EditorAction.View;
			}
		}


		#endregion

		#region IEditorView<PK> Members


		public virtual event EventHandler DataBound
		{
			add
			{
				Details.DataBound += value;
			}
			remove
			{
				Details.DataBound -= value;
			}
		}

		#endregion
	}
}
