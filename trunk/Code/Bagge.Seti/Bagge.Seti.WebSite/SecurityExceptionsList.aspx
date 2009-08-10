<%@ Page Title="" Language="C#" MasterPageFile="~/List.Master" AutoEventWireup="true" CodeBehind="SecurityExceptionsList.aspx.cs" Inherits="Bagge.Seti.WebSite.SecurityExceptionsList" %>
<%@ Register TagPrefix="seti" TagName="ListCommands" Src="~/Controls/ListCommands.ascx" %>
<asp:Content ID="_content" ContentPlaceHolderID="_content" runat="server">
	<asp:Literal ID="_roleLiteral" runat="server" meta:resourcekey="RoleLiteral"></asp:Literal>
	<asp:DropDownList ID="_role" runat="server" DataValueField="Id" DataTextField="Name" AppendDataBoundItems="true" AutoPostBack="true">
		<asp:ListItem Value="" meta:resourcekey="SelectRoleListItem"></asp:ListItem>
	</asp:DropDownList>
	<asp:PlaceHolder ID="_functionHolder" runat="server" Visible="false">
		<asp:Literal ID="_functionLiteral" runat="server" meta:resourcekey="FunctionLiteral"></asp:Literal>
		<asp:DropDownList ID="_function" runat="server" DataValueField="Id" DataTextField="Name" AppendDataBoundItems="true" AutoPostBack="true"></asp:DropDownList>
	</asp:PlaceHolder>

	<seti:SecureGridView ID="_securityExceptions" runat="server" DataKeyNames="Id" DataSourceID="_dataSource" meta:resourcekey="Grid">
		<Columns>
			<asp:TemplateField meta:resourcekey="RoleField">
				<ItemTemplate>
					<%#((Bagge.Seti.WebSite.Model.SecurityExceptionModel)Container.DataItem).SecurityException.Role%>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:BoundField DataField="HumanReadableClassName" meta:resourcekey="ClassField" />
			<asp:BoundField DataField="HumanReadableMemberName" meta:resourcekey="MemberField" />
			<asp:TemplateField meta:resourcekey="AccessibilityField">
				<ItemTemplate>
					<%#((Bagge.Seti.WebSite.Model.SecurityExceptionModel)Container.DataItem).SecurityException.Accessibility%>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField meta:resourcekey="ConstraintTypeField">
				<ItemTemplate>
					<%#Bagge.Seti.BusinessEntities.Security.Constraints.Constraint.GetConstraintName(((Bagge.Seti.WebSite.Model.SecurityExceptionModel)Container.DataItem).SecurityException.ConstraintType)%>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField meta:resourcekey="ConstraintValueField">
				<ItemTemplate>
					<%#((Bagge.Seti.WebSite.Model.SecurityExceptionModel)Container.DataItem).SecurityException.Value%>
				</ItemTemplate>
			</asp:TemplateField>
			
			<seti:DeleteUndeleteCommandField MethodName="Delete" DeleteDataField="Deleted" ItemStyle-Width="20px" 
				ItemStyle-HorizontalAlign="Center" 
				ShowUndelete="false"
				UndeleteImageUrl="<%$ Resources:WebSite, IconRecycleImagePath%>"
				ImageUrl="<%$ Resources:WebSite, IconDeleteImagePath %>" ButtonType="Image" 
				meta:resourcekey="DeleteField"></seti:DeleteUndeleteCommandField>
		</Columns>
	</seti:SecureGridView>
	<seti:ListCommands ID="_new" runat="server" meta:resourceKey="ListCommands" PostBackUrl="~/SecurityExceptionsEditor.aspx" />
	<asp:ObjectContainerDataSource id="_dataSource" runat="server"></asp:ObjectContainerDataSource>
</asp:Content>
