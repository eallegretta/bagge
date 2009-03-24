<%@ Page Title="" Language="C#" MasterPageFile="~/List.Master" AutoEventWireup="true" CodeBehind="CustomerList.aspx.cs" Inherits="Bagge.Seti.WebSite.CustomerList" meta:resourcekey="Page"%>
<%@ Register TagPrefix="seti" TagName="ListCommands" Src="~/Controls/ListCommands.ascx" %>
<asp:Content ID="_filters" runat="server" ContentPlaceHolderID="_filters">
	<table cellpadding="0" cellspacing="0">
		<tr>
			<th><asp:Literal ID="_nameLiteral" runat="server" meta:resourcekey="FilterNameLiteral"></asp:Literal></th>
			<td><asp:TextBox ID="_name" runat="server"></asp:TextBox></td>
			<th><asp:Literal ID="_cuitLiteral" runat="server" meta:resourcekey="FilterCuitLiteral"></asp:Literal></th>
			<td><asp:TextBox ID="_cuit" runat="server"></asp:TextBox></td>
			<td><asp:Button ID="_filter" runat="server" meta:resourcekey="FilterButton" />
			</td>
		</tr>
		<tr>
			<th><asp:Literal ID="_addressLiteral" runat="server" meta:resourcekey="FilterAddressLiteral"></asp:Literal></th>
			<td><asp:TextBox ID="_address" runat="server"></asp:TextBox></td>
			<th><asp:Literal ID="_phoneLiteral" runat="server" meta:resourcekey="FilterPhoneLiteral"></asp:Literal></th>
			<td colspan="2"><asp:TextBox ID="_phone" runat="server"></asp:TextBox></td>
		</tr>
	</table>
</asp:Content>

<asp:Content ID="_content" ContentPlaceHolderID="_content" runat="server">
	<seti:SecureGridView ID="_customers" runat="server" DataKeyNames="Id" 
		DataSourceID="_dataSource"
		meta:resourcekey="Grid">
		<Columns>
			<asp:BoundField DataField="Name" 
				meta:resourcekey="NameField" />
			<asp:BoundField DataField="CUIT" 
				meta:resourcekey="CUITField" />
			<asp:BoundField DataField="FullAddress" meta:resourcekey="AddressField" />
			<asp:BoundField DataField="District" meta:resourcekey="DistrictField" />
			<asp:TemplateField meta:resourcekey="CountryStateField" >
				<ItemTemplate>
					<%#((Bagge.Seti.BusinessEntities.Customer)(Container.DataItem)).District.CountryState%>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:HyperLinkField ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center" DataNavigateUrlFields="Id" 
				DataNavigateUrlFormatString="CustomerEditor.aspx?Id={0}&Action=View"
				Text="<%$ Resources:WebSite, IconViewImageTag %>"
				meta:resourcekey="ViewField" />
			<asp:HyperLinkField ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center" DataNavigateUrlFields="Id" 
				DataNavigateUrlFormatString="CustomerEditor.aspx?Id={0}&Action=Edit"
				Text="<%$ Resources:WebSite, IconEditImageTag %>"
				meta:resourcekey="EditField" />
			<eaa:DeleteCommandField ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center" ImageUrl="<%$ Resources:WebSite, IconDeleteImagePath %>" ButtonType="Image" 
				meta:resourcekey="DeleteField"></eaa:DeleteCommandField>
		</Columns>
	</seti:SecureGridView>
	<seti:ListCommands ID="_new" runat="server" meta:resourceKey="ListCommands" PostBackUrl="~/CustomerEditor.aspx" />
	<asp:ObjectContainerDataSource id="_dataSource" runat="server" 
		DataObjectTypeName=""></asp:ObjectContainerDataSource>
</asp:Content>
