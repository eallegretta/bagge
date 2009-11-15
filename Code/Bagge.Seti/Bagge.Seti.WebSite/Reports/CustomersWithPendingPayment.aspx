<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Report.Master" AutoEventWireup="true" CodeBehind="CustomersWithPendingPayment.aspx.cs" Inherits="Bagge.Seti.WebSite.Reports.CustomersWithPendingPayment" meta:resourcekey="Page" %>
<asp:Content ID="_filters" ContentPlaceHolderID="_filters" runat="server">
	<table>
		<tr>
			<td><asp:Literal ID="_customerNameLiteral" runat="server" meta:resourcekey="CustomerNameLiteral"></asp:Literal></td>
			<td><asp:TextBox ID="_customerName" runat="server"></asp:TextBox></td>
			<td><asp:Literal ID="_hasSubscriptionLiteral" runat="server" meta:resourcekey="HasSubscriptionLiteral"></asp:Literal></td>
			<td><asp:DropDownList ID="_hasSubscription" runat="server">
					<asp:ListItem></asp:ListItem>
			</asp:DropDownList></td>

			<td><asp:Button ID="_filter" runat="server" meta:resourcekey="FilterButton" /></td>
		</tr>
				
	</table>
</asp:Content>
