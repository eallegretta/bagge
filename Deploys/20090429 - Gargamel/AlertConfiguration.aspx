<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AlertConfiguration.aspx.cs" Inherits="Bagge.Seti.WebSite.AlertConfigurationEditor" meta:resourcekey="Page" %>
<%@ Register Src="~/Controls/EditorCommands.ascx" TagPrefix="seti" TagName="EditorControls" %>
<asp:Content ID="_content" ContentPlaceHolderID="_content" runat="server">
	<seti:SecureDetailsView ID="_details" DataKeyNames="Id" runat="server" DataSourceID="_dataSource" AutoGenerateRows="false">
		<Fields>
			<seti:SecureBoundField HeaderStyle-Width="300px"  DataField="Days" ControlStyle-CssClass="numeric" meta:resourcekey="DaysField">
				<Validators>
					<asp:RequiredFieldValidator ID="_daysRequired" runat="server" meta:resourcekey="DaysRequiredValidator"></asp:RequiredFieldValidator>
					<asp:RegularExpressionValidator ID="_daysNumeric" runat="server" meta:resourcekey="DaysNumericValidator" ValidationExpression="\d+"></asp:RegularExpressionValidator>
				</Validators>
			</seti:SecureBoundField>
			<seti:SecureBoundField HeaderStyle-Width="300px" DataField="MaxDaysPendingAproval" ControlStyle-CssClass="numeric" meta:resourcekey="DaysField">
				<Validators>
					<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" meta:resourcekey="DaysRequiredValidator"></asp:RequiredFieldValidator>
					<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" meta:resourcekey="DaysNumericValidator" ValidationExpression="\d+"></asp:RegularExpressionValidator>
				</Validators>
			</seti:SecureBoundField>
			<seti:SecureCheckBoxField HeaderStyle-Width="300px" DataField="SendEmailToCreator" meta:resourcekey="EmailToCreatorField"></seti:SecureCheckBoxField>
			<seti:SecureCheckBoxField HeaderStyle-Width="300px" DataField="SendEmailToEmployees" meta:resourcekey="EmailToEmployeesField"></seti:SecureCheckBoxField>
		</Fields>
	</seti:SecureDetailsView>
	<seti:EditorControls ID="_commands" runat="server" AcceptPostBackUrl="~/AlertConfiguration.aspx"
		CancelPostBackUrl="~/Default.aspx" DetailsViewID="_details" meta:resourcekey="EditorCommands">
	</seti:EditorControls>
	<asp:ObjectContainerDataSource id="_dataSource" runat="server" ></asp:ObjectContainerDataSource>
</asp:Content>
