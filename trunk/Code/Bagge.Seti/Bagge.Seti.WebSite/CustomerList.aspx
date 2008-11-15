<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomerList.aspx.cs" Inherits="Bagge.Seti.WebSite.CustomerList" %>
<asp:Content ID="_head" ContentPlaceHolderID="_head" runat="server">
</asp:Content>
<asp:Content ID="_content" ContentPlaceHolderID="_content" runat="server">
	<asp:GridView ID="_customers" runat="server" PageSize="10" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false">
		<Columns>
			<asp:BoundField DataField="Name" HeaderText="Nombre" SortExpression="Name" />
			<asp:BoundField DataField="CUIT" HeaderText="CUIT" SortExpression="CUIT" />
			<asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="CustomerEditor.aspx?Id={0}&Action=View" Text="Ver" />
			<asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="CustomerEditor.aspx?Id={0}&Action=Edit" Text="Editar" />
		</Columns>
	</asp:GridView>
	<asp:Button PostBackUrl="~/CustomerEditor.aspx" ID="_new" runat="server" Text="Nuevo" />
</asp:Content>
