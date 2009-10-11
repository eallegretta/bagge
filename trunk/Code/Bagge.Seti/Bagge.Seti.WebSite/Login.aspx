<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Bagge.Seti.WebSite.Login" MasterPageFile="~/Site.Master" meta:resourcekey="Page" %>
<asp:Content ID="_content" runat="server" ContentPlaceHolderID="_content">
	<script type="text/javascript" src="Scripts/jquery-ui-1.7.2.custom.min.js"></script>
	<asp:Label ID="_title" CssClass="titleLabel" runat="server" meta:resourcekey="TitleLabel"></asp:Label>
	<div id="loginPage">
	<asp:Image runat="server" SkinID="IconLogin" />
	<asp:Login  ID="_login" runat="server" meta:resourcekey="Login" 
		onauthenticate="_login_Authenticate" PasswordRecoveryUrl="~/RecoverPassword.aspx" onloggedin="_login_LoggedIn">
		<LayoutTemplate>
			<div class="AspNet-Login">
				<div class="AspNet-Login-UserPanel">
					<asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" meta:resourcekey="UserNameLabel"></asp:Label>
					<asp:TextBox ID="UserName" runat="server"></asp:TextBox>
					<asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
							  ValidationGroup="Login1" meta:resourcekey="UserNameRequiredValidator"></asp:RequiredFieldValidator>
				</div>
				<div class="AspNet-Login-PasswordPanel">
					<asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" meta:resourcekey="PasswordLabel"></asp:Label>
					<asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
					<asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
					meta:resourcekey="PasswordRequiredValidator" ErrorMessage="test"
							ValidationGroup="Login1">*</asp:RequiredFieldValidator>
				</div>
				<div class="AspNet-Login-RememberMePanel">
					<asp:CheckBox ID="RememberMe" runat="server" meta:resourcekey="RememberMeCheckBox" />
				</div>
				<div class="AspNet-Login-SubmitPanel">
					<asp:Button ID="LoginButton" runat="server" CommandName="Login" meta:resourcekey="LoginButton" ValidationGroup="Login1" />
				</div>
				<div>
					<asp:HyperLink ID="_recoverPassword" runat="server" meta:resourcekey="PasswordRecoveryHyperLink" NavigateUrl="~/RecoverPassword.aspx"></asp:HyperLink>
				</div>
				<div class="failureText" title="Error">
					<asp:Label ID="FailureText" runat="server"></asp:Label>
				</div>
			</div>
		</LayoutTemplate>
	</asp:Login>
	</div>
</asp:Content>