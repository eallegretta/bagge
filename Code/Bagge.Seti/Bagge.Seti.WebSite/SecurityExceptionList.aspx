<%@ Page Title="" Language="C#" MasterPageFile="~/List.Master" AutoEventWireup="true" CodeBehind="SecurityExceptionList.aspx.cs" Inherits="Bagge.Seti.WebSite.SecurityExceptionsList" meta:resourcekey="Page" %>
<%@ Register TagPrefix="seti" TagName="ListCommands" Src="~/Controls/ListCommands.ascx" %>
<asp:Content ID="_content" ContentPlaceHolderID="_content" runat="server">
	<table cellpadding="5" cellspacing="3">
		<tr>
			<th><asp:Literal ID="_roleLiteral" runat="server" meta:resourcekey="RoleLiteral"></asp:Literal></th>
			<td><asp:DropDownList ID="_role" runat="server" DataValueField="Id" DataTextField="Name" AppendDataBoundItems="true" AutoPostBack="true">
					<asp:ListItem Value="" meta:resourcekey="SelectRoleListItem"></asp:ListItem>
				</asp:DropDownList></td>
			<asp:PlaceHolder ID="_functionHolder" runat="server" Visible="false">
			<th><asp:Literal ID="_functionLiteral" runat="server" meta:resourcekey="FunctionLiteral"></asp:Literal></th>
			<td><asp:DropDownList ID="_function" runat="server" DataValueField="Id" DataTextField="Name" AppendDataBoundItems="true" AutoPostBack="true">
					<asp:ListItem Value="" meta:resourcekey="SelectFunctionListItem"></asp:ListItem>
				</asp:DropDownList></td>
			</asp:PlaceHolder>
		</tr>
	</table>
	<asp:GridView ID="_securityExceptions" runat="server" SkinID="NoPaging" DataKeyNames="Id" meta:resourcekey="Grid">
		<Columns>
			<asp:BoundField DataField="HumanReadableClassName" meta:resourcekey="ClassNameField" />
			<asp:BoundField DataField="HumanReadablePropertyName" meta:resourcekey="PropertyField" />
			<asp:BoundField DataField="Constraint" meta:resourcekey="ConstraintField" />
			<asp:BoundField DataField="Value" meta:resourcekey="ValueField" />
			<seti:SecureHyperLinkField MethodName="Get" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center" DataNavigateUrlFields="Id" 
				DataNavigateUrlFormatString="SecurityExceptionEditor.aspx?Id={0}&Action=View"
				Text="<%$ Resources:WebSite, IconViewImageTag %>"
				meta:resourcekey="ViewField" />
			<seti:SecureHyperLinkField MethodName="Update" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center" DataNavigateUrlFields="Id" 
				DataNavigateUrlFormatString="SecurityExceptionEditor.aspx?Id={0}&Action=Edit"
				Text="<%$ Resources:WebSite, IconEditImageTag %>"
				meta:resourcekey="EditField" />
			<seti:DeleteUndeleteCommandField MethodName="Delete" DeleteDataField="Deleted" ItemStyle-Width="20px" 
				ItemStyle-HorizontalAlign="Center" 
				ShowUndelete="false"
				UndeleteImageUrl="<%$ Resources:WebSite, IconRecycleImagePath%>"
				ImageUrl="<%$ Resources:WebSite, IconDeleteImagePath %>" ButtonType="Image" 
				meta:resourcekey="DeleteField"></seti:DeleteUndeleteCommandField>
		</Columns>
	</asp:GridView>
	<seti:ListCommands ID="_new" runat="server" meta:resourceKey="ListCommands" PostBackUrl="~/SecurityExceptionEditor.aspx" />
</asp:Content>
