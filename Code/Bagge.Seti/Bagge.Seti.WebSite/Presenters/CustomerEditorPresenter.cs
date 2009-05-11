using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.BusinessLogic.Contracts;

namespace Bagge.Seti.WebSite.Presenters
{
	public class CustomerEditorPresenter : EditorPresenter<Customer, int>
	{

		ISimpleFindGetManager<CountryState, int> _countryStateManager;
		ISimpleFindGetManager<District, int> _districtManager;

		public CustomerEditorPresenter(ICustomerEditorView view, ICustomerManager manager, 
			ISimpleFindGetManager<CountryState, int> countryStateManager, ISimpleFindGetManager<District, int> districtManager)
			: base(view, manager)
		{
			_countryStateManager = countryStateManager;
			_districtManager = districtManager;
		}

		protected new ICustomerEditorView View
		{
			get { return (ICustomerEditorView)base.View; }
		}

		protected override void OnInit(object sender, EventArgs e)
		{
			base.OnInit(sender, e);

			View.DataBound += new EventHandler(View_DataBound);
		}

		void View_DataBound(object sender, EventArgs e)
		{
			LoadView(View.Mode);
		}

		protected override void OnLoad(object sender, EventArgs e)
		{
			base.OnLoad(sender, e);
/*
			if (!View.IsPostBack && View.Mode == EditorAction.Insert)
			{
				LoadView(View.Mode);
			}*/
		}

		private void LoadView(EditorAction mode)
		{
			switch (mode)
			{
				case EditorAction.Insert:
					CountryState[] states = _countryStateManager.FindAll();

					View.CountryStates = states;
					if (states.Length > 1)
					{
						var districts = states[0].Districts.ToArray();
						View.Districts = districts;
						if (districts.Length > 0)
							View.ZipCode = districts[0].ZipCode;
					}
					break;
				case EditorAction.Update:
					View.CountryStates = _countryStateManager.FindAll();

					CountryState state = SelectedEntity.District.CountryState;
					View.SelectedCountryId = state.Id;
					View.Districts = state.Districts.ToArray();
					View.SelectedDistrictId = SelectedEntity.District.Id;
					break;
			}
		}

		public bool IsCuitValid(string cuit)
		{
			if (View.Mode == EditorAction.Insert)
				return GetManager<ICustomerManager>().GetByCuit(cuit) == null;
			else
			{
				var customer = GetManager<ICustomerManager>().GetByCuit(cuit);
				if (customer == null)
					return true;
				return customer.Id == View.PrimaryKey;
			}
		}

		public override void Save(Customer entity)
		{
			if(View.SelectedDistrictId.HasValue)
				entity.District = _districtManager.Get(View.SelectedDistrictId.Value);

			base.Save(entity);
		}

		public void SelectDistricts(int countryStateId)
		{
			CountryState state = _countryStateManager.Get(countryStateId);
			View.Districts = state.Districts.ToArray();
		}

		public void SelectZipCode()
		{
			if (View.SelectedDistrictId.HasValue)
			{
				District district = _districtManager.Get(View.SelectedDistrictId.Value);
				View.ZipCode = district.ZipCode;
			}
		}
	}
}
