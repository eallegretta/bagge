<%@ Control Language="C#" AutoEventWireup="true" Inherits="Bagge.Seti.WebSite.Controls.EditorCommands" Codebehind="EditorCommands.ascx.cs" %>
<br />
<asp:PlaceHolder ID="_noRecord" runat="server" Visible="False">
	<div style="text-align:center">
		<asp:Label ID="_noRecordMessage" runat="server" meta:resourcekey="NoRecordsMessageLabel"></asp:Label></div>
</asp:PlaceHolder>
<div class="commands">
	<asp:PlaceHolder ID="_extraButtons" runat="server"></asp:PlaceHolder>
	<asp:Button ID="_accept" runat="server" OnClick="_accept_Click" meta:resourcekey="AcceptButton" />
	<asp:Button ID="_cancel" runat="server" CausesValidation="False" meta:resourcekey="CancelButton" />
	<asp:Button ID="_back" runat="server" CausesValidation="False" meta:resourcekey="BackButton" />
</div>