<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Maps.aspx.cs" Inherits="Bagge.Seti.WebSite.Maps" %>
<%@ Register assembly="GMaps" namespace="Subgurim.Controles" tagprefix="controls" %>  
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
</head>
<body>
	<form id="_form" runat="server">
    <div>
    <controls:GMap ID="_googleMaps" runat="server" GZoom="15" Width="658" Height="430" enableDragging="true"  />  
    </div>
    </form>
</body>
</html>
