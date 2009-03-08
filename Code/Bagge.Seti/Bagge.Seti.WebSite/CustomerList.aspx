<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomerList.aspx.cs" Inherits="Bagge.Seti.WebSite.CustomerList" meta:resourcekey="Page"%>
<%@ Register TagPrefix="seti" TagName="ListCommands" Src="~/Controls/ListCommands.ascx" %>
<asp:Content ID="_content" ContentPlaceHolderID="_content" runat="server">
	<seti:SecureGridView ID="_customers" runat="server" DataKeyNames="Id" 
		DataSourceID="_dataSource" PageSize="20" AllowPaging="True" AllowSorting="True" 
		AutoGenerateColumns="False" meta:resourcekey="Grid">
		<Columns>
			<asp:BoundField DataField="Name" SortExpression="Name" 
				meta:resourcekey="NameField" />
			<asp:BoundField DataField="CUIT" SortExpression="CUIT" 
				meta:resourcekey="CUITField" />
			<asp:HyperLinkField DataNavigateUrlFields="Id" 
				DataNavigateUrlFormatString="CustomerEditor.aspx?Id={0}&Action=View"
				meta:resourcekey="ViewField" />
			<asp:HyperLinkField DataNavigateUrlFields="Id" 
				DataNavigateUrlFormatString="CustomerEditor.aspx?Id={0}&Action=Edit" 
				meta:resourcekey="EditField" />
			<asp:DeleteCommandField 
				meta:resourcekey="DeleteField"></asp:DeleteCommandField>
		</Columns>
	</seti:SecureGridView>
	<seti:ListCommands ID="_new" runat="server" meta:resourceKey="ListCommands" PostBackUrl="~/CustomerEditor.aspx" />
	<asp:ObjectContainerDataSource id="_dataSource" runat="server" 
		DataObjectTypeName=""></asp:ObjectContainerDataSource>
</asp:Content>
