<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductProviderSelectionGrid.ascx.cs" Inherits="Bagge.Seti.WebSite.Controls.ProductProviderSelectionGrid" %>
<asp:PlaceHolder ID="_addControls" runat="server">
<asp:Label ID="_legendProduct" Font-Italic="true" runat="server" meta:resourcekey="LegendProductLabel"></asp:Label>
		<asp:Label ID="_legendProvider" Font-Italic="true" runat="server" meta:resourcekey="LegendProviderLabel"></asp:Label>
<table>
	<tr>
		<th><asp:Literal ID="_nameTitle" runat="server" 
				meta:resourcekey="NameTitleLiteral"></asp:Literal></th>
		<th><asp:Literal ID="_priceTitle" runat="server" meta:resourcekey="PriceTitleLiteral"></asp:Literal></th>
		<td rowspan="2" valign="bottom" style="padding-bottom:4px"><asp:Button ID="_add" runat="server" onclick="_add_Click" 
				meta:resourcekey="AddButton" CausesValidation="false"  /></td>
	</tr>
	
	<tr>
		<td><asp:TextBox ID="_name" runat="server" MaxLength="50" Width="250px" 
				meta:resourcekey="NameTextBox"></asp:TextBox></td>
		<td><asp:TextBox ID="_price" runat="server" meta:resourcekey="PriceTextBox"></asp:TextBox></td>
	</tr>
</table>
<ajax:AutoCompleteExtender ID="_nameExt" runat="server" TargetControlID="_name" ServicePath="ProductProviderSelectionGridUIService.asmx"></ajax:AutoCompleteExtender>
</asp:PlaceHolder>
<asp:GridView ID="_items" DataKeyNames="Id" SkinID="NoPaging" runat="server" 
	OnDataBound="_items_DataBound" OnRowCommand="_items_RowCommand" meta:resourcekey="ItemsGridView" >
	<Columns>
		<asp:BoundField DataField="Provider" meta:resourcekey="ProviderField" />
		<asp:BoundField DataField="Product" meta:resourcekey="ProductField" />
		<asp:BoundField DataField="Price" meta:resourcekey="PriceField" />
		<asp:CommandField DeleteImageUrl="~/App_Themes/Default/Images/iconDelete.gif" 
			ShowDeleteButton="true" ButtonType="Image" 
			meta:resourcekey="DeleteField" />
	</Columns>
</asp:GridView>