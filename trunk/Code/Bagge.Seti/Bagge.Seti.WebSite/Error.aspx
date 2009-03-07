<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Bagge.Seti.WebSite.Error" %>
<asp:Content ID="_content" ContentPlaceHolderID="_content" runat="server">

	<p>Se ha producido un error en la aplicaci&oacute;n
	</p>
	<p>
		<asp:Label ID="_error" SkinID="_error" runat="server"></asp:Label>
	</p>
	<p>
		<asp:Label ID="_errorDescription" SkinID="_errorDescription" runat="server"></asp:Label>
	</p>
	
	
</asp:Content>
