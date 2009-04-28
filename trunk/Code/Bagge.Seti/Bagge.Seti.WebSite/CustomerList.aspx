<%@ Page Title="" Language="C#" MasterPageFile="~/List.Master" AutoEventWireup="true" CodeBehind="CustomerList.aspx.cs" Inherits="Bagge.Seti.WebSite.CustomerList" meta:resourcekey="Page"%>
<%@ Register TagPrefix="seti" TagName="ListCommands" Src="~/Controls/ListCommands.ascx" %>
<asp:Content ID="_filters" runat="server" ContentPlaceHolderID="_filters">
	<table cellpadding="0" cellspacing="0">
		<tr>
			<seti:SecurePropertyPlaceHolder runat="server" PropertyName="Name">
				<th><asp:Literal ID="_nameLiteral" runat="server" meta:resourcekey="FilterNameLiteral"></asp:Literal></th>
				<td><asp:TextBox ID="_name" runat="server"></asp:TextBox></td>
			</seti:SecurePropertyPlaceHolder>
			<seti:SecurePropertyPlaceHolder runat="server" PropertyName="CUIT">
			<th><asp:Literal ID="_cuitLiteral" runat="server" meta:resourcekey="FilterCuitLiteral"></asp:Literal></th>
			<td><asp:TextBox ID="_cuit" runat="server"></asp:TextBox></td>
			</seti:SecurePropertyPlaceHolder>
			
			<seti:SecurePropertyPlaceHolder runat="server" PropertyName="Deleted">
			<th><asp:Literal ID="_deletedLiteral" runat="server" meta:resourcekey="FilterDeletedLiteral"></asp:Literal></th>
			<td><asp:DropDownList ID="_isDeleted" runat="server">
				<asp:ListItem></asp:ListItem>
			</asp:DropDownList></td>
			</seti:SecurePropertyPlaceHolder>
		</tr>
		<tr>
			<seti:SecurePropertyPlaceHolder runat="server" PropertyName="Address">
			<th><asp:Literal ID="_addressLiteral" runat="server" meta:resourcekey="FilterAddressLiteral"></asp:Literal></th>
			<td><asp:TextBox ID="_address" runat="server"></asp:TextBox></td>
			</seti:SecurePropertyPlaceHolder>
			<seti:SecurePropertyPlaceHolder runat="server" PropertyName="Phone">
			<th><asp:Literal ID="_phoneLiteral" runat="server" meta:resourcekey="FilterPhoneLiteral"></asp:Literal></th>
			<td><asp:TextBox ID="_phone" runat="server"></asp:TextBox></td>
			</seti:SecurePropertyPlaceHolder>
			<td colspan="2"><asp:Button ID="_filter" runat="server" meta:resourcekey="FilterButton" />
			</td>
		</tr>
	</table>
</asp:Content>

<asp:Content ID="_content" ContentPlaceHolderID="_content" runat="server">
	<seti:SecureGridView ID="_customers" runat="server" DataKeyNames="Id" 
		DataSourceID="_dataSource"
		meta:resourcekey="Grid">
		<Columns>
			<seti:SecureBoundField NullDisplayText="" DataField="Name" 
				meta:resourcekey="NameField" />
			<seti:SecureBoundField NullDisplayText="" DataField="CUIT" 
				meta:resourcekey="CUITField" />
			<seti:SecureBoundField NullDisplayText="" DataField="FullAddress" meta:resourcekey="AddressField" />
			<seti:SecureBoundField NullDisplayText="" DataField="District" meta:resourcekey="DistrictField" />
			<seti:SecureTemplateField meta:resourcekey="CountryStateField" >
				<ItemTemplate>
					<%#((Bagge.Seti.BusinessEntities.Customer)(Container.DataItem)).District.CountryState%>
				</ItemTemplate>
			</seti:SecureTemplateField>
			<seti:SecureBoundField NullDisplayText="" DataField="Phone" meta:resourcekey="PhoneField" />
			<seti:SecureHyperLinkField MethodName="Get" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center" DataNavigateUrlFields="Id" 
				DataNavigateUrlFormatString="CustomerEditor.aspx?Id={0}&Action=View"
				Text="<%$ Resources:WebSite, IconViewImageTag %>"
				meta:resourcekey="ViewField" />
			<seti:SecureHyperLinkField MethodName="Update" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center" DataNavigateUrlFields="Id" 
				DataNavigateUrlFormatString="CustomerEditor.aspx?Id={0}&Action=Edit"
				Text="<%$ Resources:WebSite, IconEditImageTag %>"
				meta:resourcekey="EditField" />
			<seti:DeleteUndeleteCommandField MethodName="Delete" DeleteDataField="Deleted" ItemStyle-Width="20px" 
				ItemStyle-HorizontalAlign="Center" 
				UndeleteImageUrl="<%$ Resources:WebSite, IconRecycleImagePath%>"
				ImageUrl="<%$ Resources:WebSite, IconDeleteImagePath %>" ButtonType="Image" 
				meta:resourcekey="DeleteField"></seti:DeleteUndeleteCommandField>
		</Columns>
	</seti:SecureGridView>
	<seti:SecureMethodPlaceHolder runat="server" MethodName="Create">
		<seti:ListCommands ID="_new" runat="server" meta:resourceKey="ListCommands" PostBackUrl="~/CustomerEditor.aspx" />
	</seti:SecureMethodPlaceHolder>
	<asp:ObjectContainerDataSource id="_dataSource" runat="server" 
		DataObjectTypeName=""></asp:ObjectContainerDataSource>
</asp:Content>
