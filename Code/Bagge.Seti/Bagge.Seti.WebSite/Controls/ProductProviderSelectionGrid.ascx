<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductProviderSelectionGrid.ascx.cs"
	Inherits="Bagge.Seti.WebSite.Controls.ProductProviderSelectionGrid" %>
<asp:PlaceHolder ID="_addControls" runat="server">
	<asp:Label ID="_legendProduct" Font-Italic="true" runat="server" meta:resourcekey="LegendProductLabel"></asp:Label>
	<asp:Label ID="_legendProvider" Font-Italic="true" runat="server" meta:resourcekey="LegendProviderLabel"></asp:Label>
	<table>
		<tr>
			<th>	
				<asp:Literal ID="_productField" runat="server" meta:resourcekey="ProductFieldLiteral"></asp:Literal>
				<asp:Literal ID="_providerField" runat="server" meta:resourcekey="ProviderFieldLiteral"></asp:Literal>
			</th>
			<th>
				<asp:Literal ID="_priceTitle" runat="server" meta:resourcekey="PriceFieldLiteral"></asp:Literal>
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
				<asp:TextBox ID="_price" runat="server" meta:resourcekey="PriceTextBox" EnableTheming="false" CssClass="numericMask numeric textBox" Width="70px"></asp:TextBox>
				<ajax:MaskedEditExtender ID="_priceMask" runat="server" TargetControlID="_price" MaskType="Number" Mask="999999.99" InputDirection="RightToLeft"></ajax:MaskedEditExtender>
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
			<asp:Literal ID="_priceHeaderTitle" runat="server" meta:resourcekey="PriceHeaderLiteral"></asp:Literal>
		</th>
		<th style="width:20px">
			<asp:Literal id="_deleteHeaderTitle" runat="server" meta:resourcekey="DeleteHeaderLiteral"></asp:Literal>
		</th>
	</tr>
</table>
<asp:HiddenField ID="_selectedItems" runat="server" Value="[]" />
<script type="text/javascript">
	<%=Bagge.Seti.WebSite.Helpers.ControlHelper.GetMaskedEditFunction("registerMask", AjaxControlToolkit.MaskedEditInputDirection.RightToLeft, "999999.99", 
			AjaxControlToolkit.MaskedEditType.Number, Page.ClientScript)%>
</script>


