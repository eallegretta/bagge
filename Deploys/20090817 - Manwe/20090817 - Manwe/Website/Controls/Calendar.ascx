<%@ Control Language="C#" AutoEventWireup="true" Inherits="Bagge.Seti.WebSite.Controls.Calendar" Codebehind="Calendar.ascx.cs" %>
<style type="text/css">
div.ajax__calendar_days table tr td
{
	padding-right:0px;
}
</style>
<asp:HiddenField ID="_calendarSelectedValue" runat="server" />
<asp:TextBox ID="_calendar" runat="server" Width="70"></asp:TextBox>
<asp:Image SkinID="IconCalendar" runat="server" ID="_calendarImage" />
<asp:Image SkinID="IconCalendarDisabled" runat="server" ID="_calendarImageDisabled" />
<ajax:CalendarExtender ID="_calendarExt" runat="server" TargetControlID="_calendar" PopupButtonID="_calendarImage"></ajax:CalendarExtender>
<asp:RequiredFieldValidator ID="_calendarReqVal" Enabled="false" Visible="false" runat="server" Display="Dynamic" ControlToValidate="_calendar"></asp:RequiredFieldValidator>
<asp:CustomValidator id="_invalidDateVal" Enabled="true" Visible="true" ValidateEmptyText="false" Display="Dynamic" EnableClientScript="true" ClientValidationFunction="ValidateDateCalendar" runat="server"></asp:CustomValidator>
<asp:CustomValidator id="_calendarCompare" Enabled="false" Visible="false" ValidateEmptyText="false" Display="Dynamic" EnableClientScript="true"  ClientValidationFunction="ValidateCompareCalendar" runat="server"></asp:CustomValidator>
<asp:PlaceHolder ID="_timePlaceHolder" Visible="false" runat="server">
</asp:PlaceHolder>