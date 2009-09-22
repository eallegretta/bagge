<%@ Page Title="" Language="C#" MasterPageFile="~/Editor.Master" AutoEventWireup="true"
	CodeBehind="CustomerEditor.aspx.cs" Inherits="Bagge.Seti.WebSite.CustomerEditor"
	meta:resourcekey="Page" %>

<%@ Register Src="~/Controls/EditorCommands.ascx" TagPrefix="seti" TagName="EditorControls" %>
<asp:Content ID="_content" ContentPlaceHolderID="_content" runat="server">
	<seti:SecureDetailsView ID="_details" DataKeyNames="Id" DataSourceID="_dataSource"
		runat="server" AutoGenerateRows="false" meta:resourcekey="Details">
		<Fields>
			<seti:SecureBoundField ControlStyle-Width="320px" MaxLength="50" DataField="Name" meta:resourcekey="NameField">
			</seti:SecureBoundField>
			<seti:SecureBoundField DataField="CUIT" Mask="99-99999999-9" meta:resourcekey="CUITField">
				<Validators>
					<asp:CustomValidator id="_cuitUniqueVal" runat="server" OnServerValidate="_cuitUniqueVal_ServerValidate" meta:resourcekey="CUITUniqueValidator"></asp:CustomValidator>
				</Validators>
			</seti:SecureBoundField>
			<seti:SecureTemplateField PropertyName="District" meta:resourcekey="CountryStateField">
				<EditItemTemplate>
					<asp:DropDownList ID="_countryState" AutoPostBack="true" DataTextField="Name" DataValueField="Id"
						runat="server" meta:resourcekey="CountryStateDropDown">
					</asp:DropDownList>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Server.HtmlEncode(((Bagge.Seti.BusinessEntities.Customer)Container.DataItem).District.CountryState.Name)%>
				</ItemTemplate>
			</seti:SecureTemplateField>
			<seti:SecureTemplateField meta:resourcekey="DistrictField">
				<EditItemTemplate>
					<asp:DropDownList ID="_district" AutoPostBack="true" DataTextField="Name" DataValueField="Id"
						runat="server" meta:resourcekey="DistrictDropDown">
					</asp:DropDownList>	
				</EditItemTemplate>
				<ItemTemplate>
					<asp:HiddenField ID="_district" runat="server" />
					<%#Server.HtmlEncode(Eval("District").ToString())%>
				</ItemTemplate>
			</seti:SecureTemplateField>
			<seti:SecureBoundField ControlStyle-Width="320px" MaxLength="50" DataField="Address" meta:resourcekey="AddressField" />
			<seti:SecureBoundField DataField="Floor" MaxLength="1" ControlStyle-Width="10" meta:resourcekey="FloorField" />
			<seti:SecureBoundField DataField="Apartment" MaxLength="1" ControlStyle-Width="10" meta:resourcekey="ApartmentField" />
			<seti:SecureTemplateField PropertyName="ZipCode" meta:resourcekey="ZipCodeField">
				<EditItemTemplate>
					<asp:TextBox ID="_zipCode" runat="server" Text='<%#Bind("ZipCode")%>' Width="80px" SkinID="customWidth"></asp:TextBox>
				</EditItemTemplate>
				<ItemTemplate>
				<%#Server.HtmlEncode(Eval("ZipCode").ToString())%>
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
	<asp:ObjectDataSource ID="_ds" runat="server" DataObjectTypeName="Bagge.Seti.BusinessEntities.Customer" TypeName="Bagge.Seti.BusinessLogic.CustomerManager, Bagge.Seti.BusinessLogic"></asp:ObjectDataSource>
	<asp:ObjectContainerDataSource ID="_dataSource" runat="server"></asp:ObjectContainerDataSource>
</asp:Content>
