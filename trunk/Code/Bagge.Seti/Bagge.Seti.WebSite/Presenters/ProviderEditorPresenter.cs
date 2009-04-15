﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.BusinessLogic.Contracts;
using Bagge.Seti.WebSite.Views;

namespace Bagge.Seti.WebSite.Presenters
{
	public class ProviderEditorPresenter : EditorPresenter<Provider, int>
	{

		IManager<CountryState, int> _countryStateManager;
		IManager<District, int> _districtManager;
		IManager<ProviderCalification, int> _providerCalificationManager;

		public ProviderEditorPresenter(IProviderEditorView view, IProviderManager manager, IManager<ProviderCalification, int> providerCalificationManager, IManager<CountryState, int> countryStateManager, IManager<District, int> districtManager)
			: base(view, manager)
		{
			_countryStateManager = countryStateManager;
			_districtManager = districtManager;
			_providerCalificationManager = providerCalificationManager;
		}

		protected new IProviderEditorView View
		{
			get { return GetView<IProviderEditorView>(); }
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


		private void LoadView(EditorAction mode)
		{
			View.Califications = _providerCalificationManager.FindAllOrdered("Name").ToArray();
			switch (mode)
			{
				case EditorAction.Insert:
					CountryState[] states = _countryStateManager.FindAllOrdered("Name");

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
					View.CountryStates = _countryStateManager.FindAllOrdered("Name");

					CountryState state = SelectedEntity.District.CountryState;
					View.SelectedCountryId = state.Id;
					View.Districts = state.Districts.ToArray();
					View.SelectedDistrictId = SelectedEntity.District.Id;
					View.SelectedCalificationId = SelectedEntity.Calification.Id;

					break;
			}
		}

		public bool IsCuitValid(string cuit)
		{
			if (View.Mode == EditorAction.Insert)
				return GetManager<IProviderManager>().GetByCuit(cuit) == null;
			else
			{
				var customer = GetManager<IProviderManager>().GetByCuit(cuit);
				if (customer == null)
					return true;
				return customer.Id == View.PrimaryKey;
			}
		}

		public override void Save(Provider entity)
		{
			if (View.SelectedDistrictId.HasValue)
				entity.District = _districtManager.Get(View.SelectedDistrictId.Value);

			entity.Calification = _providerCalificationManager.Get(View.SelectedCalificationId);
			entity.Products = View.Products;

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
