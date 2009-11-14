<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Report.Master" AutoEventWireup="true" CodeBehind="ProductsByProvider.aspx.cs" Inherits="Bagge.Seti.WebSite.Reports.ProductsByProvider" meta:resourcekey="Page" %>
<asp:Content ID="_filters" runat="server" ContentPlaceHolderID="_filters">
	<table>
		<tr>
			<th><asp:Literal ID="_productNameLiteral" runat="server" meta:resourcekey="ProductNameLiteral"></asp:Literal></th>
			<td><asp:TextBox ID="_productName" runat="server"></asp:TextBox></td>
			<th><asp:Literal ID="_providerNameLiteral" runat="server" meta:resourcekey="ProviderNameLiteral"></asp:Literal></th>
			<td><asp:TextBox ID="_providerName" runat="server"></asp:TextBox></td>
			<td><asp:Button ID="_filter" runat="server" meta:resourcekey="FilterButton" /></td>
		</tr>
	</table>
</asp:Content>