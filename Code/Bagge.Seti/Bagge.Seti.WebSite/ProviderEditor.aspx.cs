using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.WebSite.Presenters;
using Bagge.Seti.Common;
using Bagge.Seti.WebSite.Controls;
using Microsoft.Practices.Web.UI.WebControls;

namespace Bagge.Seti.WebSite
{
	public partial class ProviderEditor: EditorPage<Provider, int>, IProviderEditorView 
	{
		ProviderEditorPresenter _presenter;

		public ProviderEditor()
		{
			_presenter = new ProviderEditorPresenter(this, IoCContainer.ProviderManager, IoCContainer.ProviderCalificationManager, IoCContainer.CountryStateManager, IoCContainer.DistrictManager);

		}

		protected void _cuitUniqueVal_ServerValidate(object source, ServerValidateEventArgs args)
		{
			args.IsValid = _presenter.IsCuitValid(args.Value);
		}


		protected override bool ShowRequiredInformationLabel
		{
			get { return true; }
		}

		protected override EditorPresenter<Provider, int> Presenter
		{
			get { return _presenter; }
		}

		protected override CompositeDataBoundControl Details
		{
			get { return _details; }
		}

		protected override ObjectContainerDataSource ObjectDataSource
		{
			get { return _dataSource; }
		}


		protected void _countryState_SelectedIndexChanged(object sender, EventArgs e)
		{
			

			int countryStateId = ((DropDownList)sender).SelectedValue.ToInt32();
			_presenter.SelectDistricts(countryStateId);
			_presenter.SelectZipCode();
		}

		protected void _district_SelectedIndexChanged(object sender, EventArgs e)
		{
			_presenter.SelectZipCode();
		}

		#region IProviderEditorView Members



		public CountryState[] CountryStates
		{
			set 
			{
				var countryState = ((DropDownList)Details.FindControl("_countryState"));
				countryState.DataSource = value;
				countryState.DataBind();
			}
		}

		public District[] Districts
		{
			set 
			{
				var countryState = ((DropDownList)Details.FindControl("_district"));
				countryState.DataSource = value;
				countryState.DataBind();
			}
		}

		public int SelectedCountryId
		{
			set { ((DropDownList)Details.FindControl("_countryState")).SelectedValue = value.ToString(); }
		}

		public int? SelectedDistrictId
		{
			get
			{
				var districts = ((DropDownList)Details.FindControl("_district"));
				if (districts.SelectedValue.IsNullOrEmpty())
					return null;

				return districts.SelectedValue.ToInt32();
			}
			set
			{
				if(value.HasValue)
					((DropDownList)Details.FindControl("_district")).SelectedValue = value.ToString();
			}
		}

		public string ZipCode
		{
			set
			{
				((TextBox)Details.FindControl("_zipCode")).Text = value;
			}
		}

		public ProductProvider[] Products
		{
			get
			{
				return ((ProductProviderSelectionGrid)Details.FindControl("_products")).SelectedItems.ToArray();
			}
			set
			{
				((ProductProviderSelectionGrid)Details.FindControl("_products")).SelectedItems = value.ToList();
			}
		}

		#endregion

		#region IProviderEditorView Members

		public ProviderCalification[] Califications
		{
			set
			{
				var calification = ((DropDownList)Details.FindControl("_calification"));
				calification.DataSource = value;
				calification.DataBind();
			}
		}

		public int SelectedCalificationId
		{
			get
			{
				return ((DropDownList)Details.FindControl("_calification")).SelectedValue.ToInt32();
			}
			set
			{
				((DropDownList)Details.FindControl("_calification")).SelectedValue = value.ToString();
			}
		}

		#endregion
	}
}
