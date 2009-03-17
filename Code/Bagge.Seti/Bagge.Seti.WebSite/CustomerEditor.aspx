<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomerEditor.aspx.cs" Inherits="Bagge.Seti.WebSite.CustomerEditor" meta:resourcekey="Page" %>
<%@ Register Src="~/Controls/EditorCommands.ascx" TagPrefix="seti" TagName="EditorControls" %>
<asp:Content ID="_content" ContentPlaceHolderID="_content" runat="server">
	<seti:SecureDetailsView ID="_details" DataKeyNames="Id" DataSourceID="_dataSource" 
		runat="server" AutoGenerateRows="False" meta:resourcekey="Details"> 
		<Fields>
			<asp:TemplateField meta:resourcekey="NameField">
				<InsertItemTemplate>
					<asp:TextBox ID="_name" runat="server" Text='<%# Bind("Name") %>' meta:resourcekey="NameTextBox"></asp:TextBox>
					<asp:PropertyProxyValidator ID="_nameVal" runat="server" RulesetName="Rules" PropertyName="Name" ControlToValidate="_name" SourceTypeName="Bagge.Seti.BusinessEntities.Customer"></asp:PropertyProxyValidator>
				</InsertItemTemplate>
				<EditItemTemplate>
					<asp:TextBox ID="_name" runat="server" Text='<%# Bind("Name") %>' meta:resourcekey="NameTextBox"></asp:TextBox>
					<asp:PropertyProxyValidator ID="_nameVal" runat="server" RulesetName="Rules" PropertyName="Name" ControlToValidate="_name" SourceTypeName="Bagge.Seti.BusinessEntities.Customer"></asp:PropertyProxyValidator>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Eval("Name")%>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField meta:resourcekey="CUITField">
				<InsertItemTemplate>
					<asp:TextBox ID="_cuit" runat="server" Text='<%# Bind("CUIT") %>' meta:resourcekey="CUITTextBox"></asp:TextBox>
					<asp:PropertyProxyValidator ID="_cuitVal" runat="server" RulesetName="Rules" PropertyName="CUIT" ControlToValidate="_cuit" SourceTypeName="Bagge.Seti.BusinessEntities.Customer"></asp:PropertyProxyValidator>
					<ajax:MaskedEditExtender Id="_cuitMask" TargetControlID="_cuit" AutoComplete="true" ClearMaskOnLostFocus="false" Mask="99-999999999-9" runat="server"></ajax:MaskedEditExtender>
				</InsertItemTemplate>
				<EditItemTemplate>
					<asp:TextBox ID="_cuit" runat="server" Text='<%# Bind("CUIT") %>' meta:resourcekey="CUITTextBox"></asp:TextBox>
					<asp:PropertyProxyValidator ID="_cuitVal" runat="server" RulesetName="Rules" PropertyName="CUIT" ControlToValidate="_cuit" SourceTypeName="Bagge.Seti.BusinessEntities.Customer"></asp:PropertyProxyValidator>
					<ajax:MaskedEditExtender Id="_cuitMask" TargetControlID="_cuit" AutoComplete="true" ClearMaskOnLostFocus="false" Mask="99-999999999-9" runat="server"></ajax:MaskedEditExtender>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Eval("CUIT")%>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField meta:resourcekey="CountryStateField">
				<InsertItemTemplate>
					<asp:DropDownList ID="_countryState" AutoPostBack="true" DataTextField="Name" DataValueField="Id" runat="server" meta:resourcekey="CountryStateDropDown" OnSelectedIndexChanged="_countryState_SelectedIndexChanged">
						<asp:ListItem Text="-- Seleccione --" Value=""></asp:ListItem>
					</asp:DropDownList>
				</InsertItemTemplate>
				<EditItemTemplate>
					<asp:DropDownList ID="_countryState" AutoPostBack="true" DataTextField="Name" DataValueField="Id" runat="server" meta:resourcekey="CountryStateDropDown" OnSelectedIndexChanged="_countryState_SelectedIndexChanged"></asp:DropDownList>
				</EditItemTemplate>
				<ItemTemplate>
					<%#((Bagge.Seti.BusinessEntities.Customer)Container.DataItem).District.CountryState.Name%>
				</ItemTemplate>			
			</asp:TemplateField>
			<asp:TemplateField meta:resourcekey="DistrictField">
				<InsertItemTemplate>
					<asp:DropDownList ID="_district" AutoPostBack="true" DataTextField="Name" DataValueField="Id" runat="server" meta:resourcekey="DistrictDropDown" OnSelectedIndexChanged="_district_SelectedIndexChanged"></asp:DropDownList>
				</InsertItemTemplate>
				<EditItemTemplate>
					<asp:DropDownList ID="_district" AutoPostBack="true" DataTextField="Name" DataValueField="Id" runat="server" meta:resourcekey="DistrictDropDown" OnSelectedIndexChanged="_district_SelectedIndexChanged"></asp:DropDownList>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Eval("District")%>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField meta:resourcekey="AddressField">
				<InsertItemTemplate>
					<asp:TextBox ID="_address" runat="server" Text='<%# Bind("Address") %>' meta:resourcekey="AddressTextBox"></asp:TextBox>
				</InsertItemTemplate>
				<EditItemTemplate>
					<asp:TextBox ID="_address" runat="server" Text='<%# Bind("Address") %>' meta:resourcekey="AddressTextBox"></asp:TextBox>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Eval("Address")%>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField meta:resourcekey="FloorField">
				<InsertItemTemplate>
					<asp:TextBox ID="_floor" runat="server" Text='<%# Bind("Floor") %>' meta:resourcekey="FloorTextBox"></asp:TextBox>
				</InsertItemTemplate>
				<EditItemTemplate>
					<asp:TextBox ID="_floor" runat="server" Text='<%# Bind("Floor") %>' meta:resourcekey="FloorTextBox"></asp:TextBox>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Eval("Floor")%>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField meta:resourcekey="ApartmentField">
				<InsertItemTemplate>
					<asp:TextBox ID="_apartment" runat="server" Text='<%# Bind("Apartment") %>' meta:resourcekey="ApartmentTextBox"></asp:TextBox>
				</InsertItemTemplate>
				<EditItemTemplate>
					<asp:TextBox ID="_apartment" runat="server" Text='<%# Bind("Apartment") %>' meta:resourcekey="ApartmentTextBox"></asp:TextBox>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Eval("Apartment")%>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField meta:resourcekey="ZipCodeField">
				<InsertItemTemplate>
					<asp:TextBox ID="_zipCode" runat="server" Text='<%# Bind("ZipCode") %>' meta:resourcekey="ZipCodeTextBox"></asp:TextBox>
				</InsertItemTemplate>
				<EditItemTemplate>
					<asp:TextBox ID="_zipCode" runat="server" Text='<%# Bind("ZipCode") %>' meta:resourcekey="ZipCodeTextBox"></asp:TextBox>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Eval("ZipCode")%>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField meta:resourcekey="CityField">
				<InsertItemTemplate>
					<asp:TextBox ID="_city" runat="server" Text='<%# Bind("City") %>' meta:resourcekey="CityTextBox"></asp:TextBox>
				</InsertItemTemplate>
				<EditItemTemplate>
					<asp:TextBox ID="_city" runat="server" Text='<%# Bind("City") %>' meta:resourcekey="CityTextBox"></asp:TextBox>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Eval("City")%>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField meta:resourcekey="PhoneField">
				<InsertItemTemplate>
					<asp:TextBox ID="_phone" runat="server" Text='<%# Bind("Phone") %>' meta:resourcekey="PhoneTextBox"></asp:TextBox>
				</InsertItemTemplate>
				<EditItemTemplate>
					<asp:TextBox ID="_phone" runat="server" Text='<%# Bind("Phone") %>' meta:resourcekey="PhoneTextBox"></asp:TextBox>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Eval("Phone")%>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField meta:resourcekey="MobilePhoneField">
				<InsertItemTemplate>
					<asp:TextBox ID="_mobilePhone" runat="server" Text='<%# Bind("MobilePhone") %>' meta:resourcekey="MobilePhoneTextBox"></asp:TextBox>
				</InsertItemTemplate>
				<EditItemTemplate>
					<asp:TextBox ID="_mobilePhone" runat="server" Text='<%# Bind("MobilePhone") %>' meta:resourcekey="MobilePhoneTextBox"></asp:TextBox>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Eval("MobilePhone")%>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField meta:resourcekey="EmailField">
				<InsertItemTemplate>
					<asp:TextBox ID="_email" runat="server" Text='<%# Bind("Email") %>' meta:resourcekey="EmailTextBox"></asp:TextBox>
				</InsertItemTemplate>
				<EditItemTemplate>
					<asp:TextBox ID="_email" runat="server" Text='<%# Bind("Email") %>' meta:resourcekey="EmailTextBox"></asp:TextBox>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Eval("Email")%>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField meta:resourcekey="SubscriptionField">
				<InsertItemTemplate>
					<asp:CheckBox ID="_subscription" runat="server" Checked='<%# Bind("Subscription") %>' meta:resourcekey="SubscriptionCheckBox"></asp:CheckBox>
				</InsertItemTemplate>
				<EditItemTemplate>
					<asp:CheckBox ID="_subscription" runat="server" Checked='<%# Bind("Subscription") %>' meta:resourcekey="SubscriptionCheckBox"></asp:CheckBox>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Eval("Subscription")%>
				</ItemTemplate>
			</asp:TemplateField>
		</Fields>
	</seti:SecureDetailsView>
	<seti:EditorControls id="_commands" runat="server" AcceptPostBackUrl="~/CustomerList.aspx" CancelPostBackUrl="~/CustomerList.aspx" DetailsViewID="_details" meta:resourcekey="EditorCommands"></seti:EditorControls>
	<asp:ObjectContainerDataSource id="_dataSource" runat="server"></asp:ObjectContainerDataSource>
</asp:Content>
