<%@ Control Language="C#" AutoEventWireup="true" Inherits="Bagge.Seti.WebSite.Controls.EditorCommands" Codebehind="EditorCommands.ascx.cs" %>
<br />
<asp:PlaceHolder ID="_noRecord" runat="server" Visible="false"><div style="text-align:center">No se encontr&oacute; el registro a editar</div></asp:PlaceHolder>
<div class="commands">
	<asp:Button ID="_accept" runat="server" Text="Aceptar" OnClick="_accept_Click" />
	<asp:Button ID="_cancel" runat="server" Text="Cancelar" CausesValidation="false" />
	<asp:Button ID="_back" runat="server" Text="Volver" CausesValidation="false" />
</div>