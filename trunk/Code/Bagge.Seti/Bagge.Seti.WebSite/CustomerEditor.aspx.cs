using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.WebSite.Presenters;
using Bagge.Seti.Common;
using Microsoft.Practices.Web.UI.WebControls;
using Bagge.Seti.WebSite.Views;

namespace Bagge.Seti.WebSite
{
	public partial class CustomerEditor : EditorPage<Customer, int>, ICustomerEditorView 
	{
		CustomerEditorPresenter _presenter;

		public CustomerEditor()
		{
			_presenter = new CustomerEditorPresenter(this, IoCContainer.CustomerManager, IoCContainer.CountryStateManager, IoCContainer.DistrictManager);

		}

		protected void _cuitUniqueVal_ServerValidate(object source, ServerValidateEventArgs args)
		{
			args.IsValid = _presenter.IsCuitValid(args.Value);
		}


		protected override bool ShowRequiredInformationLabel
		{
			get { return true; }
		}

		protected override EditorPresenter<Customer, int> Presenter
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

		#region ICustomerEditorView Members



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
				var district = Details.FindControl("_district");
				if (district is DropDownList)
				{
					((DropDownList)district).DataSource = value;
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

		#endregion
	}
}
