<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Bagge.Seti.WebSite.Error" meta:resourcekey="Page" %>
<asp:Content ID="_content" ContentPlaceHolderID="_content" runat="server">
	<div id="errorPage">
		<asp:Image ID="_errorIcon" runat="server" SkinID="IconError" />
		<div id="errorPanel">
			<div id="errorMessage"><asp:Label id="_errorMessage" runat="server" meta:resourcekey="ErrorMessage"></asp:Label></div>
			<div id="errorStackTrace"><asp:Label ID="_error" SkinID="_error" runat="server"></asp:Label></div>
			<div id="errorDescription"><asp:Label ID="_errorDescription" SkinID="_errorDescription" runat="server"></asp:Label></div>
		</div>	
	</div>
</asp:Content>
