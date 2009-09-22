<%@ Page Title="" Language="C#" MasterPageFile="~/List.Master" AutoEventWireup="true" CodeBehind="RoleList.aspx.cs" Inherits="Bagge.Seti.WebSite.RoleList" meta:resourcekey="Page" %>
<%@ Register TagPrefix="seti" TagName="ListCommands" Src="~/Controls/ListCommands.ascx" %>
<asp:Content ID="_filters" runat="server" ContentPlaceHolderID="_filters">
	<table cellpadding="0" cellspacing="0">
		<tr>
			<seti:SecurePropertyPlaceHolder runat="server" PropertyName="Name">
				<th><asp:Literal ID="_nameLiteral" runat="server" meta:resourcekey="FilterNameLiteral"></asp:Literal></th>
				<td><asp:TextBox ID="_name" runat="server"></asp:TextBox></td>
			</seti:SecurePropertyPlaceHolder>
			<seti:SecurePropertyPlaceHolder runat="server" PropertyName="Description">
				<th><asp:Literal ID="_descriptionLiteral" runat="server" meta:resourcekey="FilterDescriptionLiteral"></asp:Literal></th>
				<td><asp:TextBox ID="_description" runat="server"></asp:TextBox></td>
			</seti:SecurePropertyPlaceHolder>
			<seti:SecurePropertyPlaceHolder runat="server" PropertyName="Deleted">
				<th><asp:Literal ID="_deletedLiteral" runat="server" meta:resourcekey="FilterDeletedLiteral"></asp:Literal></th>
				<td><asp:DropDownList ID="_isDeleted" runat="server">
					<asp:ListItem></asp:ListItem>
				</asp:DropDownList></td>
			</seti:SecurePropertyPlaceHolder>
		</tr>
		<tr>
			<seti:SecurePropertyPlaceHolder runat="server" PropertyName="Employees">
				<th><asp:Literal ID="_employeesLiteral" runat="server" meta:resourcekey="FilterEmployeesLiteral"></asp:Literal></th>
				<td><asp:DropDownList ID="_employees" AppendDataBoundItems="true" DataTextField="Fullname" DataValueField="Id" runat="server">
					<asp:ListItem></asp:ListItem>
				</asp:DropDownList></td>
			</seti:SecurePropertyPlaceHolder>
			
			<seti:SecurePropertyPlaceHolder runat="server" PropertyName="Functions">
				<th><asp:Literal ID="_functionsLiteral" runat="server" meta:resourcekey="FilterFunctionsLiteral"></asp:Literal></th>
				<td><asp:DropDownList ID="_functions" AppendDataBoundItems="true" DataTextField="Name" DataValueField="Id" runat="server">
					<asp:ListItem></asp:ListItem>
				</asp:DropDownList></td>
			</seti:SecurePropertyPlaceHolder>
			
			<td colspan="2"><asp:Button ID="_filter" runat="server" meta:resourcekey="FilterButton" />
			</td>
		</tr>
	</table>
</asp:Content>

<asp:Content ID="_content" ContentPlaceHolderID="_content" runat="server">
	<seti:SecureGridView ID="_roles" runat="server" DataKeyNames="Id" 
		DataSourceID="_dataSource"
		meta:resourcekey="Grid">
		<Columns>
			<seti:SecureBoundField NullDisplayText="" DataField="Name" 
				meta:resourcekey="NameField" />
			<seti:SecureBoundField NullDisplayText="" DataField="Description" 
				meta:resourcekey="DescriptionField" />
			<seti:SecureHyperLinkField MethodName="Get" ItemStyle-Width="20px" 
				ItemStyle-HorizontalAlign="Center" DataNavigateUrlFields="Id" 
				DataNavigateUrlFormatString="RoleEditor.aspx?Id={0}&Action=View"
				Text="<%$ Resources:WebSite, IconViewImageTag %>"
				meta:resourcekey="ViewField" >
<ItemStyle HorizontalAlign="Center" Width="20px"></ItemStyle>
			</seti:SecureHyperLinkField>
			<seti:SecureHyperLinkField MethodName="Update" ItemStyle-Width="20px" 
				ItemStyle-HorizontalAlign="Center" DataNavigateUrlFields="Id" 
				DataNavigateUrlFormatString="RoleEditor.aspx?Id={0}&Action=Edit"
				Text="<%$ Resources:WebSite, IconEditImageTag %>"
				meta:resourcekey="EditField" >
<ItemStyle HorizontalAlign="Center" Width="20px"></ItemStyle>
			</seti:SecureHyperLinkField>
			<seti:DeleteUndeleteCommandField MethodName="Delete" DeleteDataField="Deleted" ItemStyle-Width="20px" 
				ItemStyle-HorizontalAlign="Center" 
				UndeleteImageUrl="<%$ Resources:WebSite, IconRecycleImagePath%>"
				ImageUrl="<%$ Resources:WebSite, IconDeleteImagePath %>" ButtonType="Image" 
				meta:resourcekey="DeleteField">
<ItemStyle HorizontalAlign="Center" Width="20px"></ItemStyle>
			</seti:DeleteUndeleteCommandField>
		</Columns>
	</seti:SecureGridView>
	<seti:SecureMethodPlaceHolder runat="server" MethodName="Create">
		<seti:ListCommands ID="_new" runat="server" meta:resourceKey="ListCommands" PostBackUrl="~/RoleEditor.aspx" />
	</seti:SecureMethodPlaceHolder>
	<asp:ObjectContainerDataSource id="_dataSource" runat="server" 
		DataObjectTypeName=""></asp:ObjectContainerDataSource>
</asp:Content>

