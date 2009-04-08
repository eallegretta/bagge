<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductSelectionGrid.ascx.cs" Inherits="Bagge.Seti.WebSite.Controls.ProductSelectionGrid" %>
<asp:TextBox ID="_productName" runat="server" MaxLength="50" Width="250px"></asp:TextBox><asp:Button ID="_add" runat="server" />
<asp:GridView ID="_selectedProducts" SkinID="NoPaging" runat="server">
	<Columns>
		<asp:BoundField DataField="Name" meta:resourcekey="NameField" />
		<asp:CommandField DeleteImageUrl="~/App_Themes/Default/Images/iconDelete.gif" ShowDeleteButton="true" ButtonType="Image" />
	</Columns>
</asp:GridView>