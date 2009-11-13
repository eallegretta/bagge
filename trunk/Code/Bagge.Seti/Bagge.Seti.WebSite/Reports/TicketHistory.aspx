<%@ Page Language="C#" MasterPageFile="~/Reports/Report.Master" AutoEventWireup="true" CodeBehind="TicketHistory.aspx.cs" Inherits="Bagge.Seti.WebSite.Reports.TicketHistory" %>
<%@ Register Src="~/Controls/Calendar.ascx" TagPrefix="seti" TagName="Calendar"  %>
<asp:Content ID="_filters" ContentPlaceHolderID="_filters" runat="server">
	<asp:Literal id="_dateFromLiteral" runat="server" meta:resourcekey="FilterDateFromLiteral"></asp:Literal>
	<seti:Calendar ID="_dateFrom" runat="server" CompareToCalendarId="_dateTo" CompareOperator="LessThanEqual" InvalidDateErrorMessage="<%$Resources:WebSite, InvalidDateErrorMessage%>" CompareErrorMessage="<%$ Resources:WebSite, DateFromMustBeLowerThanOrEqualToDateToErrorMessage%>" />
	&nbsp;&nbsp;&nbsp;
	<asp:Literal id="_dateToLiteral" runat="server" meta:resourcekey="FilterDateToLiteral" ></asp:Literal>
	<seti:Calendar ID="_dateTo" runat="server" CompareToCalendarId="_dateFrom" CompareOperator="GreaterThanEqual" InvalidDateErrorMessage="<%$Resources:WebSite, InvalidDateErrorMessage%>" CompareErrorMessage="<%$ Resources:WebSite, DateToMustBeGreaterThanOrEqualToDateFromErrorMessage%>" />
	&nbsp;&nbsp;&nbsp;
	<asp:Literal ID="_ticketIdLiteral" runat="server" meta:resourcekey="TicketIdLiteral"></asp:Literal>
	<asp:TextBox ID="_ticketId" runat="server" EnableTheming="false" CssClass="numericMask textBox numeric smallData"></asp:TextBox>
	<ajax:MaskedEditExtender ID="_ticketIdMask" runat="server" TargetControlID="_ticketId" InputDirection="RightToLeft" Mask="999999" MaskType="Number"></ajax:MaskedEditExtender>
	&nbsp;&nbsp;&nbsp;
	<asp:Literal ID="_statusLiteral" runat="server" meta:resourcekey="StatusLiteral"></asp:Literal>
	<asp:DropDownList ID="_status" runat="server" DataTextField="Name" DataValueField="Id" AppendDataBoundItems="true">
		<asp:ListItem></asp:ListItem>
	</asp:DropDownList>
	<asp:Button ID="_filter" runat="server" meta:resourcekey="FilterButton" />
</asp:Content>
