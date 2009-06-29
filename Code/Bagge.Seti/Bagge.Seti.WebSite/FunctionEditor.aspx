<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FunctionEditor.aspx.cs" Inherits="Bagge.Seti.WebSite.FunctionEditor" %>
<%@ Register Src="~/Controls/EditorCommands.ascx" TagPrefix="seti" TagName="EditorControls" %>
<asp:Content ID="_content" ContentPlaceHolderID="_content" runat="server">
<seti:SecureDetailsView ID="_details" DataKeyNames="Id" DataSourceID="_dataSource"
		runat="server" AutoGenerateRows="False" meta:resourcekey="Details">
		<Fields>
			<seti:SecureTemplateField PropertyName="Roles">
				<EditItemTemplate>
					<asp:DropDownList id="_roles" runat="server" AppendDataBoundItems="true" DataValueField="Id" DataTextField="Name">
						<asp:ListItem></asp:ListItem>
					</asp:DropDownList>
				</EditItemTemplate>
				<ItemTemplate>
					<asp:BulletedList ID="_roles" runat="server"></asp:BulletedList>				
				</ItemTemplate>
			</seti:SecureTemplateField>
		</Fields>
	</seti:SecureDetailsView>
	<seti:EditorControls ID="_commands" runat="server" AcceptPostBackUrl="~/RoleList.aspx"
		CancelPostBackUrl="~/RoleList.aspx" DetailsViewID="_details" meta:resourcekey="EditorCommands">
	</seti:EditorControls>
	<asp:ObjectContainerDataSource ID="_dataSource" runat="server" 
		DataObjectTypeName=""></asp:ObjectContainerDataSource>

</asp:Content>
