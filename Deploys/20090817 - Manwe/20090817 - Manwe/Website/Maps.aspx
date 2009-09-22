<%@ Page Language="C#" EnableTheming="false" AutoEventWireup="true"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<title></title>
	<script type="text/C#" runat="server">
		protected bool ShowDirections
		{
			get { return Request.QueryString["ShowDirections"] == "true"; }
		}	
	</script>
</head>
<body onload="initialize()" onunload="GUnload()" style="background-color:White;">
	<script src="http://maps.google.com/maps?file=api&amp;v=2&amp;key=<%=System.Configuration.ConfigurationManager.AppSettings["GoogleMaps.Key"]%>" type="text/javascript"></script>
	<script type="text/javascript">    
	var map;
	function initialize() {      
		if (GBrowserIsCompatible()) {        
			map = new GMap2(document.getElementById("map"));        
			<%if(ShowDirections){%>
				setDirections("<%=System.Configuration.ConfigurationSettings.AppSettings["GoogleMaps.From"]%>", "<%=Request.QueryString["Destination"]%>");
			<%}else{%>
				setAddress("<%=Request.QueryString["Destination"]%>");
			<%} %>	
			map.setUIToDefault();      
		}    
		
		
	}
	<%if(ShowDirections){%>
	function setDirections(from, to)	{
		if(map){
			var directionsPanel = document.getElementById("directions");
			var directions = new GDirections(map, directionsPanel); 
			var direction = "from: " + from + " to: " + to;
			//var direction = "from: Alsina 156, Avellaneda, Buenos Aires, Argentina to: Av Ingeniero Huergo 1347, Capital Federal, Argentina";
			directions.load(direction);
		}
	}
	<%}else{%>
	var geocoder = new GClientGeocoder();
		
	function setAddress(address) {  
		geocoder.getLatLng(    address,    
			function(point) {      
				if (!point) {        
					alert(address + " not found");      
				} else 
				{        
					map.setCenter(point, 15);        
					var marker = new GMarker(point);        
					map.addOverlay(marker);        
					marker.openInfoWindowHtml(address);      
				}    
			}  );
	}    
	<%}%>
	</script>
	<form>
    <div>
		
		<%if (ShowDirections){%>
		<div id="map" style="width: 500px; height: 475px;float:left;"></div>
		<div id="directions" runat="server" style="width:440px;float:left"></div>
		<%}else{%>
		<div id="map" style="width: 947px; height: 475px;"></div>
		<%} %>
    </div>
    </form>
</body>
</html>
