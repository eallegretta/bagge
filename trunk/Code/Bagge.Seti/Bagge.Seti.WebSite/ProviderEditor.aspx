<%@ Page Title="" Language="C#" MasterPageFile="~/Editor.Master" AutoEventWireup="true"
	CodeBehind="ProviderEditor.aspx.cs" Inherits="Bagge.Seti.WebSite.ProviderEditor"
	meta:resourcekey="Page" %>
<%@ Register TagName="ProductProviderGrid" TagPrefix="controls" Src="~/Controls/ProductProviderSelectionGrid.ascx"%>
<%@ Register Src="~/Controls/EditorCommands.ascx" TagPrefix="seti" TagName="EditorControls" %>
<asp:Content ID="_head" ContentPlaceHolderID="_head" runat="server">

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
			<seti:SecureBoundField ControlStyle-Width="320px" MaxLength="50" DataField="Name"
				meta:resourcekey="NameField">
			</seti:SecureBoundField>
			<seti:SecureBoundField DataField="CUIT" meta:resourcekey="CUITField">
				<Validators>
					<asp:CustomValidator ID="_cuitUniqueVal" runat="server" OnServerValidate="_cuitUniqueVal_ServerValidate"
						meta:resourcekey="CUITUniqueValidator"></asp:CustomValidator>
				</Validators>
			</seti:SecureBoundField>
			<seti:SecureTemplateField PropertyName="Calification" meta:resourcekey="CalificationField">
				<InsertItemTemplate>
					<asp:DropDownList ID="_calification" AutoPostBack="true" DataTextField="Name" DataValueField="Id"
						runat="server" meta:resourcekey="CalificationDropDown">
					</asp:DropDownList>
				</InsertItemTemplate>
				<EditItemTemplate>
					<asp:DropDownList ID="_calification" AutoPostBack="true" DataTextField="Name" DataValueField="Id"
						runat="server" meta:resourcekey="CalificationDropDown">
					</asp:DropDownList>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Eval("Calification")%>
				</ItemTemplate>
			</seti:SecureTemplateField>
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
			<seti:SecureTemplateField PropertyName="District" meta:resourcekey="DistrictField">
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
			<seti:SecureBoundField DataField="Company" ControlStyle-Width="300px" MaxLength="50" meta:resourcekey="CompanyField"></seti:SecureBoundField>
			<seti:SecureBoundField ControlStyle-Width="320px" MaxLength="50" DataField="Address"
				meta:resourcekey="AddressField" />
			<seti:SecureBoundField DataField="Floor" MaxLength="1" ControlStyle-Width="10" meta:resourcekey="FloorField" />
			<seti:SecureBoundField DataField="Apartment" MaxLength="1" ControlStyle-Width="10"
				meta:resourcekey="ApartmentField" />
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
			<seti:SecureBoundField ControlStyle-Width="320px" MaxLength="50" DataField="City"
				meta:resourcekey="CityField" />
			<seti:SecureBoundField ControlStyle-Width="120px" MaxLength="50" DataField="PrimaryPhone"
				meta:resourcekey="PrimaryPhoneField" />
			<seti:SecureBoundField ControlStyle-Width="120px" MaxLength="50" DataField="SecondaryPhone"
				meta:resourcekey="SecondaryPhoneField" />
			<seti:SecureBoundField ControlStyle-Width="320px" MaxLength="50" DataField="Email"
				meta:resourcekey="EmailField" />
			<seti:SecureBoundField DataField="WebSite" ControlStyle-Width="320px" meta:resourcekey="WebSiteField"></seti:SecureBoundField>
			<seti:SecureBoundField DataField="ContactName" ControlStyle-Width="320px" meta:resourcekey="ContactNameField"></seti:SecureBoundField>
			<seti:SecureTemplateField PropertyName="Products" meta:resourcekey="ProductsField">
				<InsertItemTemplate>
					<controls:ProductProviderGrid id="_products" runat="server" SourceType="Product" />					
				</InsertItemTemplate>
				<EditItemTemplate>
					<controls:ProductProviderGrid id="_products" runat="server" SourceType="Product" />					
				</EditItemTemplate>
				<ItemTemplate>
					<controls:ProductProviderGrid id="_products" runat="server" SourceType="Product" ReadOnly="true" />
				</ItemTemplate>
			</seti:SecureTemplateField>
		</Fields>
	</seti:SecureDetailsView>
	<seti:EditorControls ID="_commands" runat="server" AcceptPostBackUrl="~/CustomerList.aspx"
		CancelPostBackUrl="~/CustomerList.aspx" DetailsViewID="_details" meta:resourcekey="EditorCommands">
	</seti:EditorControls>
	<asp:ObjectContainerDataSource ID="_dataSource" runat="server" 
		DataObjectTypeName=""></asp:ObjectContainerDataSource>
</asp:Content>
