﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductProviderSelectionGrid.ascx.cs"
	Inherits="Bagge.Seti.WebSite.Controls.ProductProviderSelectionGrid" %>
<asp:PlaceHolder ID="_addControls" runat="server">
	<asp:Label ID="_legendProduct" Font-Italic="true" runat="server" meta:resourcekey="LegendProductLabel"></asp:Label>
	<asp:Label ID="_legendProvider" Font-Italic="true" runat="server" meta:resourcekey="LegendProviderLabel"></asp:Label>
	<table>
		<tr>
			<th>
				<asp:Literal ID="_nameTitle" runat="server" meta:resourcekey="NameTitleLiteral"></asp:Literal>
			</th>
			<th>
				<asp:Literal ID="_priceTitle" runat="server" meta:resourcekey="PriceTitleLiteral"></asp:Literal>
			</th>
			<td rowspan="2" valign="bottom" style="padding-bottom: 4px">
				<asp:Button ID="_add" runat="server" UseSubmitBehavior="false" meta:resourcekey="AddButton"
					CausesValidation="true" ValidationGroup="ProductProviderSelectionGrid" />
			</td>
		</tr>
		<tr>
			<td>
				<asp:DropDownList ID="_name" runat="server" Width="250px" meta:resourcekey="NameDropDownList">
				</asp:DropDownList>
			</td>
			<td>
				<asp:TextBox ID="_price" runat="server" meta:resourcekey="PriceTextBox"></asp:TextBox>
				<asp:RegularExpressionValidator ID="_priceValidator" runat="server" ControlToValidate="_price"
					ValidationExpression="^\d+(?:\.\d*)?$"></asp:RegularExpressionValidator>
			</td>
		</tr>
	</table>
</asp:PlaceHolder>
<table id="_items" runat="server">
	<tr>
		<th>
			<asp:Literal ID="_itemNameHeaderTitle" runat="server" meta:resourcekey="ItemNameHeaderLiteral"></asp:Literal>
		</th>
		<th>
			<asp:Literal ID="_priceHeaderTitle" runat="server" meta:resourcekey="PriceTitleLiteral"></asp:Literal>
		</th>
		<th>
		</th>
	</tr>
</table>
<asp:HiddenField ID="_selectedItems" runat="server" />