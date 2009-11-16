<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Report.Master" AutoEventWireup="true" CodeBehind="TechniciansByTicket.aspx.cs" Inherits="Bagge.Seti.WebSite.Reports.TechniciansByTicket" meta:resourcekey="Page" %>
<%@ Register Src="~/Controls/Calendar.ascx" TagPrefix="seti" TagName="Calendar"  %>
<asp:Content ID="_filters" ContentPlaceHolderID="_filters" runat="server">
	<table>
		<tr>
			<th><asp:Literal id="_dateFromLiteral" runat="server" meta:resourcekey="FilterDateFromLiteral"></asp:Literal></th>
			<td><seti:Calendar ID="_dateFrom" runat="server" InvalidDateErrorMessage="<%$Resources:WebSite, InvalidDateErrorMessage%>" /></td>
			<th><asp:Literal id="_dateToLiteral" runat="server" meta:resourcekey="FilterDateToLiteral" ></asp:Literal></th>
			<td colspan="4"><seti:Calendar ID="_dateTo" runat="server" CompareToCalendarId="_dateFrom" CompareOperator="GreaterThanEqual" InvalidDateErrorMessage="<%$Resources:WebSite, InvalidDateErrorMessage%>" CompareErrorMessage="<%$ Resources:WebSite, DateToMustBeGreaterThanOrEqualToDateFromErrorMessage%>" /></td>
		</tr>
		<tr>
			<th><asp:Literal ID="_technicianFullnameLiteral" runat="server" meta:resourcekey="TechnicianFullnameLiteral"></asp:Literal></th>
			<td colspan="4"><asp:TextBox ID="_technicianFullname" runat="server"></asp:TextBox></td>
		</tr>
		<tr>
			<th><asp:Literal ID="_ticketCountFromLiteral" runat="server" meta:resourcekey="TicketCountFromLiteral"></asp:Literal></th>
			<td><asp:TextBox ID="_ticketCountFrom" runat="server" EnableTheming="false" CssClass="numericMask textBox numeric smallMediumData"></asp:TextBox></td>
			<th><asp:Literal ID="_ticketCountToLiteral" runat="server" meta:resourcekey="TicketCountToLiteral"></asp:Literal></th>
			<td colspan="2"><asp:TextBox ID="_ticketCountTo" runat="server" EnableTheming="false" CssClass="numericMask textBox numeric smallMediumData"></asp:TextBox>
			<asp:CompareValidator id="_ticketCountCmpVal" runat="server" meta:resourcekey="TicketCountCmpValidator" Type="Integer" Display="Static" ControlToValidate="_ticketCountTo" ControlToCompare="_ticketCountFrom" Operator="GreaterThanEqual"></asp:CompareValidator>
			</td>
		</tr>
		<tr>
			<th><asp:Literal ID="_totalRealDurationFromLiteral" runat="server" meta:resourcekey="TotalRealDurationFromLiteral"></asp:Literal></th>
			<td><asp:TextBox ID="_totalRealDurationFrom" runat="server" EnableTheming="false" CssClass="numericMask textBox numeric smallMediumData"></asp:TextBox></td>
			<th><asp:Literal ID="_totalRealDurationToLiteral" runat="server" meta:resourcekey="TotalRealDurationToLiteral"></asp:Literal></th>
			<td><asp:TextBox ID="_totalRealDurationTo" runat="server" EnableTheming="false" CssClass="numericMask textBox numeric smallMediumData"></asp:TextBox>
			<asp:CompareValidator id="_totalRealDurationCmpVal" runat="server" meta:resourcekey="TotalRealDurationCmpValidator" Type="Double" Display="Static" ControlToValidate="_totalRealDurationTo" ControlToCompare="_totalRealDurationFrom" Operator="GreaterThanEqual"></asp:CompareValidator>
			</td>
			<td colspan="2"><asp:Button ID="_filter" runat="server" meta:resourcekey="FilterButton" /></td>
		</tr>
	</table>
		<ajax:MaskedEditExtender ID="_ticketCountFromMask" runat="server" TargetControlID="_ticketCountFrom" Mask="9999999" MaskType="Number" InputDirection="RightToLeft"></ajax:MaskedEditExtender>
	<ajax:MaskedEditExtender ID="_ticketCountToMask" runat="server" TargetControlID="_ticketCountTo" Mask="9999999" MaskType="Number" InputDirection="RightToLeft"></ajax:MaskedEditExtender>
	<ajax:MaskedEditExtender ID="_totalRealDurationFromMask" runat="server" TargetControlID="_totalRealDurationFrom" Mask="9999999.99" MaskType="Number" InputDirection="RightToLeft"></ajax:MaskedEditExtender>
	<ajax:MaskedEditExtender ID="_totalRealDurationToMask" runat="server" TargetControlID="_totalRealDurationTo" Mask="9999999.99" MaskType="Number" InputDirection="RightToLeft"></ajax:MaskedEditExtender>

</asp:Content>
