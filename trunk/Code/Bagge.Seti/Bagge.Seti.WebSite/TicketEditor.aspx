<%@ Page Title="" Language="C#" MasterPageFile="~/Editor.Master" EnableEventValidation="false"
	AutoEventWireup="true" CodeBehind="TicketEditor.aspx.cs" Inherits="Bagge.Seti.WebSite.TicketEditor"
	meta:resourcekey="Page" %>

<%@ Register Src="~/Controls/EditorCommands.ascx" TagPrefix="seti" TagName="EditorControls" %>
<%@ Register Src="~/Controls/Calendar.ascx" TagPrefix="seti" TagName="Calendar" %>
<%@ Register TagPrefix="seti" TagName="ProductTicketSelectionGrid" Src="~/Controls/ProductTicketSelectionGrid.ascx" %>
<asp:Content ID="_head" runat="server" ContentPlaceHolderID="_head">

	<script type="text/javascript">
		function ValidateCustomerArrival(sender, args) {
			var nums = args.Value.split(":");
			var hours = parseInt(nums[0]);
			var mins = parseInt(nums[1]);
			args.IsValid = hours >= 0 && hours <= 23 && mins >= 0 && mins <= 59;
		}
	</script>

</asp:Content>
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
					<asp:HiddenField ID="_status" runat="server" />
					<%#Eval("Status")%>
				</ItemTemplate>
			</seti:SecureTemplateField>
			<seti:SecureTemplateField PropertyName="ExecutionDateTime" meta:resourcekey="ExecutionDateField">
				<EditItemTemplate>
					<seti:Calendar ID="_executionDateCalendar" runat="server" Date='<%#Bind("ExecutionDateTime")%>'
						IsRequired="true" meta:resourcekey="ExecutionDateCalendar"></seti:Calendar>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Eval("ExecutionDateTime", "{0:d}")%>
				</ItemTemplate>
			</seti:SecureTemplateField>
			<seti:SecureTemplateField PropertyName="ExecutionDateTime" meta:resourcekey="CustomerArrivalField">
				<EditItemTemplate>
					<seti:MaskedTextBox ID="_customerArrival" runat="server" Mask="99:99" EnableTheming="false" Text='<%#Eval("ExecutionDateTime", "{0:HH:mm}")%>' CssClass="textBox smallData"></seti:MaskedTextBox>
					<asp:CustomValidator ID="_customerArrivalVal" ControlToValidate="_customerArrival" runat="server" ValidateEmptyText="true"
						ClientValidationFunction="ValidateCustomerArrival" EnableClientScript="true"
						OnServerValidate="_customerArrivalVal_ServerValidate" meta:resourcekey="CustomerArrivalValidator">
					</asp:CustomValidator>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Eval("ExecutionDateTime", "{0:HH:mm}")%>
				</ItemTemplate>
			</seti:SecureTemplateField>
			<seti:SecureTemplateField PropertyName="EstimatedDuration" meta:resourcekey="EstimatedDurationField">
				<EditItemTemplate>
					<asp:TextBox ID="_estimatedDuration" runat="server" Text='<%#Bind("EstimatedDuration")%>' EnableTheming="false" CssClass="textBox smallData numeric"></asp:TextBox>
					<ajax:MaskedEditExtender ID="_estimatedDurationMask" runat="server" MaskType="Number" InputDirection="RightToLeft" Mask="99.99" TargetControlID="_estimatedDuration"></ajax:MaskedEditExtender>
					<asp:RequiredFieldValidator ID="_estimatedDurationReqVal" ControlToValidate="_estimatedDuration" runat="server" meta:resourcekey="EstimatedDurationValidator"></asp:RequiredFieldValidator>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Eval("EstimatedDuration")%>
				</ItemTemplate>
			</seti:SecureTemplateField>
			<seti:SecureTemplateField PropertyName="RealDuration" meta:resourcekey="RealDurationField">
				<EditItemTemplate>
					<asp:TextBox ID="_realDuration" runat="server" Text='<%#Bind("RealDuration")%>' EnableTheming="false" CssClass="textBox smallData numeric"></asp:TextBox>
					<ajax:MaskedEditExtender ID="_realDurationMask" runat="server" MaskType="Number" InputDirection="RightToLeft" Mask="99.99" TargetControlID="_realDuration"></ajax:MaskedEditExtender>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Eval("RealDuration")%>
				</ItemTemplate>
			</seti:SecureTemplateField>
			<seti:SecureTemplateField PropertyName="Description" meta:resourcekey="DescriptionField">
				<EditItemTemplate>
					<asp:TextBox ID="_description" runat="server" Text='<%# Bind("Description") %>' Rows="4"
						Columns="80" TextMode="MultiLine"></asp:TextBox>
					<asp:RequiredFieldValidator ID="_descriptionReqVal" runat="server" ControlToValidate="_description"
						meta:resourcekey="DescriptionValidator"></asp:RequiredFieldValidator>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Server.HtmlEncode(Eval("Description").ToString())%>
				</ItemTemplate>
			</seti:SecureTemplateField>
			<seti:SecureTemplateField PropertyName="Employees" meta:resourcekey="EmployeesField">
				<EditItemTemplate>
					<asp:CheckBoxList ID="_employees" runat="server" DataValueField="Id" DataTextField="Fullname">
					</asp:CheckBoxList>
					<asp:TextBox ID="_validationTarget" runat="server" Visible="false" />
					<asp:CustomValidator ID="_employeesValidator" ValidateEmptyText="true" OnServerValidate="_employeesValidator_ServerValidate"
						ControlToValidate="_validationTarget" runat="server" EnableClientScript="false"
						meta:resourcekey="EmployeesValidator">
					</asp:CustomValidator>
				</EditItemTemplate>
				<ItemTemplate>
					<asp:BulletedList ID="_employees" runat="server" DataValueField="Id" DataTextField="Fullname">
					</asp:BulletedList>
				</ItemTemplate>
			</seti:SecureTemplateField>
			<seti:SecureTemplateField PropertyName="Products" meta:resourcekey="ProductsField">
				<EditItemTemplate>
					<seti:ProductTicketSelectionGrid ID="_products" runat="server" SelectedItems='<%#Eval("Products")%>'>
					</seti:ProductTicketSelectionGrid>
				</EditItemTemplate>
				<ItemTemplate>
					<seti:ProductTicketSelectionGrid ID="_products" runat="server" SelectedItems='<%#Eval("Products")%>'
						ReadOnly="true"></seti:ProductTicketSelectionGrid>
				</ItemTemplate>
			</seti:SecureTemplateField>
			<seti:SecureTemplateField meta:resourcekey="BudgetField" PropertyName="Budget">
				<EditItemTemplate>
					<asp:TextBox ID="_budget" runat="server" Text='<%#Bind("Budget")%>' EnableTheming="false" CssClass="textBox numeric mediumData"></asp:TextBox>
					<ajax:MaskedEditExtender ID="_budgetMask" runat="server" TargetControlID="_budget" InputDirection="RightToLeft" Mask="999999999.99" MaskType="Number"></ajax:MaskedEditExtender>
					<asp:CustomValidator ID="_budgetValidator" runat="server" ControlToValidate="_budget" ValidateEmptyText="true" OnServerValidate="_budgetValidator_ServerValidate" meta:resourcekey="BudgetValidator"></asp:CustomValidator>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Eval("Budget")%>				
				</ItemTemplate>
			</seti:SecureTemplateField>
			<seti:SecureBoundField DataField="Notes" meta:resourcekey="NotesField" TextMode="Multiline"></seti:SecureBoundField>
		</Fields>
	</seti:SecureDetailsView>
	<seti:EditorControls ID="_commands" runat="server" DefaultButton="_accept" AcceptPostBackUrl="~/TicketList.aspx"
		CancelPostBackUrl="~/TicketList.aspx" DetailsViewID="_details" meta:resourcekey="EditorCommands">
		<ExtraButtons>
			<asp:Button ID="_approve" runat="server" OnClick="_approve_Click" meta:resourcekey="ApproveButton" />
			<asp:Button ID="_close" runat="server"  OnClick="_close_Click" meta:resourcekey="CloseButton" />
		</ExtraButtons>
	</seti:EditorControls>
	<asp:ObjectContainerDataSource ID="_dataSource" runat="server" DataObjectTypeName="">
	</asp:ObjectContainerDataSource>
</asp:Content>
