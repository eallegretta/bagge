<%@ Page Title="" Language="C#" MasterPageFile="~/Editor.Master" AutoEventWireup="true"
	CodeBehind="ProviderEditor.aspx.cs" Inherits="Bagge.Seti.WebSite.ProviderEditor"
	meta:resourcekey="Page" %>
<%@ Register TagName="ProductProviderGrid" TagPrefix="controls" Src="~/Controls/ProductProviderSelectionGrid.ascx"%>
<%@ Register Src="~/Controls/EditorCommands.ascx" TagPrefix="seti" TagName="EditorControls" %>
<asp:Content ID="_content" ContentPlaceHolderID="_contentNoUpdatePanel" runat="server">
	<seti:SecureDetailsView ID="_details" DataKeyNames="Id, AuditTimeStamp" DataSourceID="_dataSource"
		runat="server" AutoGenerateRows="False" meta:resourcekey="Details">
		<Fields>
			<seti:SecureBoundField ControlStyle-Width="320px" MaxLength="50" DataField="Name"
				meta:resourcekey="NameField">
			</seti:SecureBoundField>
			<seti:SecureBoundField DataField="CUIT" Mask="99-99999999-9" meta:resourcekey="CUITField">
				<Validators>
					<asp:CustomValidator ID="_cuitUniqueVal" runat="server" OnServerValidate="_cuitUniqueVal_ServerValidate"
						meta:resourcekey="CUITUniqueValidator"></asp:CustomValidator>
				</Validators>
			</seti:SecureBoundField>
			<seti:SecureTemplateField PropertyName="Calification" meta:resourcekey="CalificationField">
				<EditItemTemplate>
					<asp:DropDownList ID="_calification" AutoPostBack="true" DataTextField="Name" DataValueField="Id"
						runat="server" meta:resourcekey="CalificationDropDown">
					</asp:DropDownList>
				</EditItemTemplate>
				<ItemTemplate>
					<asp:HiddenField ID="_calification" runat="server" />
					<%#Server.HtmlEncode(Eval("Calification").ToString())%>
				</ItemTemplate>
			</seti:SecureTemplateField>
			<seti:SecureTemplateField PropertyName="District" meta:resourcekey="CountryStateField">
				<EditItemTemplate>
					<asp:DropDownList ID="_countryState" AutoPostBack="true" DataTextField="Name" DataValueField="Id"
						runat="server" meta:resourcekey="CountryStateDropDown" OnSelectedIndexChanged="_countryState_SelectedIndexChanged">
					</asp:DropDownList>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Server.HtmlEncode(((Bagge.Seti.BusinessEntities.Provider)Container.DataItem).District.CountryState.Name)%>
				</ItemTemplate>
			</seti:SecureTemplateField>
			<seti:SecureTemplateField PropertyName="District" meta:resourcekey="DistrictField">
				<EditItemTemplate>
					<asp:UpdatePanel ID="_districtUpdatePanel" runat="server" UpdateMode="Conditional" RenderMode="Inline">
					<ContentTemplate>
						<asp:DropDownList ID="_district" AutoPostBack="true" DataTextField="Name" DataValueField="Id"
						runat="server" meta:resourcekey="DistrictDropDown">
						</asp:DropDownList>	
					</ContentTemplate>
					<Triggers>
						<asp:AsyncPostBackTrigger ControlID="_countryState" EventName="SelectedIndexChanged" />
					</Triggers>
					</asp:UpdatePanel>
				</EditItemTemplate>
				<ItemTemplate>
					<asp:HiddenField ID="_district" runat="server" />
					<%#Server.HtmlEncode(Eval("District").ToString())%>
				</ItemTemplate>
			</seti:SecureTemplateField>
			<seti:SecureBoundField DataField="Company" ControlStyle-Width="300px" MaxLength="50" meta:resourcekey="CompanyField"></seti:SecureBoundField>
			<seti:SecureBoundField ControlStyle-Width="320px" MaxLength="50" DataField="Address"
				meta:resourcekey="AddressField" />
			<seti:SecureBoundField DataField="Floor" MaxLength="1" ControlStyle-Width="10" meta:resourcekey="FloorField" />
			<seti:SecureBoundField DataField="Apartment" MaxLength="1" ControlStyle-Width="10"
				meta:resourcekey="ApartmentField" />
			<seti:SecureTemplateField PropertyName="ZipCode" meta:resourcekey="ZipCodeField">
				<EditItemTemplate>
					<asp:UpdatePanel ID="_zipCodeUpdatePanel" runat="server" UpdateMode="Conditional" RenderMode="Inline">
					<ContentTemplate>
						<asp:TextBox ID="_zipCode" runat="server" Text='<%#Bind("ZipCode")%>' Width="80px" SkinID="customWidth"></asp:TextBox>
					</ContentTemplate>
						<Triggers>
							<asp:AsyncPostBackTrigger ControlID="_countryState" EventName="SelectedIndexChanged" />
							<asp:AsyncPostBackTrigger ControlID="_district" EventName="SelectedIndexChanged" />
						</Triggers>
					</asp:UpdatePanel>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Server.HtmlEncode(Eval("ZipCode") as string)%>
				</ItemTemplate>
			</seti:SecureTemplateField>
			<seti:SecureBoundField ControlStyle-Width="320px" MaxLength="50" DataField="PrimaryPhone"
				meta:resourcekey="PrimaryPhoneField" />
			<seti:SecureBoundField ControlStyle-Width="320px" MaxLength="50" DataField="SecondaryPhone"
				meta:resourcekey="SecondaryPhoneField" />
			<seti:SecureBoundField ControlStyle-Width="320px" MaxLength="50" DataField="Email"
				meta:resourcekey="EmailField" />
			<seti:SecureBoundField DataField="WebSite" ControlStyle-Width="320px" meta:resourcekey="WebSiteField"></seti:SecureBoundField>
			<seti:SecureBoundField DataField="ContactName" ControlStyle-Width="320px" meta:resourcekey="ContactNameField"></seti:SecureBoundField>
			<seti:SecureTemplateField PropertyName="Products" meta:resourcekey="ProductsField">
				<EditItemTemplate>
					<controls:ProductProviderGrid id="_products" runat="server" SourceType="Product" />					
				</EditItemTemplate>
				<ItemTemplate>
					<controls:ProductProviderGrid id="_products" runat="server" SourceType="Product" ReadOnly="true" />
				</ItemTemplate>
			</seti:SecureTemplateField>
		</Fields>
	</seti:SecureDetailsView>
	<seti:EditorControls ID="_commands" runat="server" AcceptPostBackUrl="~/ProviderList.aspx"
		CancelPostBackUrl="~/ProviderList.aspx" DetailsViewID="_details" meta:resourcekey="EditorCommands">
	</seti:EditorControls>
	<asp:ObjectContainerDataSource ID="_dataSource" runat="server" 
		DataObjectTypeName=""></asp:ObjectContainerDataSource>
</asp:Content>
