<%@ Control Language="C#" AutoEventWireup="true" Inherits="Bagge.Seti.WebSite.Controls.ListCommands" Codebehind="ListCommands.ascx.cs" %>
<br />
<div class="commands">
	<button id="_new" runat="server" onserverclick="_new_ServerClick"><asp:Image ID="_iconAdd" runat="server" SkinID="IconAdd" /><asp:Literal ID="_newText" runat="server"></asp:Literal></button>
</div>