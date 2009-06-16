<%@ Control Language="C#" AutoEventWireup="true" Inherits="Bagge.Seti.WebSite.Controls.Calendar" Codebehind="Calendar.ascx.cs" %>
<asp:HiddenField ID="_calendarSelectedValue" runat="server" />
<asp:TextBox ID="_calendar" runat="server" ReadOnly="true" Width="70"></asp:TextBox>
<asp:Image SkinID="IconCalendar" runat="server" ID="_calendarImage" />
<asp:Image SkinID="IconCalendarDisabled" runat="server" ID="_calendarImageDisabled" />
<ajax:CalendarExtender ID="_calendarExt" runat="server" TargetControlID="_calendar" PopupButtonID="_calendarImage"></ajax:CalendarExtender>
<asp:RequiredFieldValidator ID="_calendarReqVal" Enabled="false" runat="server" ControlToValidate="_calendar"></asp:RequiredFieldValidator>
<asp:PlaceHolder ID="_timePlaceHolder" Visible="false" runat="server">
</asp:PlaceHolder>