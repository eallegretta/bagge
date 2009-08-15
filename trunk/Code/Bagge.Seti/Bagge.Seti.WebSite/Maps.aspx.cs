using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Subgurim.Controles;

namespace Bagge.Seti.WebSite
{
	public partial class Maps : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string address = Request.QueryString["Destination"];
			var geoCode = _googleMaps.getGeoCodeRequest(address, "AR");
			_googleMaps.addControl(new GControl(GControl.preBuilt.LargeMapControl));
			_googleMaps.setCenter(geoCode.Placemark.coordinates);
			_googleMaps.addGMarker(
				new GMarker(geoCode.Placemark.coordinates,
					new GMarkerOptions(
							new GIcon(
								),
							geoCode.name
						)
					)
				);

			_googleMaps.addInfoWindow(new GInfoWindow(geoCode.Placemark.coordinates, address));
		}
	}
}
