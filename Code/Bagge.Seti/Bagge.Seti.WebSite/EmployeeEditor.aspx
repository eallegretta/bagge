<%@ Page Title="" Language="C#" MasterPageFile="~/Editor.Master" AutoEventWireup="true" CodeBehind="EmployeeEditor.aspx.cs" Inherits="Bagge.Seti.WebSite.EmployeeEditor" meta:resourcekey="Page" %>
<%@ Register Src="~/Controls/EditorCommands.ascx" TagPrefix="seti" TagName="EditorControls" %>
<asp:Content ContentPlaceHolderID="_head" ID="_head" runat="server">
</asp:Content>

<asp:Content ID="_content" ContentPlaceHolderID="_content" runat="server">
	<script type="text/javascript">
		onLoadFunctions["employeeEditor"] = function() {
			<%if(_details.FindControl("_usernameInvalidVal") != null){%>
				var display = $('#<%=_details.FindControl("_usernameInvalidVal").ClientID%>').css("display");
				var usernameTextBox = $('#<%=_details.FindControl("_username").ClientID%>');
				if(display && display != "none")
					usernameTextBox.addClass("error");
				else
					usernameTextBox.removeClass("error");
			<%}%>
		}	

	</script>
	<div id="employeeEditor">
	<seti:SecureDetailsView ID="_details" DataKeyNames="Id" DataSourceID="_dataSource"
		runat="server" AutoGenerateRows="False" meta:resourcekey="Details">
		<Fields>
			<seti:SecureTemplateField PropertyName="Username" meta:resourcekey="UsernameField">
				<EditItemTemplate>
					<asp:Literal ID="_domainUsernameHint" runat="server" meta:resourcekey="DomainUsernameHintLiteral"></asp:Literal>
					<asp:TextBox ID="_username" runat="server" MaxLength="50" Text='<%#Bind("Username")%>' ControlStyle-CssClass="textBox mediumData"></asp:TextBox>
					<asp:CustomValidator ID="_usernameUniqueVal" runat="server" ControlToValidate="_username" OnServerValidate="_usernameUniqueVal_ServerValidate" meta:resourcekey="UsernameUniqueValidator"></asp:CustomValidator>
					<asp:PlaceHolder ID="_windowsAuthUsernameConfig" runat="server">
						<asp:Button id="_validateUsername" runat="server" meta:resourcekey="ValidateUsernameButton" CausesValidation="false" />
						<asp:RequiredFieldValidator ID="_usernameReqVal" runat="server" ControlToValidate="_username" Display="Dynamic" meta:resourcekey="UsernameReqValidator"></asp:RequiredFieldValidator>
						<asp:CustomValidator ID="_usernameInvalidVal" runat="server" ControlToValidate="_username" Display="Static" EnableClientScript="false"  meta:resourcekey="UsernameInvalidValidator"></asp:CustomValidator>
					</asp:PlaceHolder>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Server.HtmlEncode(Eval("Username").ToString())%>
				</ItemTemplate>
			</seti:SecureTemplateField>
			<seti:SecureTemplateField PropertyName="Password" meta:resourcekey="PasswordField">
				<InsertItemTemplate>
					<asp:TextBox ID="_password" EnableTheming="false" runat="server" TextMode="Password" CssClass="textBox mediumData" ></asp:TextBox>
					<asp:RequiredFieldValidator ID="_passwordVal" runat="server" ControlToValidate="_password" meta:resourcekey="PasswordRequiredValidator"></asp:RequiredFieldValidator>
				</InsertItemTemplate>
				<EditItemTemplate>
					<asp:Label ID="_emptyPassword" runat="server" meta:resourcekey="EmptyPasswordLabel"></asp:Label><br />
					<asp:TextBox ID="_password" EnableTheming="false" runat="server" TextMode="Password" CssClass="textBox mediumData"></asp:TextBox>
				</EditItemTemplate>
				<ItemTemplate>
					<span class="password"></span>
				</ItemTemplate>
			</seti:SecureTemplateField>
			<seti:SecureTemplateField PropertyName="Password" meta:resourcekey="ConfirmPasswordField">
				<InsertItemTemplate>
					<asp:TextBox ID="_confirmPassword" EnableTheming="false" runat="server" TextMode="Password" CssClass="textBox mediumData"></asp:TextBox>
					<asp:RequiredFieldValidator ID="_confirmPasswordReqVal" runat="server" ControlToValidate="_confirmPassword" meta:resourcekey="ConfirmPasswordRequiredValidator"></asp:RequiredFieldValidator>
					<asp:CompareValidator ID="_confirmPasswordCmpVal" runat="server" ControlToValidate="_password" ControlToCompare="_confirmPassword" meta:resourcekey="ConfirmPasswordCompareValidator"></asp:CompareValidator>
				</InsertItemTemplate>
				<EditItemTemplate>
					<asp:TextBox ID="_confirmPassword" EnableTheming="false" runat="server" TextMode="Password" CssClass="textBox mediumData"></asp:TextBox>
					<asp:CompareValidator ID="_confirmPasswordCmpVal" runat="server" ControlToValidate="_password" ControlToCompare="_confirmPassword" meta:resourcekey="ConfirmPasswordCompareValidator"></asp:CompareValidator>
				</EditItemTemplate>
				<ItemTemplate>
					<span class="password"></span>
				</ItemTemplate>
			</seti:SecureTemplateField>
			<seti:SecureBoundField DataField="Firstname" meta:resourcekey="FirstnameField" ControlStyle-CssClass="textBox longData"></seti:SecureBoundField>
			<seti:SecureBoundField DataField="Lastname"  meta:resourcekey="LastnameField" ControlStyle-CssClass="textBox longData"></seti:SecureBoundField>
			<seti:SecureBoundField DataField="Phone"  meta:resourcekey="PhoneField" ControlStyle-CssClass="textBox mediumData"></seti:SecureBoundField>
			<seti:SecureBoundField DataField="EmergencyPhone"  meta:resourcekey="EmergencyPhoneField" ControlStyle-CssClass="textBox mediumData"></seti:SecureBoundField>
			<seti:SecureBoundField DataField="Email"  meta:resourcekey="EmailField" ControlStyle-CssClass="textBox longData"></seti:SecureBoundField>
			<seti:SecureTemplateField PropertyName="Category" meta:resourcekey="CategoryField">
				<EditItemTemplate>
					<asp:DropDownList ID="_categories" runat="server" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Server.HtmlEncode(Eval("Category").ToString())%>
				</ItemTemplate>
			</seti:SecureTemplateField>
			<seti:SecureTemplateField PropertyName="Roles" meta:resourcekey="RolesField">
				<EditItemTemplate>
					<asp:CheckBoxList ID="_roles" runat="server" DataTextField="Name" DataValueField="Id"></asp:CheckBoxList>
				</EditItemTemplate>
				<ItemTemplate>
					<asp:BulletedList ID="_roles" runat="server" DataTextField="Name"></asp:BulletedList>
				</ItemTemplate>
			</seti:SecureTemplateField>
		</Fields>
	</seti:SecureDetailsView>
	<seti:EditorControls ID="_commands" runat="server" AcceptPostBackUrl="~/EmployeeList.aspx"
		CancelPostBackUrl="~/EmployeeList.aspx" DetailsViewID="_details" meta:resourcekey="EditorCommands">
	</seti:EditorControls>
	<asp:ObjectContainerDataSource ID="_dataSource" runat="server" 
		DataObjectTypeName=""></asp:ObjectContainerDataSource>
	</div>
</asp:Content>

