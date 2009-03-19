<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
	CodeBehind="CustomerEditor.aspx.cs" Inherits="Bagge.Seti.WebSite.CustomerEditor"
	meta:resourcekey="Page" %>

<%@ Register Src="~/Controls/EditorCommands.ascx" TagPrefix="seti" TagName="EditorControls" %>
<asp:Content ContentPlaceHolderID="_head" ID="_head" runat="server">

	<script type="text/javascript" src="Scripts/jquery.maskedinput-1.2.2.min.js"></script>

	<script type="text/javascript">
		$(document).ready(function() {
			Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function() {
				setCuitMask();
			});
			setCuitMask();
		});

		function setCuitMask() {
			var obj = $('#<%=_details.FindControl("_cuit").ClientID%>');
			if (obj)
				obj.mask("99-999999999-9");
		}
	</script>

</asp:Content>
<asp:Content ID="_content" ContentPlaceHolderID="_content" runat="server">
	<seti:SecureDetailsView ID="_details" DataKeyNames="Id" DataSourceID="_dataSource"
		runat="server" AutoGenerateRows="False" meta:resourcekey="Details">
		<Fields>
			<seti:BoundField DataField="Name" meta:resourcekey="NameField"></seti:BoundField>
			<seti:SecureTemplateField meta:resourcekey="CUITField">
				<InsertItemTemplate>
					<asp:TextBox ID="_cuit" runat="server" Text='<%# Bind("CUIT") %>' meta:resourcekey="CUITTextBox"></asp:TextBox>
					<asp:PropertyProxyValidator ID="_cuitVal" runat="server" PropertyName="CUIT"
						ControlToValidate="_cuit" SourceTypeName="Bagge.Seti.BusinessEntities.Customer"></asp:PropertyProxyValidator>
				</InsertItemTemplate>
				<EditItemTemplate>
					<asp:TextBox ID="_cuit" runat="server" Text='<%# Bind("CUIT") %>' meta:resourcekey="CUITTextBox"></asp:TextBox>
					<asp:PropertyProxyValidator ID="_cuitVal" runat="server" PropertyName="CUIT"
						ControlToValidate="_cuit" SourceTypeName="Bagge.Seti.BusinessEntities.Customer"></asp:PropertyProxyValidator>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Eval("CUIT")%>
				</ItemTemplate>
			</seti:SecureTemplateField>
			<seti:SecureTemplateField PropertyName="District" meta:resourcekey="CountryStateField">
				<InsertItemTemplate>
					<asp:DropDownList ID="_countryState" AutoPostBack="true" DataTextField="Name" DataValueField="Id"
						runat="server" meta:resourcekey="CountryStateDropDown" OnSelectedIndexChanged="_countryState_SelectedIndexChanged">
						<asp:ListItem Text="-- Seleccione --" Value=""></asp:ListItem>
					</asp:DropDownList>
				</InsertItemTemplate>
				<EditItemTemplate>
					<asp:DropDownList ID="_countryState" AutoPostBack="true" DataTextField="Name" DataValueField="Id"
						runat="server" meta:resourcekey="CountryStateDropDown" OnSelectedIndexChanged="_countryState_SelectedIndexChanged">
					</asp:DropDownList>
				</EditItemTemplate>
				<ItemTemplate>
					<%#((Bagge.Seti.BusinessEntities.Customer)Container.DataItem).District.CountryState.Name%>
				</ItemTemplate>
			</seti:SecureTemplateField>
			<seti:SecureTemplateField meta:resourcekey="DistrictField">
				<InsertItemTemplate>
					<asp:DropDownList ID="_district" AutoPostBack="true" DataTextField="Name" DataValueField="Id"
						runat="server" meta:resourcekey="DistrictDropDown" OnSelectedIndexChanged="_district_SelectedIndexChanged">
					</asp:DropDownList>
				</InsertItemTemplate>
				<EditItemTemplate>
					<asp:DropDownList ID="_district" AutoPostBack="true" DataTextField="Name" DataValueField="Id"
						runat="server" meta:resourcekey="DistrictDropDown" OnSelectedIndexChanged="_district_SelectedIndexChanged">
					</asp:DropDownList>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Eval("District")%>
				</ItemTemplate>
			</seti:SecureTemplateField>
			<seti:BoundField DataField="Address" meta:resourcekey="AddressField" />
			<seti:BoundField DataField="Floor" MaxLength="1" ControlStyle-Width="10" meta:resourcekey="FloorField" />
			<seti:BoundField DataField="Apartment" MaxLength="1" ControlStyle-Width="10" meta:resourcekey="ApartmentField" />
			<seti:SecureTemplateField PropertyName="ZipCode" meta:resourcekey="ZipCodeField">
				<InsertItemTemplate>
					<asp:TextBox ID="_zipCode" runat="server" Text='<%#Bind("ZipCode")%>'></asp:TextBox>
				</InsertItemTemplate>
				<EditItemTemplate>
					<asp:TextBox ID="_zipCode" runat="server" Text='<%#Bind("ZipCode")%>'></asp:TextBox>
				</EditItemTemplate>
				<ItemTemplate>
				<%#Eval("ZipCode")%>
				</ItemTemplate>
			</seti:SecureTemplateField>
			<seti:BoundField DataField="City" meta:resourcekey="CityField" />
			<seti:BoundField DataField="Phone" meta:resourcekey="PhoneField" />
			<seti:BoundField DataField="MobilePhone" meta:resourcekey="MobilePhoneField" />
			<seti:BoundField DataField="Email" meta:resourcekey="EmailField" />
			<asp:CheckBoxField DataField="Subscription" meta:resourcekey="SubscriptionField" />
		</Fields>
	</seti:SecureDetailsView>
	<seti:EditorControls ID="_commands" runat="server" AcceptPostBackUrl="~/CustomerList.aspx"
		CancelPostBackUrl="~/CustomerList.aspx" DetailsViewID="_details" meta:resourcekey="EditorCommands">
	</seti:EditorControls>
	<asp:ObjectContainerDataSource ID="_dataSource" runat="server"></asp:ObjectContainerDataSource>
</asp:Content>
