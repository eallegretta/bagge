<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomerList.aspx.cs" Inherits="Bagge.Seti.WebSite.CustomerList" %>
<asp:Content ID="_head" ContentPlaceHolderID="_head" runat="server">
</asp:Content>
<asp:Content ID="_content" ContentPlaceHolderID="_content" runat="server">
	<asp:GridView ID="_customers" runat="server" PageSize="2" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false">
		<Columns>
			<asp:BoundField DataField="Name" HeaderText="Nombre" SortExpression="Name" />
			<asp:BoundField DataField="CUIT" HeaderText="CUIT" SortExpression="CUIT" />
		</Columns>
	</asp:GridView>
</asp:Content>
