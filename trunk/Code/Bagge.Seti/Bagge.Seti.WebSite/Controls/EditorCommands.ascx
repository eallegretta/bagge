<%@ Control Language="C#" AutoEventWireup="true" Inherits="ELearningEngine.WebSite.Admin.Controls.EditorCommands" Codebehind="EditorCommands.ascx.cs" %>
<br />
<asp:PlaceHolder ID="_noRecord" runat="server" Visible="false"><div style="text-align:center">No se encontr&oacute; el registro a editar</div></asp:PlaceHolder>
<div class="commands">
	<asp:Button ID="_accept" runat="server" Text="Aceptar" OnClick="_accept_Click" />
	<asp:Button ID="_cancel" runat="server" Text="Cancelar" CausesValidation="false" />
</div>