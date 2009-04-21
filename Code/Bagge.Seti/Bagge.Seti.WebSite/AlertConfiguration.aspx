<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AlertConfiguration.aspx.cs" Inherits="Bagge.Seti.WebSite.AlertConfigurationEditor" meta:resourcekey="PageResource1" %>
<%@ Register Src="~/Controls/EditorCommands.ascx" TagPrefix="seti" TagName="EditorControls" meta:resourcekey="Page" %>
<asp:Content ID="_content" ContentPlaceHolderID="_content" runat="server">
	<asp:DetailsView ID="_details" runat="server">
		<Fields>
			<seti:SecureBoundField DataField="Days" meta:resourcekey="DaysField">
				<Validators>
					<asp:RequiredFieldValidator ID="_daysRequired" runat="server" meta:resourcekey="DaysRequiredValidator"></asp:RequiredFieldValidator>
					<asp:RegularExpressionValidator ID="_daysNumeric" runat="server" meta:resourcekey="DaysNumericValidator" ValidationExpression="\d+"></asp:RegularExpressionValidator>
				</Validators>
			</seti:SecureBoundField>
			<seti:SecureBoundField DataField="MaxDaysPendingAproval" meta:resourcekey="DaysField">
				<Validators>
					<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" meta:resourcekey="DaysRequiredValidator"></asp:RequiredFieldValidator>
					<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" meta:resourcekey="DaysNumericValidator" ValidationExpression="\d+"></asp:RegularExpressionValidator>
				</Validators>
			</seti:SecureBoundField>
			<seti:SecureCheckBoxField DataField="SendEmailToCreator" meta:resourcekey="EmailToCreatorField"></seti:SecureCheckBoxField>
			<seti:SecureCheckBoxField DataField="SendEmailToEmployees" meta:resourcekey="EmailToEmployeesField"></seti:SecureCheckBoxField>
		</Fields>
	</asp:DetailsView>
	<seti:EditorControls ID="_commands" runat="server" AcceptPostBackUrl="~/AlertConfiguration.aspx"
		CancelPostBackUrl="~/Home.aspx" DetailsViewID="_details" meta:resourcekey="EditorCommands">
	</seti:EditorControls>
	<asp:ObjectContainerDataSource id="_dataSource" runat="server" 
		DataObjectTypeName=""></asp:ObjectContainerDataSource>
</asp:Content>
