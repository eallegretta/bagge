<%@ Page Language="C#" MasterPageFile="~/Reports/Report.Master" AutoEventWireup="true" CodeBehind="TicketHistory.aspx.cs" Inherits="Bagge.Seti.WebSite.Reports.TicketHistory" %>
<%@ Register Src="~/Controls/Calendar.ascx" TagPrefix="seti" TagName="Calendar"  %>
<asp:Content ID="_filters" ContentPlaceHolderID="_filters" runat="server">
	<table>
		<tr>
			<th><asp:Literal id="_dateFromLiteral" runat="server" meta:resourcekey="FilterDateFromLiteral"></asp:Literal></th>
			<td><seti:Calendar ID="_dateFrom" runat="server" InvalidDateErrorMessage="<%$Resources:WebSite, InvalidDateErrorMessage%>" /></td>
			<th><asp:Literal id="_dateToLiteral" runat="server" meta:resourcekey="FilterDateToLiteral" ></asp:Literal></th>
			<td colspan="10"><seti:Calendar ID="_dateTo" runat="server" CompareToCalendarId="_dateFrom" CompareOperator="GreaterThanEqual" InvalidDateErrorMessage="<%$Resources:WebSite, InvalidDateErrorMessage%>" CompareErrorMessage="<%$ Resources:WebSite, DateToMustBeGreaterThanOrEqualToDateFromErrorMessage%>" /></td>
		</tr>
		<tr>
			<th><asp:Literal ID="_ticketIdLiteral" runat="server" meta:resourcekey="TicketIdLiteral"></asp:Literal></th>
			<td><asp:TextBox ID="_ticketId" runat="server" EnableTheming="false" CssClass="numericMask textBox numeric smallData"></asp:TextBox></td>
			<th><asp:Literal ID="_statusLiteral" runat="server" meta:resourcekey="StatusLiteral"></asp:Literal></th>
			<td>
			<asp:DropDownList ID="_status" runat="server" DataTextField="Name" DataValueField="Id" AppendDataBoundItems="true">
				<asp:ListItem></asp:ListItem>
			</asp:DropDownList>
			&nbsp;&nbsp;&nbsp;<asp:Button ID="_filter" runat="server" meta:resourcekey="FilterButton" /></td>
		</tr>
	</table>
	<ajax:MaskedEditExtender ID="_ticketIdMask" runat="server" TargetControlID="_ticketId" InputDirection="RightToLeft" Mask="999999" MaskType="Number"></ajax:MaskedEditExtender>
</asp:Content>
