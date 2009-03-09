using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bagge.Seti.BusinessEntities;
using Bagge.Seti.WebSite.Views;
using Bagge.Seti.BusinessLogic.Contracts;

namespace Bagge.Seti.WebSite.Presenters
{
	public class CustomerEditorPresenter: EditorPresenter<Customer, int>
	{

		IManager<CountryState, int> _countryStateManager;
		IManager<District, int> _districtManager;

		public CustomerEditorPresenter(ICustomerEditorView view, ICustomerManager manager, IManager<CountryState, int> countryStateManager, IManager<District, int> districtManager): base(view, manager)
		{
			_countryStateManager = countryStateManager;
			_districtManager = districtManager;
		}

		protected new ICustomerEditorView View
		{
			get { return (ICustomerEditorView)base.View; }
		}

		protected override void OnLoad(object sender, EventArgs e)
		{
			base.OnLoad(sender, e);

			if (!View.IsPostBack)
			{
				CountryState[] states = _countryStateManager.FindAllOrdered("Name");
				
				View.CountryStates = states;
				if (states.Length > 1)
				{
					View.Districts = states[0].Districts.ToArray();
				}
				
			}
		}

		public override void Save(Customer entity)
		{
			entity.District = _districtManager.Get(View.SelectedDistrictId);

			base.Save(entity);
		}

		public override void Select()
		{
			base.Select();


			if (!View.IsPostBack)
			{
				View.CountryStates = _countryStateManager.FindAllOrdered("Name");

				if (View.Mode == EditorAction.Update)
				{
					CountryState state = SelectedEntity.District.CountryState;
					View.SelectedCountryId = state.Id;
					View.Districts = state.Districts.ToArray();
					View.SelectedDistrictId = SelectedEntity.District.Id;
				}
			}
		}

		public void SelectDistricts(int countryStateId)
		{
			CountryState state = _countryStateManager.Get(countryStateId);
			View.Districts = state.Districts.ToArray();
		}
	}
}
