<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Bagge.Seti.WebSite.Login" MasterPageFile="~/Site.Master" meta:resourcekey="Page" %>
<asp:Content ID="_content" runat="server" ContentPlaceHolderID="_content">
	<asp:Login ID="_login" runat="server" meta:resourcekey="Login" 
		onauthenticate="_login_Authenticate" onloggedin="_login_LoggedIn">
	</asp:Login>
</asp:Content>