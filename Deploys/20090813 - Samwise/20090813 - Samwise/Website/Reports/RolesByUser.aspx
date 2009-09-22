<%@ Page Language="C#" MasterPageFile="~/Reports/Report.Master" AutoEventWireup="true" CodeBehind="RolesByUser.aspx.cs" Inherits="Bagge.Seti.WebSite.Reports.RolesByUser" meta:resourcekey="Page" %>

<asp:Content ID="_filters" ContentPlaceHolderID="_filters" runat="server">
	<asp:Literal id="_nameLiteral" runat="server" meta:resourcekey="FilterNameLiteral"></asp:Literal>
	
	<asp:TextBox ID="_name" runat="server"></asp:TextBox>
	
	
	<asp:Literal id="_descriptionLiteral" runat="server" meta:resourcekey="FilterDescriptionLiteral"></asp:Literal>
	<asp:TextBox ID="_description" runat="server"></asp:TextBox>
	
	
	<asp:Button ID="_filter" runat="server" meta:resourcekey="FilterButton" />

</asp:Content>
