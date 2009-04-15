<%@ Page Title="" Language="C#" MasterPageFile="~/Editor.Master" AutoEventWireup="true" CodeBehind="ProductEditor.aspx.cs" Inherits="Bagge.Seti.WebSite.ProductEditor" meta:resourcekey="Page" %>
<%@ Register TagName="ProductProviderGrid" TagPrefix="controls" Src="~/Controls/ProductProviderSelectionGrid.ascx"%>
<%@ Register Src="~/Controls/EditorCommands.ascx" TagPrefix="seti" TagName="EditorControls" %>
<asp:Content ID="_content" ContentPlaceHolderID="_content" runat="server">
	<seti:SecureDetailsView ID="_details" DataKeyNames="Id, AuditTimeStamp" DataSourceID="_dataSource"
		runat="server" AutoGenerateRows="False" meta:resourcekey="Details">
		<Fields>
			<seti:SecureBoundField ControlStyle-Width="320px" MaxLength="50" DataField="Name"
				meta:resourcekey="NameField">
			</seti:SecureBoundField>
			<seti:SecureBoundField DataField="Description" meta:resourcekey="DescriptionField">
			</seti:SecureBoundField>
			<seti:SecureTemplateField PropertyName="Providers" meta:resourcekey="ProvidersField">
				<InsertItemTemplate>
					<controls:ProductProviderGrid id="_providers" runat="server" SourceType="Provider" SelectedItems='<%#Bind("Providers")%>' />					
				</InsertItemTemplate>
				<EditItemTemplate>
					<controls:ProductProviderGrid id="_providers" runat="server" SourceType="Provider" SelectedItems='<%#Bind("Providers")%>' />					
				</EditItemTemplate>
				<ItemTemplate>
					<controls:ProductProviderGrid id="_providers" runat="server" SourceType="Provider" SelectedItems='<%#Bind("Providers")%>' ReadOnly="true" />
				</ItemTemplate>
			</seti:SecureTemplateField>
		</Fields>
	</seti:SecureDetailsView>
	<seti:EditorControls ID="_commands" runat="server" AcceptPostBackUrl="~/ProductList.aspx"
		CancelPostBackUrl="~/ProductList.aspx" DetailsViewID="_details" meta:resourcekey="EditorCommands">
	</seti:EditorControls>
	<asp:ObjectContainerDataSource ID="_dataSource" runat="server" 
		DataObjectTypeName=""></asp:ObjectContainerDataSource>
</asp:Content>

