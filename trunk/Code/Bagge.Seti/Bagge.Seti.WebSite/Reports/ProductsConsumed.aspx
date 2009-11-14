<%@ Page Title="" Language="C#" MasterPageFile="~/Reports/Report.Master" AutoEventWireup="true" CodeBehind="ProductsConsumed.aspx.cs" Inherits="Bagge.Seti.WebSite.Reports.ProductsConsumed" meta:resourcekey="Page" %>
<%@ Register Src="~/Controls/Calendar.ascx" TagPrefix="seti" TagName="Calendar"  %>
<asp:Content ID="_filters" ContentPlaceHolderID="_filters" runat="server">
	<table>
	
	<tr>
		<th><asp:Literal id="_dateFromLiteral" runat="server" meta:resourcekey="FilterDateFromLiteral"></asp:Literal>	</th>
		<td><seti:Calendar ID="_dateFrom" runat="server" InvalidDateErrorMessage="<%$Resources:WebSite, InvalidDateErrorMessage%>" /></td>
		<th><asp:Literal id="_dateToLiteral" runat="server" meta:resourcekey="FilterDateToLiteral"></asp:Literal></th>
		<td><seti:Calendar ID="_dateTo" runat="server" CompareToCalendarId="_dateFrom" CompareOperator="GreaterThanEqual" InvalidDateErrorMessage="<%$Resources:WebSite, InvalidDateErrorMessage%>" CompareErrorMessage="<%$ Resources:WebSite, DateToMustBeGreaterThanOrEqualToDateFromErrorMessage%>" /></td>
		<td rowspan="3"><asp:Button ID="_filter" runat="server" meta:resourcekey="FilterButton" /></td>
	</tr>
	<tr>
		<th><asp:Literal ID="_productNameLiteral" runat="server" meta:resourcekey="ProductNameLiteral"></asp:Literal></th>
		<td colspan="3"><asp:TextBox ID="_productName" runat="server"></asp:TextBox></td>
	</tr>
	<tr>
		<th><asp:Literal ID="_estimatedQuantityFromLiteral" runat="server" meta:resourcekey="EstimatedQuantityFromLiteral"></asp:Literal></th>
		<td><asp:TextBox ID="_estimatedQuantityFrom" runat="server" EnableTheming="false" CssClass="numericMask textBox numeric smallMediumData"></asp:TextBox></td>
		<th><asp:Literal ID="_estimatedQuantityToLiteral" runat="server" meta:resourcekey="EstimatedQuantityToLiteral"></asp:Literal></th>
		<td><asp:TextBox ID="_estimatedQuantityTo" runat="server" EnableTheming="false" CssClass="numericMask textBox numeric smallMediumData"></asp:TextBox>
		<asp:CompareValidator id="_estimatedQuantityCmpVal" runat="server" meta:resourcekey="EstimatedQuantityCmpValidator" Type="Integer" Display="Static" ControlToValidate="_estimatedQuantityTo" ControlToCompare="_estimatedQuantityFrom" Operator="GreaterThanEqual"></asp:CompareValidator>
		</td>
	</tr>
	</table>
	<ajax:MaskedEditExtender ID="_estimatedQuantityFromMask" runat="server" TargetControlID="_estimatedQuantityFrom" Mask="9999999" MaskType="Number" InputDirection="RightToLeft"></ajax:MaskedEditExtender>
	<ajax:MaskedEditExtender ID="_estimatedQuantityToMask" runat="server" TargetControlID="_estimatedQuantityTo" Mask="9999999" MaskType="Number" InputDirection="RightToLeft"></ajax:MaskedEditExtender>
</asp:Content>
