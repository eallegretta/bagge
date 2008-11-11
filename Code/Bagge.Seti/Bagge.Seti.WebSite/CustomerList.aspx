<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomerList.aspx.cs" Inherits="Bagge.Seti.WebSite.CustomerList" %>
<asp:Content ID="_head" ContentPlaceHolderID="_head" runat="server">
</asp:Content>
<asp:Content ID="_content" ContentPlaceHolderID="_content" runat="server">
	<asp:GridView ID="_customers" runat="server" AutoGenerateColumns="false">
		<Columns>
			<asp:BoundField DataField="Name" />
		</Columns>
	</asp:GridView>
</asp:Content>
