<%@ Page Title="" Language="C#" MasterPageFile="~/Editor.Master" AutoEventWireup="true" CodeBehind="RoleEditor.aspx.cs" Inherits="Bagge.Seti.WebSite.RoleEditor" meta:resourcekey="Page" %>
<%@ Register Src="~/Controls/EditorCommands.ascx" TagPrefix="seti" TagName="EditorControls" %>
<asp:Content ID="_content" ContentPlaceHolderID="_content" runat="server">
	<div id="roleEditor">
	<seti:SecureDetailsView ID="_details" DataKeyNames="Id" DataSourceID="_dataSource"
		runat="server" AutoGenerateRows="False" meta:resourcekey="Details">
		<Fields>
			<seti:SecureBoundField DataField="Name" meta:resourcekey="NameField" MaxLength="50" ControlStyle-CssClass="textBox mediumData">
				<ControlStyle CssClass="textBox mediumData"></ControlStyle>
			</seti:SecureBoundField>
			<seti:SecureBoundField DataField="Description" meta:resourcekey="DescriptionField" MaxLength="255" ControlStyle-CssClass="textBox longData">
				<ControlStyle CssClass="textBox longData"></ControlStyle>
			</seti:SecureBoundField>
			<seti:SecureTemplateField PropertyName="Functions" meta:resourcekey="FunctionsField">
				<EditItemTemplate>
					<asp:CheckBoxList ID="_functions" runat="server" DataTextField="Name" DataValueField="Id"></asp:CheckBoxList>
				</EditItemTemplate>
				<ItemTemplate>
					<asp:BulletedList ID="_functions" runat="server" DataTextField="Name"></asp:BulletedList>
				</ItemTemplate>
			</seti:SecureTemplateField>
		</Fields>
	</seti:SecureDetailsView>
	<seti:EditorControls ID="_commands" runat="server" AcceptPostBackUrl="~/RoleList.aspx"
		CancelPostBackUrl="~/RoleList.aspx" DetailsViewID="_details" meta:resourcekey="EditorCommands">
	</seti:EditorControls>
	<asp:ObjectContainerDataSource ID="_dataSource" runat="server" 
		DataObjectTypeName=""></asp:ObjectContainerDataSource>
	</div>
</asp:Content>
