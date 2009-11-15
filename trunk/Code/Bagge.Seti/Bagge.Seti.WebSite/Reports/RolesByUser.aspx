<%@ Page Language="C#" MasterPageFile="~/Reports/Report.Master" AutoEventWireup="true" CodeBehind="RolesByUser.aspx.cs" Inherits="Bagge.Seti.WebSite.Reports.RolesByUser" meta:resourcekey="Page" %>

<asp:Content ID="_filters" ContentPlaceHolderID="_filters" runat="server">
	<table>
		<tr>
			<th><asp:Literal id="_nameLiteral" runat="server" meta:resourcekey="FilterNameLiteral"></asp:Literal></th>
			<td><asp:TextBox ID="_name" runat="server"></asp:TextBox></td>
			<th><asp:Literal id="_descriptionLiteral" runat="server" meta:resourcekey="FilterDescriptionLiteral"></asp:Literal></th>
			<td><asp:TextBox ID="_description" runat="server"></asp:TextBox></td>
			<th><asp:Literal id="_userNameLiteral" runat="server" meta:resourcekey="FilterUserNameNameLiteral"></asp:Literal></th>
			<td><asp:TextBox ID="_userName" runat="server"></asp:TextBox></td>
			<td><asp:Button ID="_filter" runat="server" meta:resourcekey="FilterButton" /></td>
		</tr>
	</table>
</asp:Content>
