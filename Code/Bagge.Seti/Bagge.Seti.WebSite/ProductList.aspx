<%@ Page Title="" Language="C#" MasterPageFile="~/List.Master" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="Bagge.Seti.WebSite.ProductList" meta:resourcekey="PageResource1" %>
<%@ Register TagPrefix="seti" TagName="ListCommands" Src="~/Controls/ListCommands.ascx" %>
<asp:Content ID="_filters" runat="server" ContentPlaceHolderID="_filters">
	<table cellpadding="0" cellspacing="0">
		<tr>
			<th><asp:Literal ID="_nameLiteral" runat="server" meta:resourcekey="FilterNameLiteral"></asp:Literal></th>
			<td><asp:TextBox ID="_name" runat="server"></asp:TextBox></td>
			<th><asp:Literal ID="_descriptionLiteral" runat="server" meta:resourcekey="FilterDescriptionLiteral"></asp:Literal></th>
			<td><asp:TextBox ID="_description" runat="server"></asp:TextBox></td>
		</tr>
		<tr>
			<th><asp:Literal ID="_deletedLiteral" runat="server" meta:resourcekey="FilterDeletedLiteral"></asp:Literal></th>
			<td><asp:DropDownList ID="_isDeleted" runat="server">
				<asp:ListItem></asp:ListItem>
				</asp:DropDownList></td>
			<th><asp:Literal ID="_providersLiteral" runat="server" meta:resourcekey="FilterProvidersLiteral"></asp:Literal></th>
			<td><asp:DropDownList ID="_providers" runat="server" DataValueField="Id" DataTextField="NameAndCUIT" AppendDataBoundItems="true">
			<asp:ListItem></asp:ListItem>
			</asp:DropDownList></td>
			<td colspan="2"><asp:Button ID="_filter" runat="server" meta:resourcekey="FilterButton" />
			</td>
		</tr>
	</table>
</asp:Content>

<asp:Content ID="_content" ContentPlaceHolderID="_content" runat="server">
	<seti:SecureGridView ID="_products" runat="server" DataKeyNames="Id" 
		DataSourceID="_dataSource"
		meta:resourcekey="Grid">
		<Columns>
			<seti:SecureBoundField DataField="Name" meta:resourcekey="NameField" />
			<seti:SecureBoundField DataField="Description" meta:resourcekey="DescriptionField" />
			<seti:SecureHyperLinkField MethodName="Get" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center" DataNavigateUrlFields="Id" 
				DataNavigateUrlFormatString="ProductEditor.aspx?Id={0}&Action=View"
				Text="<%$ Resources:WebSite, IconViewImageTag %>"
				meta:resourcekey="ViewField" >
				<ItemStyle HorizontalAlign="Center" Width="20px"></ItemStyle>
			</seti:SecureHyperLinkField>
			<seti:SecureHyperLinkField MethodName="Update" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center" DataNavigateUrlFields="Id" 
				DataNavigateUrlFormatString="ProductEditor.aspx?Id={0}&Action=Edit"
				Text="<%$ Resources:WebSite, IconEditImageTag %>"
				meta:resourcekey="EditField" >
				<ItemStyle HorizontalAlign="Center" Width="20px"></ItemStyle>
			</seti:SecureHyperLinkField>
			<seti:DeleteUndeleteCommandField MethodName="Delete" DeleteDataField="Deleted" ItemStyle-Width="20px" 
				ItemStyle-HorizontalAlign="Center" 
				UndeleteImageUrl="<%$ Resources:WebSite, IconRecycleImagePath%>"
				ImageUrl="<%$ Resources:WebSite, IconDeleteImagePath %>" ButtonType="Image" 
				meta:resourcekey="DeleteField"></seti:DeleteUndeleteCommandField>
		</Columns>
	</seti:SecureGridView>
	<seti:ListCommands ID="_new" runat="server" meta:resourceKey="ListCommands" PostBackUrl="~/ProductEditor.aspx" />
	<asp:ObjectContainerDataSource id="_dataSource" runat="server" 
		DataObjectTypeName=""></asp:ObjectContainerDataSource>
</asp:Content>
