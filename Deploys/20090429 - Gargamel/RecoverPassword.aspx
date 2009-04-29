<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecoverPassword.aspx.cs" 
Inherits="Bagge.Seti.WebSite.RecoverPassword" meta:resourcekey="Page"%>
<asp:Content ID="_content" ContentPlaceHolderID="_content" runat="server">
	<asp:MultiView ID="_recoverPassword" runat="server" ActiveViewIndex="0">
		<asp:View runat="server">
			<asp:Label ID="_emailAddressText" Font-Bold="true" runat="server" meta:resourcekey="EmailAddressTextLabel"></asp:Label>
			<asp:TextBox ID="_emailAddress" runat="server" meta:resourcekey="EmailAddressTextBox"></asp:TextBox>
			<asp:Button ID="_send" runat="server" meta:resourcekey="SendButton" onclick="_send_Click" />	
		</asp:View>
		<asp:View runat="server">
			<asp:Label ID="_passwordSent" Font-Bold="true" runat="server" meta:resourcekey="PasswordSentLabel"></asp:Label>
		</asp:View>
		<asp:View runat="server">
			<asp:Label ID="_passwordRegenerated" Font-Bold="true" runat="server" meta:resourcekey="PasswordRegeneratedLabel"></asp:Label>
		</asp:View>
	</asp:MultiView>
	
</asp:Content>
