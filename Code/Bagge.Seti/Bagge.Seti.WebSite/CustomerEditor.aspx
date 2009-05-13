﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Editor.Master" AutoEventWireup="true"
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
			$("input[type=text]").each(function() {
				if (this.id.indexOf("CUIT_txt") > -1)
					$(this).mask("99-999999999-9");
			});
		}
	</script>

</asp:Content>
<asp:Content ID="_content" ContentPlaceHolderID="_content" runat="server">
	<seti:SecureDetailsView ID="_details" DataKeyNames="Id" DataSourceID="_dataSource"
		runat="server" AutoGenerateRows="False" meta:resourcekey="Details">
		<Fields>
			<seti:SecureBoundField ControlStyle-Width="320px" MaxLength="50" DataField="Name" meta:resourcekey="NameField">
			</seti:SecureBoundField>
			<seti:SecureBoundField DataField="CUIT" meta:resourcekey="CUITField">
				<Validators>
					<asp:CustomValidator id="_cuitUniqueVal" runat="server" OnServerValidate="_cuitUniqueVal_ServerValidate" meta:resourcekey="CUITUniqueValidator"></asp:CustomValidator>
				</Validators>
			</seti:SecureBoundField>
			<seti:SecureTemplateField PropertyName="District" meta:resourcekey="CountryStateField">
				<InsertItemTemplate>
					<asp:DropDownList ID="_countryState" AutoPostBack="true" DataTextField="Name" DataValueField="Id"
						runat="server" meta:resourcekey="CountryStateDropDown" OnSelectedIndexChanged="_countryState_SelectedIndexChanged">
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
					<asp:HiddenField ID="_district" runat="server" />
					<%#Eval("District")%>
				</ItemTemplate>
			</seti:SecureTemplateField>
			<seti:SecureBoundField ControlStyle-Width="320px" MaxLength="50" DataField="Address" meta:resourcekey="AddressField" />
			<seti:SecureBoundField DataField="Floor" MaxLength="1" ControlStyle-Width="10" meta:resourcekey="FloorField" />
			<seti:SecureBoundField DataField="Apartment" MaxLength="1" ControlStyle-Width="10" meta:resourcekey="ApartmentField" />
			<seti:SecureTemplateField PropertyName="ZipCode" meta:resourcekey="ZipCodeField">
				<InsertItemTemplate>
					<asp:TextBox ID="_zipCode" runat="server" Text='<%#Bind("ZipCode")%>' Width="80px" SkinID="customWidth"></asp:TextBox>
				</InsertItemTemplate>
				<EditItemTemplate>
					<asp:TextBox ID="_zipCode" runat="server" Text='<%#Bind("ZipCode")%>' Width="80px" SkinID="customWidth"></asp:TextBox>
				</EditItemTemplate>
				<ItemTemplate>
				<%#Eval("ZipCode")%>
				</ItemTemplate>
			</seti:SecureTemplateField>
			<seti:SecureBoundField ControlStyle-Width="320px" MaxLength="50" DataField="City" meta:resourcekey="CityField" />
			<seti:SecureBoundField ControlStyle-Width="120px" MaxLength="50" DataField="Phone" meta:resourcekey="PhoneField" />
			<seti:SecureBoundField ControlStyle-Width="120px" MaxLength="50" DataField="MobilePhone" meta:resourcekey="MobilePhoneField" />
			<seti:SecureBoundField ControlStyle-Width="320px" MaxLength="50" DataField="Email" meta:resourcekey="EmailField" />
			<seti:SecureCheckBoxField DataField="Subscription" meta:resourcekey="SubscriptionField" />
		</Fields>
	</seti:SecureDetailsView>
	<seti:EditorControls ID="_commands" runat="server" AcceptPostBackUrl="~/CustomerList.aspx"
		CancelPostBackUrl="~/CustomerList.aspx" DetailsViewID="_details" meta:resourcekey="EditorCommands">
	</seti:EditorControls>
	<asp:ObjectContainerDataSource ID="_dataSource" runat="server"></asp:ObjectContainerDataSource>
</asp:Content>
