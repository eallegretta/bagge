<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Editor.Master" CodeBehind="EditProfile.aspx.cs" Inherits="Bagge.Seti.WebSite.EditProfile" meta:resourcekey="Page" %>
<%@ Register Src="~/Controls/EditorCommands.ascx" TagPrefix="seti" TagName="EditorControls" %>
<asp:Content ID="_content" ContentPlaceHolderID="_content" runat="server">
	<seti:SecureDetailsView ID="_details" DataKeyNames="Id" DataSourceID="_dataSource"
		runat="server" AutoGenerateRows="False" meta:resourcekey="Details">
		<Fields>
			<seti:SecureBoundField DataField="Firstname" meta:resourcekey="FirstnameField" ControlStyle-CssClass="textBox longData"></seti:SecureBoundField>
			<seti:SecureBoundField DataField="Lastname"  meta:resourcekey="LastnameField" ControlStyle-CssClass="textBox longData"></seti:SecureBoundField>
			<seti:SecureBoundField DataField="Phone"  meta:resourcekey="PhoneField" ControlStyle-CssClass="textBox mediumData"></seti:SecureBoundField>
			<seti:SecureBoundField DataField="EmergencyPhone"  meta:resourcekey="EmergencyPhoneField" ControlStyle-CssClass="textBox mediumData"></seti:SecureBoundField>
			<seti:SecureBoundField DataField="Email"  meta:resourcekey="EmailField" ControlStyle-CssClass="textBox longData"></seti:SecureBoundField>
			<seti:SecureTemplateField PropertyName="Password" meta:resourcekey="PasswordField">
				<InsertItemTemplate>
					<asp:TextBox ID="_password" EnableTheming="false" runat="server" TextMode="Password" CssClass="textBox mediumData" ></asp:TextBox>
					<asp:RequiredFieldValidator ID="_passwordVal" runat="server" ControlToValidate="_password" meta:resourcekey="PasswordRequiredValidator"></asp:RequiredFieldValidator>
				</InsertItemTemplate>
				<EditItemTemplate>
					<asp:Label ID="_emptyPassword" runat="server" meta:resourcekey="EmptyPasswordLabel"></asp:Label><br />
					<asp:TextBox ID="_password" EnableTheming="false" runat="server" TextMode="Password" CssClass="textBox mediumData"></asp:TextBox>
				</EditItemTemplate>
			</seti:SecureTemplateField>
			<seti:SecureTemplateField PropertyName="Password" meta:resourcekey="ConfirmPasswordField">
				<InsertItemTemplate>
					<asp:TextBox ID="_confirmPassword" EnableTheming="false" runat="server" TextMode="Password" CssClass="textBox mediumData"></asp:TextBox>
					<asp:RequiredFieldValidator ID="_confirmPasswordReqVal" runat="server" ControlToValidate="_confirmPassword" meta:resourcekey="ConfirmPasswordRequiredValidator"></asp:RequiredFieldValidator>
					<asp:CompareValidator ID="_confirmPasswordCmpVal" runat="server" ControlToValidate="_password" ControlToCompare="_confirmPassword" meta:resourcekey="ConfirmPasswordCompareValidator"></asp:CompareValidator>
				</InsertItemTemplate>
				<EditItemTemplate>
					<asp:TextBox ID="_confirmPassword" EnableTheming="false" runat="server" TextMode="Password" CssClass="textBox mediumData"></asp:TextBox>
					<asp:CompareValidator ID="_confirmPasswordCmpVal" runat="server" ControlToValidate="_password" ControlToCompare="_confirmPassword" meta:resourcekey="ConfirmPasswordCompareValidator"></asp:CompareValidator>
				</EditItemTemplate>
			</seti:SecureTemplateField>
		</Fields>
	</seti:SecureDetailsView>
	<seti:EditorControls ID="_commands" runat="server" AcceptPostBackUrl="~/Default.aspx"
		CancelPostBackUrl="~/Default.aspx" DetailsViewID="_details" meta:resourcekey="EditorCommands">
	</seti:EditorControls>
	<asp:ObjectContainerDataSource ID="_dataSource" runat="server" 
		DataObjectTypeName=""></asp:ObjectContainerDataSource>
</asp:Content>

