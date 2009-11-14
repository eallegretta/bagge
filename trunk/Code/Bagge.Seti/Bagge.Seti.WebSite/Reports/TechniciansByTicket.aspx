<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Report.Master" AutoEventWireup="true" CodeBehind="TechniciansByTicket.aspx.cs" Inherits="Bagge.Seti.WebSite.Reports.TechniciansByTicket" meta:resourcekey="Page" %>
<%@ Register Src="~/Controls/Calendar.ascx" TagPrefix="seti" TagName="Calendar"  %>
<asp:Content ID="_filters" ContentPlaceHolderID="_filters" runat="server">
	<table>
		<tr>
			<th><asp:Literal id="_dateFromLiteral" runat="server" meta:resourcekey="FilterDateFromLiteral"></asp:Literal></th>
			<td><seti:Calendar ID="_dateFrom" runat="server" InvalidDateErrorMessage="<%$Resources:WebSite, InvalidDateErrorMessage%>" /></td>
			<th><asp:Literal id="_dateToLiteral" runat="server" meta:resourcekey="FilterDateToLiteral" ></asp:Literal></th>
			<td><seti:Calendar ID="_dateTo" runat="server" CompareToCalendarId="_dateFrom" CompareOperator="GreaterThanEqual" InvalidDateErrorMessage="<%$Resources:WebSite, InvalidDateErrorMessage%>" CompareErrorMessage="<%$ Resources:WebSite, DateToMustBeGreaterThanOrEqualToDateFromErrorMessage%>" /></td>
			<td><asp:Button ID="_filter" runat="server" meta:resourcekey="FilterButton" /></td>
		</tr>
	</table>
</asp:Content>
