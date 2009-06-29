<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Report.Master" AutoEventWireup="true" CodeBehind="CustomersByTicket.aspx.cs" Inherits="Bagge.Seti.WebSite.Reports.CustomersByTicket" meta:resourcekey="Page" %>
<asp:Content ID="_filters" ContentPlaceHolderID="_filters" runat="server">
	<asp:Literal id="_dateFromLiteral" runat="server" meta:resourcekey="FilterDateFromLiteral"></asp:Literal>
	<asp:TextBox ReadOnly="true" ID="_dateFrom" runat="server"></asp:TextBox>
	<ajax:CalendarExtender ID="_dateFromCalendar" runat="server" TargetControlID="_dateFrom"></ajax:CalendarExtender>
	&nbsp;&nbsp;&nbsp;
	
	<asp:Literal id="_dateToLiteral" runat="server" meta:resourcekey="FilterDateToLiteral"></asp:Literal>
	<asp:TextBox ReadOnly="true" ID="_dateTo" runat="server"></asp:TextBox>
	<ajax:CalendarExtender ID="_dateToCalendar" runat="server" TargetControlID="_dateTo"></ajax:CalendarExtender>
	&nbsp;&nbsp;&nbsp;
	
	<asp:Button ID="_filter" runat="server" meta:resourcekey="FilterButton" />
</asp:Content>
