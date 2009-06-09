<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductTicketSelectionGrid.ascx.cs" Inherits="Bagge.Seti.WebSite.Controls.ProductTicketSelectionGrid" %>
<asp:PlaceHolder ID="_addControls" runat="server">
	<table>
		<tr>
			<th>
				<asp:Literal ID="_productTitle" runat="server" meta:resourcekey="ProductTitleLiteral"></asp:Literal>
			</th>
			<th>
				<asp:Literal ID="_providerTitle" runat="server" meta:resourcekey="ProviderTitleLiteral"></asp:Literal>
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
				<asp:DropDownList ID="_product" runat="server" AutoPostBack="true" AppendDataBoundItems="true" Width="250px" meta:resourcekey="ProductDropDownList" OnSelectedIndexChanged="_product_SelectedIndexChanged">
					<asp:ListItem meta:resourcekey="ProductDropDownListFirstItem"></asp:ListItem>
				</asp:DropDownList>
			</td>
			<td>
				<asp:UpdatePanel ID="_providerUpdatePanel" runat="server" UpdateMode="Conditional" RenderMode="Inline">
					<ContentTemplate>
						<asp:DropDownList ID="_provider" runat="server" Width="250px" meta:resourcekey="ProviderDropDownList">
						</asp:DropDownList>
					</ContentTemplate>
					<Triggers>
						<asp:AsyncPostBackTrigger ControlID="_product" EventName="SelectedIndexChanged" />
					</Triggers>
				</asp:UpdatePanel>
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
			<asp:Literal ID="_productHeaderTitle" runat="server" meta:resourcekey="ProductHeaderLiteral"></asp:Literal>
		</th>
		<th>
			<asp:Literal ID="_providerHeaderTitle" runat="server" meta:resourcekey="ProviderHeaderLiteral"></asp:Literal>
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
	
		<td colspan="2"><asp:Literal ID="_totalText" runat="server" meta:resourcekey="TotalTextLiteral"></asp:Literal></td>
		<td style="text-align:right"><span id="totalQuantity">0</span></td>
		<td style="text-align:right">$<span id="totalPrice">0</td>
		<td></td>
	</tr>
</table>

<asp:HiddenField ID="_totalQuantity" runat="server" />
<asp:HiddenField ID="_selectedItems" runat="server" Value="[]" />