<%@ Page Title="" Language="C#" MasterPageFile="~/Editor.Master" AutoEventWireup="true" CodeBehind="SecurityExceptionEditor.aspx.cs" Inherits="Bagge.Seti.WebSite.SecurityExceptionEditor" meta:resourcekey="Page" %>
<%@ Register TagPrefix="seti" TagName="Calendar" Src="~/Controls/Calendar.ascx" %>
<%@ Register TagPrefix="seti" TagName="EditorControls" Src="~/Controls/EditorCommands.ascx"  %>
<asp:Content ID="_content" ContentPlaceHolderID="_content" runat="server">
<seti:SecureDetailsView  ID="_details" runat="server" DataKeyNames="Id" DataSourceID="_dataSource" AutoGenerateRows="false">
	<Fields>
		<asp:TemplateField meta:resourcekey="RoleField">
			<InsertItemTemplate>
				<asp:DropDownList ID="_role" runat="server" AppendDataBoundItems="true" AutoPostBack="true" DataTextField="Name" DataValueField="Id">
					<asp:ListItem></asp:ListItem>
				</asp:DropDownList>
				<asp:RequiredFieldValidator ID="_roleVal" runat="server" ControlToValidate="_role" meta:resourcekey="RoleValidator"></asp:RequiredFieldValidator>
			</InsertItemTemplate>
			<EditItemTemplate>
				<%#Eval("Role")%>
				<asp:HiddenField ID="_role" runat="server" />
			</EditItemTemplate>
			<ItemTemplate>
				<%#Eval("Role")%>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField meta:resourcekey="FunctionField">
			<InsertItemTemplate>
				<asp:PlaceHolder ID="_functionPanel" runat="server">
				<asp:DropDownList ID="_function" runat="server" AppendDataBoundItems="true" AutoPostBack="true" DataTextField="Name" DataValueField="Id">
					<asp:ListItem></asp:ListItem>
				</asp:DropDownList>
				<asp:RequiredFieldValidator ID="_functionVal" runat="server" ControlToValidate="_function" meta:resourcekey="FunctionValidator"></asp:RequiredFieldValidator>
				</asp:PlaceHolder>
				<asp:Literal ID="_selectRoleMessage" runat="server" meta:resourcekey="SelectRoleLiteral"></asp:Literal>
			</InsertItemTemplate>
			<EditItemTemplate>
				<asp:HiddenField ID="_function" runat="server" />
				<%#Eval("Function")%>
			</EditItemTemplate>
			<ItemTemplate>
				<%#Eval("Function")%>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField meta:resourcekey="EntityField">
			<InsertItemTemplate>
				<asp:PlaceHolder ID="_entityPanel" runat="server">
				<asp:DropDownList ID="_entity" AppendDataBoundItems="true" runat="server" AutoPostBack="true">
					<asp:ListItem></asp:ListItem>
				</asp:DropDownList>
				<asp:RequiredFieldValidator ID="_entityVal" runat="server" ControlToValidate="_entity" meta:resourcekey="EntityValidator"></asp:RequiredFieldValidator>
				</asp:PlaceHolder>
				<asp:Literal ID="_selectFunctionMessage" runat="server" meta:resourcekey="SelectFunctionLiteral"></asp:Literal>
			</InsertItemTemplate>
			<EditItemTemplate>
				<asp:HiddenField ID="_entity" runat="server" />
				<%#Eval("HumanReadableClassName")%>
			</EditItemTemplate>
			<ItemTemplate>
				<%#Eval("HumanReadableClassName")%>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField meta:resourcekey="PropertyField">
			<InsertItemTemplate>
				<asp:PlaceHolder ID="_propertyPanel" runat="server">
				<asp:DropDownList ID="_property" AppendDataBoundItems="true" runat="server" AutoPostBack="true">
					<asp:ListItem></asp:ListItem>
				</asp:DropDownList>
				<asp:RequiredFieldValidator ID="_propertyVal" runat="server" ControlToValidate="_property" meta:resourcekey="PropertyValidator"></asp:RequiredFieldValidator>
				</asp:PlaceHolder>
				
				<asp:Literal ID="_selectEntityMessage" runat="server" meta:resourcekey="SelectEntityLiteral"></asp:Literal>
			</InsertItemTemplate>
			<EditItemTemplate>
				<asp:HiddenField ID="_property" runat="server" />
				<%#Eval("HumanReadablePropertyName")%>
			</EditItemTemplate>
			<ItemTemplate>
				<%#Eval("HumanReadablePropertyName")%>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField meta:resourcekey="ConstraintField">
			<EditItemTemplate>
				<asp:PlaceHolder ID="_constraintPanel" runat="server">
				<asp:DropDownList ID="_constraint" AppendDataBoundItems="true" runat="server">
					<asp:ListItem></asp:ListItem>
				</asp:DropDownList>
				<asp:RequiredFieldValidator ID="_constraintVal" runat="server" ControlToValidate="_constraint" meta:resourcekey="ConstraintValidator"></asp:RequiredFieldValidator>
				</asp:PlaceHolder>
				
				<asp:Literal ID="_selectPropertyMessage" runat="server" meta:resourcekey="SelectPropertyLiteral"></asp:Literal>
			</EditItemTemplate>
			<ItemTemplate>
				<%#Eval("Constraint")%>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField meta:resourcekey="ValueField">
			<EditItemTemplate>
				<asp:PlaceHolder ID="_valuePanel" runat="server">
				<asp:MultiView ID="_valueView" runat="server">
					<asp:View ID="_valueViewString" runat="server">
						<asp:TextBox ID="_valueString" runat="server"></asp:TextBox>
					</asp:View>
					<asp:View ID="_valueViewNumber" runat="server">
						<asp:TextBox ID="_valueNumber" runat="server"></asp:TextBox>
						<ajax:MaskedEditExtender ID="_valueNumberExt" runat="server" MaskType="Number" TargetControlID="_valueNumber" InputDirection="RightToLeft"></ajax:MaskedEditExtender>
					</asp:View>
					<asp:View ID="_valueViewBoolean" runat="server">
						<asp:CheckBox ID="_valueBoolean" runat="server" />
					</asp:View>
					<asp:View ID="_valueViewCalendar" runat="server">
						<seti:Calendar ID="_valueDate" runat="server" />
					</asp:View>
					<asp:View ID="_valueViewChar" runat="server">
						<asp:TextBox ID="_valueChar" runat="server" MaxLength="1" Width="10"></asp:TextBox>
					</asp:View>
				</asp:MultiView>
				</asp:PlaceHolder>
				<asp:Literal ID="_selectPropertyMessage2" runat="server" meta:resourcekey="SelectPropertyLiteral"></asp:Literal>
			</EditItemTemplate>
			<ItemTemplate>
				<%#Eval("Value")%>
			</ItemTemplate>
		</asp:TemplateField>					
	</Fields>
</seti:SecureDetailsView>
<seti:EditorControls ID="_commands" runat="server" DetailsViewID="_details" AcceptPostBackUrl="~/SecurityExceptionList.aspx"
	CancelPostBackUrl="~/SecurityExceptionList.aspx" meta:resourcekey="EditorCommands">
</seti:EditorControls>
<asp:ObjectContainerDataSource ID="_dataSource" runat="server" 
		DataObjectTypeName=""></asp:ObjectContainerDataSource>
</asp:Content>
