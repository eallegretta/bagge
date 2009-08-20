<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Report.Master" AutoEventWireup="true" CodeBehind="ProductsConsumed.aspx.cs" Inherits="Bagge.Seti.WebSite.Reports.ProductsConsumed" meta:resourcekey="Page" %>
<%@ Register Src="~/Controls/Calendar.ascx" TagPrefix="seti" TagName="Calendar"  %>
<asp:Content ID="_filters" ContentPlaceHolderID="_filters" runat="server">
	<asp:Literal id="_dateFromLiteral" runat="server" meta:resourcekey="FilterDateFromLiteral"></asp:Literal>	
	<seti:Calendar ID="_dateFrom" runat="server" CompareToCalendarId="_dateTo" CompareOperator="LessThanEqual" InvalidDateErrorMessage="<%$Resources:WebSite, InvalidDateErrorMessage%>" CompareErrorMessage="<%$ Resources:WebSite, DateFromMustBeLowerThanOrEqualToDateToErrorMessage%>" />
	&nbsp;&nbsp;&nbsp;
	<asp:Literal id="_dateToLiteral" runat="server" meta:resourcekey="FilterDateToLiteral"></asp:Literal>
	<seti:Calendar ID="_dateTo" runat="server" CompareToCalendarId="_dateFrom" CompareOperator="GreaterThanEqual" InvalidDateErrorMessage="<%$Resources:WebSite, InvalidDateErrorMessage%>" CompareErrorMessage="<%$ Resources:WebSite, DateToMustBeGreaterThanOrEqualToDateFromErrorMessage%>" />
	&nbsp;&nbsp;&nbsp;
	<asp:Button ID="_filter" runat="server" meta:resourcekey="FilterButton" />
</asp:Content>
