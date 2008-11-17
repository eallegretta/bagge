<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomerList.aspx.cs" Inherits="Bagge.Seti.WebSite.CustomerList" %>
<asp:Content ID="_content" ContentPlaceHolderID="_content" runat="server">
	<asp:GridView ID="_customers" runat="server" DataKeyNames="Id" DataSourceID="_dataSource" PageSize="20" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false">
		<Columns>
			<asp:BoundField DataField="Name" HeaderText="Nombre" SortExpression="Name" />
			<asp:BoundField DataField="CUIT" HeaderText="CUIT" SortExpression="CUIT" />
			<asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="CustomerEditor.aspx?Id={0}&Action=View" Text="Ver" />
			<asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="CustomerEditor.aspx?Id={0}&Action=Edit" Text="Editar" />
			<asp:DeleteCommandField Text="Borrar" ButtonType="Image" ConfirmationMessage="¿Esta seguro que desea borrar el cliente?"></asp:DeleteCommandField>
		</Columns>
	</asp:GridView>
	<asp:Button PostBackUrl="~/CustomerEditor.aspx" ID="_new" runat="server" Text="Nuevo" />
	<asp:ObjectContainerDataSource id="_dataSource" runat="server"></asp:ObjectContainerDataSource>
</asp:Content>
