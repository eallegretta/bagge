<%@ Page Title="" Language="C#" MasterPageFile="~/List.Master" AutoEventWireup="true" CodeBehind="ProviderList.aspx.cs" Inherits="Bagge.Seti.WebSite.ProviderList" %>
<%@ Register TagPrefix="seti" TagName="ListCommands" Src="~/Controls/ListCommands.ascx" %>
<asp:Content ID="_filters" runat="server" ContentPlaceHolderID="_filters">
	<table cellpadding="0" cellspacing="0">
		<tr>
			<th><asp:Literal ID="_nameLiteral" runat="server" meta:resourcekey="FilterNameLiteral"></asp:Literal></th>
			<td><asp:TextBox ID="_name" runat="server"></asp:TextBox></td>
			<th><asp:Literal ID="_cuitLiteral" runat="server" meta:resourcekey="FilterCuitLiteral"></asp:Literal></th>
			<td><asp:TextBox ID="_cuit" runat="server"></asp:TextBox></td>
			<th><asp:Literal ID="_productLiteral" runat="server" meta:resourcekey="FilterProductLiteral"></asp:Literal></th>
			<td><asp:DropDownList ID="_products" runat="server" DataValueField="Id" DataTextField="Name" AppendDataBoundItems="true">
			<asp:ListItem></asp:ListItem>
			</asp:DropDownList></td>
		</tr>
		<tr>
			<th><asp:Literal ID="_addressLiteral" runat="server" meta:resourcekey="FilterAddressLiteral"></asp:Literal></th>
			<td><asp:TextBox ID="_address" runat="server"></asp:TextBox></td>
			<th><asp:Literal ID="_phoneLiteral" runat="server" meta:resourcekey="FilterPhoneLiteral"></asp:Literal></th>
			<td><asp:TextBox ID="_phone" runat="server"></asp:TextBox></td>
			<td colspan="2"><asp:Button ID="_filter" runat="server" meta:resourcekey="FilterButton" />
			</td>
		</tr>
	</table>
</asp:Content>

<asp:Content ID="_content" ContentPlaceHolderID="_content" runat="server">
	<seti:SecureGridView ID="_providers" runat="server" DataKeyNames="Id" 
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
					<%#((Bagge.Seti.BusinessEntities.Provider)(Container.DataItem)).District.CountryState%>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:HyperLinkField ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center" DataNavigateUrlFields="Id" 
				DataNavigateUrlFormatString="ProviderEditor.aspx?Id={0}&Action=View"
				Text="<%$ Resources:WebSite, IconViewImageTag %>"
				meta:resourcekey="ViewField" />
			<asp:HyperLinkField ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center" DataNavigateUrlFields="Id" 
				DataNavigateUrlFormatString="ProviderEditor.aspx?Id={0}&Action=Edit"
				Text="<%$ Resources:WebSite, IconEditImageTag %>"
				meta:resourcekey="EditField" />
			<eaa:DeleteCommandField ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center" ImageUrl="<%$ Resources:WebSite, IconDeleteImagePath %>" ButtonType="Image" 
				meta:resourcekey="DeleteField"></eaa:DeleteCommandField>
		</Columns>
	</seti:SecureGridView>
	<seti:ListCommands ID="_new" runat="server" meta:resourceKey="ListCommands" PostBackUrl="~/CustomerEditor.aspx" />
	<asp:ObjectContainerDataSource id="_dataSource" runat="server"></asp:ObjectContainerDataSource>
</asp:Content>

