<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Bagge.Seti.WebSite.Error" meta:resourcekey="Page" %>
<asp:Content ID="_content" ContentPlaceHolderID="_content" runat="server">

	<p><asp:Label id="_errorMessage" runat="server" meta:resourcekey="ErrorMessage"></asp:Label> 
	</p>
	<p>
		<asp:Label ID="_error" SkinID="_error" runat="server"></asp:Label>
	</p>
	<p>
		<asp:Label ID="_errorDescription" SkinID="_errorDescription" runat="server"></asp:Label>
	</p>
	
	
</asp:Content>
