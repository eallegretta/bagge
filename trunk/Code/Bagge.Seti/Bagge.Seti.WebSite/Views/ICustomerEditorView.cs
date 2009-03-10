using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bagge.Seti.BusinessEntities;

namespace Bagge.Seti.WebSite.Views
{
	public interface ICustomerEditorView: IEditorView<int>
	{
		CountryState[] CountryStates { set; }
		District[] Districts { set; }

		int SelectedCountryId { set; }
		int SelectedDistrictId { get; set; }

		string ZipCode { set; }
	}
}
