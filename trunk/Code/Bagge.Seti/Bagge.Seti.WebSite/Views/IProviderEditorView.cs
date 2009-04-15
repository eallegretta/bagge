using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.WebSite.Views
{
	public interface IProviderEditorView: IEditorView<int>
	{
		ProviderCalification[] Califications { set; }
		CountryState[] CountryStates { set; }
		District[] Districts { set; }

		int SelectedCountryId { set; }
		int? SelectedDistrictId { get; set; }
		int SelectedCalificationId { get;  set; }

		string ZipCode { set; }

		ProductProvider[] Products { get; set; }
	}
}
