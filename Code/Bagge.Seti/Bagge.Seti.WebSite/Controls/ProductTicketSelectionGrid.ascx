<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductTicketSelectionGrid.ascx.cs" Inherits="Bagge.Seti.WebSite.Controls.ProductTicketSelectionGrid" %>
<asp:PlaceHolder ID="_addControls" runat="server">
	<table>
		<tr>
			<th>
				<asp:Literal ID="_nameTitle" runat="server" meta:resourcekey="NameTitleLiteral"></asp:Literal>
			</th>
			<th>
				<asp:Literal ID="_quantityTitle" runat="server" meta:resourcekey="QuantityTitleLiteral"></asp:Literal>
			</th>
			<td rowspan="2" valign="bottom" style="padding-bottom: 4px">
				<input id="_add" class="button" type="button" runat="server" meta:resourcekey="AddButton" />
			</td>
		</tr>
		<tr>
			<td>
				<asp:DropDownList ID="_name" runat="server" AppendDataBoundItems="true" Width="250px" meta:resourcekey="NameDropDownList">
					<asp:ListItem></asp:ListItem>
				</asp:DropDownList>
			</td>
			<td>
				<asp:TextBox ID="_quantity" runat="server" meta:resourcekey="QuantityTextBox"></asp:TextBox>
			</td>
		</tr>
	</table>
</asp:PlaceHolder>
<table id="_items" width="100%" runat="server">
	<tr class="gridHeader">
		<th>
			<asp:Literal ID="_itemNameHeaderTitle" runat="server" meta:resourcekey="ItemNameHeaderLiteral"></asp:Literal>
		</th>
		<th style="width:200px">
			<asp:Literal ID="_quantityHeaderTitle" runat="server" meta:resourcekey="QuantityHeaderLiteral"></asp:Literal>
		</th>
		<th style="width:200px">
			<asp:Literal ID="_priceHeaderTitle" runat="server" meta:resourcekey="PriceHeaderLiteral"></asp:Literal>
		</th>
		<th style="width:20px">
			<asp:Literal id="_deleteHeaderTitle" runat="server" meta:resourcekey="DeleteHeaderLiteral"></asp:Literal>
		</th>
	</tr>
	<tr class="gridFooter">
		<td><asp:Literal ID="_totalText" runat="server" meta:resourcekey="TotalTextLiteral"></asp:Literal></td>
		<td style="text-align:right"><span id="totalQuantity">0</span></td>
		<td style="text-align:right"><span id="totalValue">$0</span></td>
		<td></td>
	</tr>	
</table>

<asp:DropDownList ID="_calculateOptions" runat="server" meta:resourcekey="CalculateOptionsDropDownList">
	<asp:ListItem Value="max" meta:resourcekey="CalculateOptionsMaxListItem"></asp:ListItem>
	<asp:ListItem Value="min" meta:resourcekey="CalculateOptionsMinListItem"></asp:ListItem>
	<asp:ListItem Value="avg" meta:resourcekey="CalculateOptionsAvgListItem"></asp:ListItem>
</asp:DropDownList>
<input type="button" id="_calculate" class="button" runat="server" meta:resourcekey="CalculateButton" />
<asp:HiddenField ID="_totalQuantity" runat="server" />
<asp:HiddenField ID="_totalPrice" runat="server" />
<asp:HiddenField ID="_selectedItems" runat="server" Value="[]" />