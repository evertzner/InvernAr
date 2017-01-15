<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SeguridadUsuarios.aspx.vb" Inherits="InvernArTFI.SeguridadUsuarios" MasterPageFile="~/Master.Master" %>

<%@ MasterType VirtualPath="~/Master.Master" %>
<asp:Content ID="Main" runat="server" ContentPlaceHolderID="Main">
	<!DOCTYPE html>

	<html>
	<head>
		<script src="Scripts/jquery-1.9.1.min.js"></script>
		<script src="Scripts/bootstrap.min.js"></script>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<title></title>
	</head>
	<body>
		<div id="divSeguridadUsuariosControles" runat="server" class="container">
			<div class="vertical-center-row">
				<div class="row">
					<div class="col-md-12">
						<div class="scrolling-table-container">
							<asp:GridView ID="gvSeguridadUsuariosUsuarios" runat="server" CssClass="table table-striped table-bordered table-hover" 
								AllowPaging="True" PageSize="40">
								<Columns>
									<asp:CommandField ButtonType="Image" CancelImageUrl="~/images/14_Delete_16x16.png" CancelText="" DeleteText="" 
										EditImageUrl="~/images/05_Edit_16x16.png" EditText="" ShowEditButton="True" 
										UpdateImageUrl="~/images/15_Tick_16x16.png" UpdateText="">
										<ItemStyle Wrap="False" />
									</asp:CommandField>
									<asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" Visible="False" />
									<asp:BoundField DataField="DNI" HeaderText="DNI" SortExpression="DNI" Visible="false" />
									<asp:BoundField DataField="CUIT" HeaderText="CUIT" SortExpression="CUIT" Visible="false" />
									<asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" Visible="false" />
									<asp:BoundField DataField="Apellido" HeaderText="Apellido" SortExpression="Apellido" Visible="false" />
									<asp:TemplateField HeaderText="CorreoElectronico">
										<EditItemTemplate>
											<asp:TextBox ID="txtCorreoElectronico" runat="server" Text='<%# Bind("CorreoElectronico")%>'
												Width="300px"></asp:TextBox>
											<asp:RegularExpressionValidator ID="revCorreoElectronico" ErrorMessage="*" runat="server"
												ControlToValidate="txtCorreoElectronico"
												ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$"
												ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
											<asp:RequiredFieldValidator ID="rfvCorreoElectronico" runat="server" ErrorMessage="*"
												ControlToValidate="txtCorreoElectronico" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
										</EditItemTemplate>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# Bind("CorreoElectronico")%>' ID="lblCorreoElectronico"></asp:Label>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:BoundField DataField="Domicilio" HeaderText="Domicilio" SortExpression="Domicilio" Visible="false" />
									<asp:BoundField DataField="Localidad" HeaderText="Localidad" SortExpression="Localidad" Visible="false" />
									<asp:BoundField DataField="Provincia" HeaderText="Provincia" SortExpression="Provincia" Visible="false" />
									<asp:BoundField DataField="Telefono" HeaderText="Telefono" SortExpression="Telefono" Visible="false" />
									<asp:BoundField DataField="Interno" HeaderText="Interno" SortExpression="Interno" Visible="false" />
									<asp:BoundField DataField="TelefonoCelular" HeaderText="Telefono Celular" SortExpression="TelefonoCelular" Visible="false" />
									<asp:TemplateField HeaderText="Contraseña">
										<EditItemTemplate>
											<asp:TextBox ID="txtContraseña" runat="server" TextMode="Password" Text='<%# Bind("Contraseña") %>'></asp:TextBox>
										</EditItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="IntentosFallidos">
										<EditItemTemplate>
											<asp:TextBox ID="txtIntentosFallidos" runat="server" Text='<%# Bind("IntentosFallidos")%>' Width="30px"></asp:TextBox>
											<asp:RegularExpressionValidator ID="revIntentosFallidos" ErrorMessage="*" runat="server"
												ControlToValidate="txtIntentosFallidos" ValidationExpression="^[0-3]?$" ForeColor="Red"
												Font-Bold="true"></asp:RegularExpressionValidator>
											<asp:RequiredFieldValidator ID="rfvIntentosFallidos" runat="server" ErrorMessage="*"
												ControlToValidate="txtIntentosFallidos" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
										</EditItemTemplate>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# Bind("IntentosFallidos")%>' ID="lblIntentosFallidos"></asp:Label>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:CheckBoxField DataField="Bloqueado" HeaderText="Bloqueado" SortExpression="Bloqueado" />
									<asp:CheckBoxField DataField="Validado" HeaderText="Validado" SortExpression="Validado" />
									<asp:CheckBoxField DataField="Baja" HeaderText="Baja" SortExpression="Baja" />
								</Columns>
							</asp:GridView>
						</div>
					</div>
				</div>
			</div>
		</div>
	</body>
	</html>
</asp:Content>
