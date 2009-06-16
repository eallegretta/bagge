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
				if (countryState != null)
				{
					countryState.DataSource = value;
					countryState.DataBind();
				}
			}
		}

		public District[] Districts
		{
			set
			{
				var district = Details.FindControl("_district") as DropDownList;
				if (district != null)
				{
					if (value != null && value.Length > 0)
						district.SelectedValue = value[0].Id.ToString();
					district.DataSource = value;
					district.DataBind();
				}
			}
		}

		public int SelectedCountryId
		{
			set
			{
				var countryState = ((DropDownList)Details.FindControl("_countryState"));
				if (countryState != null)
					countryState.SelectedValue = value.ToString();
			}
		}

		public int? SelectedDistrictId
		{
			get
			{
				var districts = Details.FindControl("_district");
				string value = string.Empty;
				if (districts is DropDownList)
					value = ((DropDownList)districts).SelectedValue;
				else
					value = ((HiddenField)districts).Value;
				if (string.IsNullOrEmpty(value))
					return null;
				return value.ToInt32();
			}
			set
			{
				if (value.HasValue)
				{
					var district = Details.FindControl("_district");
					if (district is DropDownList)
						((DropDownList)district).SelectedValue = value.ToString();
					else
						((HiddenField)district).Value = value.ToString();
				}
			}
		}

		public string ZipCode
		{
			set
			{
				var zipCode = ((TextBox)Details.FindControl("_zipCode"));
				if (zipCode != null)
					zipCode.Text = value;
			}
		}

		public ProductProvider[] Products
		{
			get
			{
				var products = ((ProductProviderSelectionGrid)Details.FindControl("_products"));
				if (products != null)
					return products.SelectedItems.ToArray();
				return null;
			}
			set
			{
				var products = ((ProductProviderSelectionGrid)Details.FindControl("_products"));
				if (products != null)
					products.SelectedItems = value.ToList();
			}
		}

		#endregion

		#region IProviderEditorView Members

		public ProviderCalification[] Califications
		{
			set
			{
				var calification = Details.FindControl("_calification");
				if (calification is DropDownList)
				{
					((DropDownList)calification).DataSource = value;
					calification.DataBind();
				}
			}
		}

		public int SelectedCalificationId
		{
			get
			{
				var calification = Details.FindControl("_calification");
				if (calification is DropDownList)
					return ((DropDownList)calification).SelectedValue.ToInt32();
				else
					return ((HiddenField)calification).Value.ToInt32();
			}
			set
			{
				var calification = Details.FindControl("_calification");
				if (calification is DropDownList)
					((DropDownList)calification).SelectedValue = value.ToString();
				else
					((HiddenField)calification).Value = value.ToString();
			}
		}

		#endregion
	}
}
