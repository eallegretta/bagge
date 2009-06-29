<%@ Page Title="" Language="C#" MasterPageFile="~/List.Master" AutoEventWireup="true" CodeBehind="TicketList.aspx.cs" Inherits="Bagge.Seti.WebSite.TicketList" meta:resourcekey="Page" %>
<%@ Register TagPrefix="seti" TagName="ListCommands" Src="~/Controls/ListCommands.ascx" %>
<asp:Content ID="_filters" runat="server" ContentPlaceHolderID="_filters">
	<table cellpadding="0" cellspacing="0">
		<tr>
			<seti:SecurePropertyPlaceHolder runat="server" PropertyName="Id">
				<th><asp:Literal ID="_idLiteral" runat="server" meta:resourcekey="FilterIdLiteral"></asp:Literal></th>
				<td><asp:TextBox ID="_id" runat="server"></asp:TextBox></td>
			</seti:SecurePropertyPlaceHolder>
			<seti:SecurePropertyPlaceHolder runat="server" PropertyName="Status">
				<th><asp:Literal ID="_statusLiteral" runat="server" meta:resourcekey="FilterStatusLiteral"></asp:Literal></th>
				<td><asp:DropDownList ID="_status" runat="server" DataValueField="Id" DataTextField="Name" AppendDataBoundItems="true">
						<asp:ListItem></asp:ListItem>
					</asp:DropDownList>
				</td>
			</seti:SecurePropertyPlaceHolder>
			<seti:SecurePropertyPlaceHolder runat="server" PropertyName="Description">
				<th><asp:Literal ID="_descriptionLiteral" runat="server" meta:resourcekey="FilterDescriptionLiteral"></asp:Literal></th>
				<td><asp:TextBox ID="_description" runat="server"></asp:TextBox></td>
			</seti:SecurePropertyPlaceHolder>
			<seti:SecurePropertyPlaceHolder runat="server" PropertyName="Employees">
				<th><asp:Literal ID="_employeesLiteral" runat="server" meta:resourcekey="FilterEmployeesLiteral"></asp:Literal></th>
				<td><asp:DropDownList ID="_employees" runat="server" DataTextField="Fullname" DataValueField="Id" AppendDataBoundItems="true">
					<asp:ListItem></asp:ListItem>
				</asp:DropDownList></td>
			</seti:SecurePropertyPlaceHolder>
		</tr>
		<tr>
			<seti:SecurePropertyPlaceHolder runat="server" PropertyName="CreationDate">
				<th><asp:Literal ID="_creationDateLiteral" runat="server" meta:resourcekey="FilterCreationDateLiteral"></asp:Literal></th>
				<td><asp:TextBox ID="_creationDate" runat="server"></asp:TextBox>
				<ajax:CalendarExtender ID="_creationDateCalendar" runat="server" TargetControlID="_creationDate" runat="server"></ajax:CalendarExtender>
				</td>
			</seti:SecurePropertyPlaceHolder>
			<seti:SecurePropertyPlaceHolder runat="server" PropertyName="ExecutionDate">
				<th><asp:Literal ID="_executionDateLiteral" runat="server" meta:resourcekey="FilterExecutionDateLiteral"></asp:Literal></th>
				<td><asp:TextBox ID="_executionDate" runat="server"></asp:TextBox>
					<ajax:CalendarExtender ID="_executaionDateCalendar" runat="server" TargetControlID="_executionDate" runat="server"></ajax:CalendarExtender>
				</td>
			</seti:SecurePropertyPlaceHolder>
			<seti:SecurePropertyPlaceHolder runat="server" PropertyName="Customer">
				<th><asp:Literal ID="_customerLiteral" runat="server" meta:resourcekey="FilterCustomerLiteral"></asp:Literal></th>
				<td>
				<asp:DropDownList ID="_customer" runat="server" DataValueField="Id" DataTextField="Name" AppendDataBoundItems="true">
				<asp:ListItem></asp:ListItem>
				</asp:DropDownList>
				</td>
			</seti:SecurePropertyPlaceHolder>
			<td colspan="2"><asp:Button ID="_filter" runat="server" meta:resourcekey="FilterButton" />
			</td>
		</tr>
	</table>
</asp:Content>

<asp:Content ID="_content" ContentPlaceHolderID="_content" runat="server">
	<seti:SecureGridView ID="_tickets" runat="server" DataKeyNames="Id" 
		DataSourceID="_dataSource"
		meta:resourcekey="Grid">
		<Columns>
			<seti:SecureBoundField NullDisplayText="" DataField="Customer" 
				meta:resourcekey="CustomerField" />
			<seti:SecureBoundField NullDisplayText="" DataField="ExecutionDate" 
				meta:resourcekey="ExecutionDateField" />
			<seti:SecureBoundField NullDisplayText="" DataField="CustomerArrival" 
				meta:resourcekey="CustomerArrivalField" />
			<seti:SecureBoundField NullDisplayText="" DataField="EstimatedDuration" 
				meta:resourcekey="EstimatedDurationField" />
			<seti:SecureBoundField NullDisplayText="" DataField="RealDuration" 
				meta:resourcekey="RealDurationField" />	
			<seti:SecureBoundField NullDisplayText="" DataField="Description" 
				meta:resourcekey="DescriptionField" />	
			<seti:SecureBoundField NullDisplayText="" DataField="Status" 
				meta:resourcekey="StatusField" />
			
			<seti:SecureHyperLinkField MethodName="Get" ItemStyle-Width="20px" 
				ItemStyle-HorizontalAlign="Center" DataNavigateUrlFields="Id" 
				DataNavigateUrlFormatString="TicketEditor.aspx?Id={0}&Action=View"
				Text="<%$ Resources:WebSite, IconViewImageTag %>"
				meta:resourcekey="ViewField" >
				<ItemStyle HorizontalAlign="Center" Width="20px"></ItemStyle>
			</seti:SecureHyperLinkField>
			<seti:SecureHyperLinkField MethodName="Update" ItemStyle-Width="20px" 
				ItemStyle-HorizontalAlign="Center" DataNavigateUrlFields="Id" 
				DataNavigateUrlFormatString="TicketEditor.aspx?Id={0}&Action=Edit"
				Text="<%$ Resources:WebSite, IconEditImageTag %>"
				meta:resourcekey="EditField" >
				<ItemStyle HorizontalAlign="Center" Width="20px"></ItemStyle>
			</seti:SecureHyperLinkField>
		</Columns>
	</seti:SecureGridView>
	<seti:SecureMethodPlaceHolder runat="server" MethodName="Create">
		<seti:ListCommands ID="_new" runat="server" meta:resourceKey="ListCommands" PostBackUrl="~/TicketEditor.aspx" />
	</seti:SecureMethodPlaceHolder>
	<asp:ObjectContainerDataSource id="_dataSource" runat="server" 
		DataObjectTypeName=""></asp:ObjectContainerDataSource>
</asp:Content>