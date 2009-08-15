<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Bagge.Seti.WebSite._Default" MasterPageFile="~/Site.Master" meta:resourcekey="Page" %>
<asp:Content ID="_head" ContentPlaceHolderID="_head" runat="server">
	<script type="text/javascript" src="Scripts/jquery.colorbox-min.js"></script>
	<link type="text/css" media="screen" rel="stylesheet" href="Styles/colorbox.css" />
	<!--[if IE]>
	<link type="text/css" media="screen" rel="stylesheet" href="Styles/colorbox-ie.css" title="example" />
	<![endif]-->

	<script type="text/javascript">
		$(document).ready(function(){
			setupGoolgeMaps();
		
			Sys.WebForms.PageRequestManager.getInstance().add_endRequest(setupGoolgeMaps);
		});

		function setupGoolgeMaps(){
			$(".googleMaps").colorbox({width:700, height:500, iframe:true});
		}
	</script>
</asp:Content>
<asp:Content ContentPlaceHolderID="_content" runat="server" ID="_content">
<div id="homePage">
		<div id="options">
			<div id="calendarModeOptions">
				<span id="displayDate"><asp:Literal ID="_displayDateText" runat="server" meta:resourcekey="DisplayDateTextLiteral"></asp:Literal></span>
				<asp:DropDownList ID="_displayDate" runat="server" AutoPostBack="true">
					<asp:ListItem Value="Weekly" meta:resourcekey="WeeklyDisplayDateListItem"></asp:ListItem>
					<asp:ListItem Selected="True" Value="Monthly" meta:resourcekey="MontlyDisplayDateListItem"></asp:ListItem>
				</asp:DropDownList>
			</div>

			<asp:LinkButton ID="_prevDate" runat="server"><asp:Image runat="server" SkinID="IconArrowLeft" /></asp:LinkButton>	
			<span id="currentDate"><asp:Literal ID="_currentDate" runat="server"></asp:Literal></span>
			<asp:LinkButton ID="_nextDate" runat="server"><asp:Image runat="server" SkinID="IconArrowRight" /></asp:LinkButton>	
		</div>
		<div id="calendarTickets">
			<asp:Repeater ID="_tickets" runat="server" OnItemDataBound="_tickets_ItemDataBound">
				<ItemTemplate>
					<div class="ticket">
						<div class="ticketContainer">
							<div class="ticketHeader  head_status_<%#(Bagge.Seti.BusinessEntities.TicketStatusEnum)((Bagge.Seti.BusinessEntities.TicketStatus)Eval("Status")).Id%>"><%#Eval("ExecutionDateTime", "{0:d}")%> <%#Eval("ExecutionDateTime", "{0:HH:mm}")%></div>
							<div class="ticketBody  body_status_<%#(Bagge.Seti.BusinessEntities.TicketStatusEnum)((Bagge.Seti.BusinessEntities.TicketStatus)Eval("Status")).Id%>">
								<%#Eval("Customer")%><br />
								<a target="_blank" id="_googleMapsLink" runat="server" class="googleMaps" href="Maps.aspx?Destination={0}">
								<%#((Bagge.Seti.BusinessEntities.Customer)Eval("Customer")).FullAddress%>,
								<%#((Bagge.Seti.BusinessEntities.Customer)Eval("Customer")).District%><br />
								<%#((Bagge.Seti.BusinessEntities.Customer)Eval("Customer")).District.CountryState%></a><br />
								<asp:Panel ID="_actionLinks" runat="server" CssClass="actionLinks">
									<asp:PlaceHolder ID="_viewLink" runat="server"><a id="viewLink" href="TicketEditor.aspx?Id=<%#Eval("Id")%>&Action=View"><asp:Literal id="_viewImage" runat="server" /></a></asp:PlaceHolder>
									<asp:PlaceHolder ID="_editLink" runat="server"><a id="editLink" href="TicketEditor.aspx?Id=<%#Eval("Id")%>&Action=Edit"><asp:Literal id="_editImage" runat="server" /></a></asp:PlaceHolder>
									<asp:PlaceHolder ID="_updateProgressLink" runat="server"><a id="updateProgress" href="TicketEditor.aspx?Id=<%#Eval("Id")%>&Action=Edit&UpdateProgress=true"><asp:Literal id="_updateProgressImage" runat="server"  /></a></asp:PlaceHolder>
								</asp:Panel>
							</div>
						</div>
						<div class="ticketShadowBottom">
							<div class="ticketShadowBottomLeft"></div>
							<div class="ticketShadowBottomRight"></div>
						</div>
					</div>
				</ItemTemplate>
			</asp:Repeater>
			<asp:Label ID="_noTicketsMessage" runat="server" CssClass="NoTicketMessage" meta:resourcekey="NoTicketMessageLabel"></asp:Label>
		</div>
		<div id="legends">
			<fieldset>
				<legend><asp:Literal ID="_legendText" runat="server" meta:resourcekey="LegendTextLiteral"></asp:Literal></legend>
				<asp:Repeater ID="_legends" runat="server">
					<HeaderTemplate>
						<table width="100%">
					</HeaderTemplate>
					<ItemTemplate>
						<tr>
							<td><div class="legend body_status_<%#(Bagge.Seti.BusinessEntities.TicketStatusEnum)Eval("Id")%>"></div></td>
							<td><%#Eval("Name")%></td>	
						</tr>					
					</ItemTemplate>
					<FooterTemplate>
						</table>
					</FooterTemplate>
				</asp:Repeater>
			</fieldset>
		</div>
</div>

 
</asp:Content>
