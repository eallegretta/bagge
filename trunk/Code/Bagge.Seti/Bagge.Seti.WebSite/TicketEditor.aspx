<%@ Page Title="" Language="C#" MasterPageFile="~/Editor.Master" AutoEventWireup="true" CodeBehind="TicketEditor.aspx.cs" Inherits="Bagge.Seti.WebSite.TicketEditor" %>
<%@ Register Src="~/Controls/EditorCommands.ascx" TagPrefix="seti" TagName="EditorControls" %>
<%@ Register Src="~/Controls/Calendar.ascx" TagPrefix="seti" TagName="Calendar"  %>
<asp:Content ID="_content" ContentPlaceHolderID="_content" runat="server">
	<seti:SecureDetailsView ID="_details" DataKeyNames="Id" DataSourceID="_dataSource"
		runat="server" AutoGenerateRows="False" meta:resourcekey="Details">
		<Fields>
			<seti:SecureTemplateField PropertyName="Customer" meta:resourcekey="CustomerField">
				<EditItemTemplate>
					<asp:DropDownList ID="_customer" runat="server" DataValueField="Id" DataTextField="Name">
					</asp:DropDownList>
				</EditItemTemplate>
				<ItemTemplate>
					<asp:HiddenField ID="_customer" runat="server" />
					<%#Eval("Customer")%>
				</ItemTemplate>
			</seti:SecureTemplateField>
			<seti:SecureTemplateField PropertyName="Status" meta:resourcekey="StatusField">
				<EditItemTemplate>
					<asp:DropDownList ID="_status" runat="server" DataValueField="Id" DataTextField="Name">
					</asp:DropDownList>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Eval("Status")%>
				</ItemTemplate>
			</seti:SecureTemplateField>
			<seti:SecureTemplateField PropertyName="CreationDate" meta:resourcekey="CreationDateField">
				<EditItemTemplate>
					<seti:Calendar id="_creationDateCalendar" runat="server" RequiresValidation="true" NonNullableDate='<%#Bind("CreationDate")%>'></seti:Calendar>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Eval("CreationDate")%>
				</ItemTemplate>
			</seti:SecureTemplateField>
			<seti:SecureTemplateField PropertyName="ExecutionDate" meta:resourcekey="ExecutionDateField">
				<EditItemTemplate>
					<seti:Calendar id="_executionDateCalendar" runat="server" Date='<%#Bind("ExecutionDate")%>'></seti:Calendar>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Eval("ExecutionDate")%>
				</ItemTemplate>
			</seti:SecureTemplateField>
			<seti:SecureTemplateField PropertyName="CustomerArrival" meta:resourcekey="CustomerArrivalField">
				<EditItemTemplate>
					<seti:Calendar id="_customerArrival" runat="server" Date='<%#Bind("CustomerArrival")%>' RequiresValidation="true" ShowTime="true"></seti:Calendar>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Eval("CustomerArrival")%>
				</ItemTemplate>
			</seti:SecureTemplateField>
			<seti:SecureBoundField DataField="EstimatedDuration" DefaultValue="00.00" Mask="99.99" MaskPlaceHolder="0" meta:resourcekey="EstimatedDurationField" />
			<seti:SecureBoundField DataField="RealDuration" Mask="99.99" MaskPlaceHolder="0" meta:resourcekey="RealDurationField" />
			<seti:SecureTemplateField PropertyName="Description" meta:resourcekey="Description">
				<EditItemTemplate>
					<asp:TextBox id="_description" runat="server" Text='<%#Bind("Description")%>' Rows="4" Columns="80" TextMode="Multiline">
				</asp:TextBox>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Server.HtmlEncode(Eval("Description").ToString())%>
				</ItemTemplate>
			</seti:SecureTemplateField>
		</Fields>
	</seti:SecureDetailsView>
	<seti:EditorControls ID="_commands" runat="server" AcceptPostBackUrl="~/RoleList.aspx"
		CancelPostBackUrl="~/RoleList.aspx" DetailsViewID="_details" meta:resourcekey="EditorCommands">
	</seti:EditorControls>
	<asp:ObjectContainerDataSource ID="_dataSource" runat="server" 
		DataObjectTypeName=""></asp:ObjectContainerDataSource>
		<!--
		[Property]
		decimal? Budget


		[HasAndBelongsToMany(typeof(TicketEmployee), Table = "TicketEmployee", ColumnKey = "TicketId", ColumnRef = "EmployeeId", Lazy = true)]
		public virtual IList<TicketEmployee> Employees

		[HasAndBelongsToMany(typeof(ProductTicket), Table = "ProductTicket", ColumnKey = "TicketId", ColumnRef = "ProductId", Lazy = true)]
		Products

		[BelongsTo("EmployeeCreatorId")]
		Creator

		[BelongsTo("TicketStatusId")]
		Status
		-->
</asp:Content>
