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
using Bagge.Seti.Common.Helpers;

namespace Bagge.Seti.WebSite
{
	public abstract class EditorPage<T, PK> : System.Web.UI.Page, IEditorView<PK> where T: PrimaryKeyDomainObject<T, PK>
	{

		protected override void OnInit(EventArgs e)
		{

			if (Details is DetailsView)
			{
				((DetailsView)Details).ItemInserting += new DetailsViewInsertEventHandler(OnDetailsViewItemInserting);
				((DetailsView)Details).ItemUpdating += new DetailsViewUpdateEventHandler(OnDetailsViewItemUpdating);
			}
			else if (Details is FormView)
			{
				((FormView)Details).ItemInserting += new FormViewInsertEventHandler(OnFormsViewItemInserting);
				((FormView)Details).ItemUpdating += new FormViewUpdateEventHandler(OnFormsViewItemUpdating);
			}

			base.OnInit(e);
			
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

		void OnFormsViewItemUpdating(object sender, FormViewUpdateEventArgs e)
		{
			T instance = Activator.CreateInstance<T>();
			TypeDescriptionHelper.BuildInstance(e.NewValues, instance);
			OnUpdating(instance);
		}

		protected virtual void OnInserting(T instance)
		{
			Presenter.Save(instance);
		}
		protected virtual void OnUpdating(T instance)
		{
			Presenter.Save(instance);
		}

		private void OnFormsViewItemInserting(object sender, FormViewInsertEventArgs e)
		{
			T instance = Activator.CreateInstance<T>();
			TypeDescriptionHelper.BuildInstance(e.Values, instance);
			OnInserting(instance);
		}

		private void OnDetailsViewItemUpdating(object sender, DetailsViewUpdateEventArgs e)
		{
			T instance = Activator.CreateInstance<T>();
			TypeDescriptionHelper.BuildInstance(e.NewValues, instance);
			OnUpdating(instance);	
		}

		private void OnDetailsViewItemInserting(object sender, DetailsViewInsertEventArgs e)
		{
			T instance = Activator.CreateInstance<T>();
			TypeDescriptionHelper.BuildInstance(e.Values, instance);
			OnInserting(instance);
		}

		

		protected abstract EditorPresenter<T, PK> Presenter
		{
			get;
		}
		protected abstract CompositeDataBoundControl Details
		{
			get;
		}

		#region IListView Members

		
		#endregion

		#region IView Members


		public object DataSource
		{
			get
			{
				return Details.DataSource;
			}
			set
			{
				Details.DataSource = value;
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
					else if (Request.QueryString["Action"].ToLowerInvariant() == "view")
					{
						ViewState["Id"] = Request.QueryString["Id"];
						ViewState["Mode"] = EditorAction.View;
					}
					else
					{
						ViewState["Id"] = Request.QueryString["Id"];
						ViewState["Mode"] = EditorAction.Update;
					}
				}
				return (EditorAction)ViewState["Mode"];
			}
		}

		#endregion
	}
}
