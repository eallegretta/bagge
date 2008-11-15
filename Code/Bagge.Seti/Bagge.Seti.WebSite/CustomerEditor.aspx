<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomerEditor.aspx.cs" Inherits="Bagge.Seti.WebSite.CustomerEditor" %>
<asp:Content ID="_content" ContentPlaceHolderID="_content" runat="server">
	<asp:DetailsView ID="_details" runat="server" AutoGenerateRows="false">
		<Fields>
			<asp:TemplateField HeaderText="Nombre">
				<InsertItemTemplate>
					<asp:TextBox ID="_name" runat="server" Text='<%#Bind("Name")%>'></asp:TextBox>
				</InsertItemTemplate>
				<EditItemTemplate>
					<asp:TextBox ID="_name" runat="server" Text='<%#Bind("Name")%>'></asp:TextBox>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Eval("Name")%>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="CUIT">
				<InsertItemTemplate>
					<asp:TextBox ID="_cuit" runat="server" Text='<%#Bind("CUIT")%>'></asp:TextBox>
				</InsertItemTemplate>
				<EditItemTemplate>
					<asp:TextBox ID="_cuit" runat="server" Text='<%#Bind("CUIT")%>'></asp:TextBox>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Eval("CUIT")%>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Direcci&oacute;n">
				<InsertItemTemplate>
					<asp:TextBox ID="_address" runat="server" Text='<%#Bind("Address")%>'></asp:TextBox>
				</InsertItemTemplate>
				<EditItemTemplate>
					<asp:TextBox ID="_address" runat="server" Text='<%#Bind("Address")%>'></asp:TextBox>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Eval("Name")%>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Piso">
				<InsertItemTemplate>
					<asp:TextBox ID="_floor" runat="server" Text='<%#Bind("Floor")%>'></asp:TextBox>
				</InsertItemTemplate>
				<EditItemTemplate>
					<asp:TextBox ID="_floor" runat="server" Text='<%#Bind("Floor")%>'></asp:TextBox>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Eval("Floor")%>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Departamento">
				<InsertItemTemplate>
					<asp:TextBox ID="_apartment" runat="server" Text='<%#Bind("Apartment")%>'></asp:TextBox>
				</InsertItemTemplate>
				<EditItemTemplate>
					<asp:TextBox ID="_apartment" runat="server" Text='<%#Bind("Apartment")%>'></asp:TextBox>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Eval("Apartment")%>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Codigo Postal">
				<InsertItemTemplate>
					<asp:TextBox ID="_zipCode" runat="server" Text='<%#Bind("ZipCode")%>'></asp:TextBox>
				</InsertItemTemplate>
				<EditItemTemplate>
					<asp:TextBox ID="_zipCode" runat="server" Text='<%#Bind("ZipCode")%>'></asp:TextBox>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Eval("ZipCode")%>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Ciudad">
				<InsertItemTemplate>
					<asp:TextBox ID="_city" runat="server" Text='<%#Bind("City")%>'></asp:TextBox>
				</InsertItemTemplate>
				<EditItemTemplate>
					<asp:TextBox ID="_city" runat="server" Text='<%#Bind("City")%>'></asp:TextBox>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Eval("City")%>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Tel&eacute;fono">
				<InsertItemTemplate>
					<asp:TextBox ID="_phone" runat="server" Text='<%#Bind("Phone")%>'></asp:TextBox>
				</InsertItemTemplate>
				<EditItemTemplate>
					<asp:TextBox ID="_phone" runat="server" Text='<%#Bind("Phone")%>'></asp:TextBox>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Eval("Phone")%>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Celular">
				<InsertItemTemplate>
					<asp:TextBox ID="_mobilePhone" runat="server" Text='<%#Bind("MobilePhone")%>'></asp:TextBox>
				</InsertItemTemplate>
				<EditItemTemplate>
					<asp:TextBox ID="_mobilePhone" runat="server" Text='<%#Bind("MobilePhone")%>'></asp:TextBox>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Eval("MobilePhone")%>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Email">
				<InsertItemTemplate>
					<asp:TextBox ID="_email" runat="server" Text='<%#Bind("Email")%>'></asp:TextBox>
				</InsertItemTemplate>
				<EditItemTemplate>
					<asp:TextBox ID="_email" runat="server" Text='<%#Bind("Email")%>'></asp:TextBox>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Eval("Email")%>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Subscripci&oacute;n">
				<InsertItemTemplate>
					<asp:TextBox ID="_subscription" runat="server" Text='<%#Bind("Subscription")%>'></asp:TextBox>
				</InsertItemTemplate>
				<EditItemTemplate>
					<asp:TextBox ID="_subscription" runat="server" Text='<%#Bind("Subscription")%>'></asp:TextBox>
				</EditItemTemplate>
				<ItemTemplate>
					<%#Eval("Subscription")%>
				</ItemTemplate>
			</asp:TemplateField>
		</Fields>
	</asp:DetailsView>
</asp:Content>
