<%@ Page Title="" Language="C#" MasterPageFile="~/Editor.Master" AutoEventWireup="true" CodeBehind="TicketEditor.aspx.cs" Inherits="Bagge.Seti.WebSite.TicketEditor" meta:resourcekey="Page"%>
<%@ Register Src="~/Controls/EditorCommands.ascx" TagPrefix="seti" TagName="EditorControls" %>
<%@ Register Src="~/Controls/Calendar.ascx" TagPrefix="seti" TagName="Calendar"  %>
<%@ Register TagPrefix="seti" TagName="ProductTicketSelectionGrid" Src="~/Controls/ProductTicketSelectionGrid.ascx" %>
<asp:Content ID="_content" ContentPlaceHolderID="_content" runat="server">
	<seti:SecureDetailsView ID="_details" DataKeyNames="Id" DataSourceID="_dataSource"
		runat="server" AutoGenerateRows="False" meta:resourcekey="Details">
		<Fields>
			<seti:SecureTemplateField PropertyName="Customer" meta:resourcekey="CustomerField">
				<EditItemTemplate>
					<asp:DropDownList ID="_customer" runat="server" DataValueField="Id" 
						DataTextField="Name">
					</asp:DropDownList>
				</EditItemTemplate>
				<ItemTemplate>
					<asp:HiddenField ID="_customer" runat="server" />
					<%#Eval("Customer")%>
				</ItemTemplate>
			</seti:SecureTemplateField>
			<seti:SecureTemplateField PropertyName="Status" meta:resourcekey="StatusField">
				<EditItemTemplate>
					<asp:DropDownList ID="_status" runat="server" DataValueField="Id" 
						DataTextField="Name">
					</asp:DropDownList>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Eval("Status")%>
				</ItemTemplate>
			</seti:SecureTemplateField>
			<seti:SecureTemplateField PropertyName="ExecutionDate" meta:resourcekey="ExecutionDateField">
				<EditItemTemplate>
					<seti:Calendar id="_executionDateCalendar" runat="server" Date='<%#Bind("ExecutionDate")%>' ></seti:Calendar>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Eval("ExecutionDate")%>
				</ItemTemplate>
			</seti:SecureTemplateField>
			<seti:SecureTemplateField PropertyName="CustomerArrival" meta:resourcekey="CustomerArrivalField">
				<EditItemTemplate>
					<seti:Calendar id="_customerArrival" runat="server" RequiresValidation="true" 
						ShowTime="true"  NonNullableDate='<%#Bind("CustomerArrival")%>'></seti:Calendar>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Eval("CustomerArrival")%>
				</ItemTemplate>
			</seti:SecureTemplateField>
			<seti:SecureBoundField DataField="EstimatedDuration" DefaultValue="00{NumberSeparator}00" Mask="99{NumberSeparator}99" MaskPlaceHolder="0" meta:resourcekey="EstimatedDurationField" />
			<seti:SecureBoundField DataField="RealDuration" Mask="99{NumberSeparator}99" MaskPlaceHolder="0" meta:resourcekey="RealDurationField" />
			<seti:SecureTemplateField PropertyName="Description" meta:resourcekey="DescriptionField">
				<EditItemTemplate>
					<asp:TextBox id="_description" runat="server" Text='<%# Bind("Description") %>' 
						Rows="4" Columns="80" TextMode="MultiLine"></asp:TextBox>
					<asp:RequiredFieldValidator ID="_descriptionReqVal" runat="server" ControlToValidate="_description" meta:resourcekey="DescriptionValidator"></asp:RequiredFieldValidator>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Server.HtmlEncode(Eval("Description").ToString())%>
				</ItemTemplate>
			</seti:SecureTemplateField>
			<seti:SecureTemplateField PropertyName="Employees" meta:resourcekey="EmployeesField">
				<EditItemTemplate>
					<asp:CheckBoxList ID="_employees" runat="server" DataValueField="Id" 
						DataTextField="Fullname">
					</asp:CheckBoxList>
				</EditItemTemplate>
				<ItemTemplate>
					<asp:BulletedList ID="_employees" runat="server" DataValueField="Id" 
						DataTextField="Fullname">
					</asp:BulletedList>
				</ItemTemplate>
			</seti:SecureTemplateField>
			<seti:SecureTemplateField PropertyName="Products" meta:resourcekey="ProductsField">
				<EditItemTemplate>
					<seti:ProductTicketSelectionGrid id="_products" runat="server"></seti:ProductTicketSelectionGrid>
				</EditItemTemplate>
				<ItemTemplate>
					<seti:ProductTicketSelectionGrid id="_products" runat="server" ReadOnly="true"></seti:ProductTicketSelectionGrid>
				</ItemTemplate>
			</seti:SecureTemplateField>
		</Fields>
	</seti:SecureDetailsView>
	<seti:EditorControls ID="_commands" runat="server" AcceptPostBackUrl="~/TicketList.aspx"
		CancelPostBackUrl="~/TicketList.aspx" DetailsViewID="_details" meta:resourcekey="EditorCommands">
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
