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


		protected void _countryState_SelecteIndexChanged(object sender, EventArgs e)
		{
			int countryStateId = ((DropDownList)sender).SelectedValue.ToInt32();
			_presenter.SelectDistricts(countryStateId);
		}

		#region ICustomerEditorView Members



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

		public int SelectedDistrictId
		{
			get
			{
				return ((DropDownList)Details.FindControl("_district")).SelectedValue.ToInt32();
			}
			set
			{
				((DropDownList)Details.FindControl("_district")).SelectedValue = value.ToString();
			}
		}

		#endregion
	}
}
