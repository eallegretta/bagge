<%@ Page Title="" Language="C#" MasterPageFile="~/List.Master" AutoEventWireup="true" CodeBehind="EmployeeList.aspx.cs" Inherits="Bagge.Seti.WebSite.EmployeeList" meta:resourcekey="Page" %>
<%@ Register TagPrefix="seti" TagName="ListCommands" Src="~/Controls/ListCommands.ascx" %>
<asp:Content ID="_filters" runat="server" ContentPlaceHolderID="_filters">
	<table cellpadding="0" cellspacing="0">
		<tr>
			<seti:SecurePropertyPlaceHolder runat="server" PropertyName="Category">
				<th><asp:Literal ID="_categoryLiteral" runat="server" meta:resourcekey="FilterCategoryLiteral"></asp:Literal></th>
				<td><asp:DropDownList ID="_category" AppendDataBoundItems="true" runat="server" DataValueField="Id" DataTextField="Name">
					<asp:ListItem Value=""></asp:ListItem>
				</asp:DropDownList></td>
			</seti:SecurePropertyPlaceHolder>
			<seti:SecurePropertyPlaceHolder runat="server" PropertyName="Username">
			<th><asp:Literal ID="_usernameLiteral" runat="server" meta:resourcekey="FilteUsernameLiteral"></asp:Literal></th>
			<td><asp:TextBox ID="_username" runat="server"></asp:TextBox></td>
			</seti:SecurePropertyPlaceHolder>
			
			<seti:SecurePropertyPlaceHolder runat="server" PropertyName="Deleted">
			<th><asp:Literal ID="_deletedLiteral" runat="server" meta:resourcekey="FilterDeletedLiteral"></asp:Literal></th>
			<td colspan="2"><asp:DropDownList ID="_isDeleted" runat="server">
				<asp:ListItem></asp:ListItem>
			</asp:DropDownList></td>
			</seti:SecurePropertyPlaceHolder>
		</tr>
		<tr>
			<seti:SecurePropertyPlaceHolder runat="server" PropertyName="FileNumber">
			<th><asp:Literal ID="_fileNumberLiteral" runat="server" meta:resourcekey="FilterFileNumberLiteral"></asp:Literal></th>
			<td><asp:TextBox ID="_fileNumber" runat="server"></asp:TextBox></td>
			</seti:SecurePropertyPlaceHolder>
			<seti:SecurePropertyPlaceHolder runat="server" PropertyName="Firstname">
			<th><asp:Literal ID="_firstnameLiteral" runat="server" meta:resourcekey="FilterFirstnameLiteral"></asp:Literal></th>
			<td><asp:TextBox ID="_firstname" runat="server"></asp:TextBox></td>
			</seti:SecurePropertyPlaceHolder>
			<seti:SecurePropertyPlaceHolder runat="server" PropertyName="Lastname">
			<th><asp:Literal ID="_lastnameLiteral" runat="server" meta:resourcekey="FilterLastnameLiteral"></asp:Literal></th>
			<td><asp:TextBox ID="_lastname" runat="server"></asp:TextBox></td>
			</seti:SecurePropertyPlaceHolder>
			<td><asp:Button ID="_filter" runat="server" meta:resourcekey="FilterButton" />
			</td>
		</tr>
	</table>
</asp:Content>

<asp:Content ID="_content" ContentPlaceHolderID="_content" runat="server">
	<seti:SecureGridView ID="_employees" runat="server" DataKeyNames="Id" 
		DataSourceID="_dataSource"
		meta:resourcekey="Grid">
		<Columns>
			<seti:SecureBoundField NullDisplayText="" DataField="Firstname" 
				meta:resourcekey="FirstnameField" />
			<seti:SecureBoundField NullDisplayText="" DataField="Lastname" 
				meta:resourcekey="LastnameField" />
			<seti:SecureBoundField NullDisplayText="" DataField="Username" 
				meta:resourcekey="UsernameField" />
			<seti:SecureBoundField NullDisplayText="" DataField="Category" meta:resourcekey="CategoryField" />
			<seti:SecureBoundField NullDisplayText="" DataField="FileNumber" meta:resourcekey="FileNumberField" />
			<seti:SecureHyperLinkField MethodName="Get" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center" DataNavigateUrlFields="Id" 
				DataNavigateUrlFormatString="EmployeeEditor.aspx?Id={0}&Action=View"
				Text="<%$ Resources:WebSite, IconViewImageTag %>"
				meta:resourcekey="ViewField" />
			<seti:SecureHyperLinkField MethodName="Update" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center" DataNavigateUrlFields="Id" 
				DataNavigateUrlFormatString="EmployeeEditor.aspx?Id={0}&Action=Edit"
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
		<seti:ListCommands ID="_new" runat="server" meta:resourceKey="ListCommands" PostBackUrl="~/EmployeeEditor.aspx" />
	</seti:SecureMethodPlaceHolder>
	<asp:ObjectContainerDataSource id="_dataSource" runat="server" 
		DataObjectTypeName=""></asp:ObjectContainerDataSource>
</asp:Content>

