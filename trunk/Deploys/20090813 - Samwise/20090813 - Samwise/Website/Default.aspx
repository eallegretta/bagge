<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Bagge.Seti.WebSite._Default" MasterPageFile="~/Site.Master" %>
<asp:Content ContentPlaceHolderID="_content" runat="server" ID="_content">
<div id="homePage">

<table id="calendar">
<tr>
	<td><asp:HyperLink ID="_prevWeek" runat="server"><</asp:HyperLink></td>
	<td>
		<table id="dates">
			<thead>
				<asp:Repeater ID="_dayNames" runat="server">
					<HeaderTemplate><tr id="dayNames"></HeaderTemplate>
					<ItemTemplate><th><%#Container.DataItem%></th></ItemTemplate>
					<FooterTemplate></tr></FooterTemplate>
				</asp:Repeater>
				<asp:Repeater ID="_days" runat="server">
					<HeaderTemplate><tr id="days"></HeaderTemplate>
					<ItemTemplate><td><%#string.Format("{0:d}", Container.DataItem)%></td></ItemTemplate>
					<FooterTemplate></tr></FooterTemplate>
				</asp:Repeater>
				<asp:Repeater ID="_details" runat="server" OnItemDataBound="_details_ItemDataBound">
					<HeaderTemplate><tr></HeaderTemplate>
					<ItemTemplate><td id="tickets">
						<asp:Repeater ID="_tickets" runat="server">
							<ItemTemplate>
								<div class="ticket">
								<%#Eval("Customer")%><br />
								<%#Eval("ExecutionDateTime")%>
								</div>
							</ItemTemplate>
						</asp:Repeater>
					</td></ItemTemplate>
					<FooterTemplate></tr></FooterTemplate>
				</asp:Repeater>
			</thead>
		</table>
	</td>
	<td>
		<asp:HyperLink id="_nextWeek" runat="server">></asp:HyperLink>
	</td>
</tr>
</table>
</div>
</asp:Content>
