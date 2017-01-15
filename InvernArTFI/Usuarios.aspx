<%@ Page Language="vb" AutoEventWireup="true" CodeBehind="Usuarios.aspx.vb" Inherits="InvernArTFI.Usuarios" MasterPageFile="~/Master.Master" %>

<%@ MasterType VirtualPath="~/Master.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
		<div id="divUsuariosControles" runat="server" class="container">
			<div id="wrapper">
				<div id="sidebar-wrapper">
					<ul class="sidebar-nav">
						<li id="liUsuariosUsuarios" runat="server" class="sidebar-brand"></li>
						<li>
							<a id="aUsuariosAlta" runat="server" href="#"></a>
						</li>
						<li>
							<a id="aUsuariosListado" runat="server" href="#"></a>
						</li>
					</ul>
				</div>
				<div id="divUsuariosAlta" class="container" runat="server">
					<div class="vertical-center-row">
						<div class="row">
							<div class="col-md-2">
								<div class="form-group">
									<label id="lblUsuariosDNI" runat="server" class="control-label" for="txtUsuariosDNI"></label>
									<input class="form-control" id="txtUsuariosDNI" runat="server" placeholder="Ej. 12345678">
								</div>
							</div>
							<div class="col-md-10" style="padding-top: 25px;">
								<div class="form-group">
									<asp:RegularExpressionValidator ID="revDNI" runat="server" ControlToValidate="txtUsuariosDNI"
										ValidationExpression="^((\d{1,8}))$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-2">
								<div class="form-group">
									<label id="lblUsuariosCUIT" runat="server" class="control-label" for="txtUsuariosCUIT"></label>
									<input class="form-control" id="txtUsuariosCUIT" runat="server" placeholder="Ej. 12-12345678-1">
								</div>
							</div>
							<div class="col-md-10" style="padding-top: 25px;">
								<div class="form-group">
									<asp:RegularExpressionValidator ID="revCUIT" runat="server" ControlToValidate="txtUsuariosCUIT"
										ValidationExpression="^(\d{1,2}-)+(\d{1,8}-)+(\d{1,1})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-3">
								<div class="form-group">
									<label id="lblUsuariosNombre" runat="server" class="control-label" for="txtUsuariosNombre"></label>
									<input class="form-control" id="txtUsuariosNombre" runat="server">
								</div>
							</div>
							<div class="col-md-9" style="padding-top: 25px;">
								<div class="form-group">
									<asp:RegularExpressionValidator ID="revNombre" runat="server" ControlToValidate="txtUsuariosNombre"
										ValidationExpression="^([\D\s]{1,50})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-3">
								<div class="form-group">
									<label id="lblUsuariosApellido" runat="server" class="control-label" for="txtUsuariosApellido"></label>
									<input class="form-control" id="txtUsuariosApellido" runat="server">
								</div>
							</div>
							<div class="col-md-9" style="padding-top: 25px;">
								<div class="form-group">
									<asp:RegularExpressionValidator ID="revApellido" runat="server" ControlToValidate="txtUsuariosApellido"
										ValidationExpression="^([\D\s]{1,50})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-4">
								<div class="form-group">
									<label id="lblUsuariosDomicilio" runat="server" class="control-label" for="txtUsuariosDomicilio"></label>
									<input class="form-control" id="txtUsuariosDomicilio" runat="server">
								</div>
							</div>
							<div class="col-md-8" style="padding-top: 25px;">
								<div class="form-group">
									<asp:RegularExpressionValidator ID="revDomicilio" runat="server" ControlToValidate="txtUsuariosDomicilio"
										ValidationExpression="^([\w\s\.]{1,80})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-3">
								<div class="form-group">
									<label id="lblUsuariosLocalidad" runat="server" class="control-label" for="txtUsuariosLocalidad"></label>
									<input class="form-control" id="txtUsuariosLocalidad" runat="server">
								</div>
							</div>
							<div class="col-md-9" style="padding-top: 25px;">
								<div class="form-group">
									<asp:RegularExpressionValidator ID="revLocalidad" runat="server" ControlToValidate="txtUsuariosLocalidad"
										ValidationExpression="^([\w\s\.]{1,50})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-3">
								<div class="form-group">
									<label id="lblUsuariosProvincia" runat="server" class="control-label" for="txtUsuariosProvincia"></label>
									<input class="form-control" id="txtUsuariosProvincia" runat="server">
								</div>
							</div>
							<div class="col-md-9" style="padding-top: 25px;">
								<div class="form-group">
									<asp:RegularExpressionValidator ID="revProvincia" runat="server" ControlToValidate="txtUsuariosProvincia"
										ValidationExpression="^([\w\s\.]{1,50})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-2">
								<div class="form-group">
									<label id="lblUsuariosTelefono" runat="server" class="control-label" for="txtUsuariosTelefono"></label>
									<input class="form-control" id="txtUsuariosTelefono" runat="server" placeholder="011-1234-5678">
								</div>
							</div>
							<div class="col-md-10" style="padding-top: 25px;">
								<div class="form-group">
									<asp:RegularExpressionValidator ID="revTelefono" runat="server" ControlToValidate="txtUsuariosTelefono"
										ValidationExpression="^([\d]{2,3}-[\d]{3,4}-[\d]{3,4})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-2">
								<div class="form-group">
									<label id="lblUsuariosInterno" runat="server" class="control-label" for="txtUsuariosInterno"></label>
									<input class="form-control" id="txtUsuariosInterno" runat="server" placeholder="Ej. 12345">
								</div>
							</div>
							<div class="col-md-10" style="padding-top: 25px;">
								<div class="form-group">
									<asp:RegularExpressionValidator ID="revInterno" runat="server" ControlToValidate="txtUsuariosInterno"
										ValidationExpression="^([\d]{1,10})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-2">
								<div class="form-group">
									<label id="lblUsuariosTelefonoCelular" runat="server" class="control-label" for="txtUsuariosTelefonoCelular"></label>
									<input class="form-control" id="txtUsuariosTelefonoCelular" runat="server" placeholder="Ej. 011-15-1234-5678">
								</div>
							</div>
							<div class="col-md-10" style="padding-top: 25px;">
								<div class="form-group">
									<asp:RegularExpressionValidator ID="revTelefonoCelular" runat="server"
										ControlToValidate="txtUsuariosTelefonoCelular"
										ValidationExpression="^([\d]{2,3}-(15)-[\d]{3,4}-[\d]{3,4})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-3">
								<div class="form-group">
									<label id="lblUsuariosCorreoElectronico" runat="server" class="control-label" for="txtUsuariosCorreoElectronico"></label>
									<input class="form-control" id="txtUsuariosCorreoElectronico" runat="server">
								</div>
							</div>
							<div class="col-md-9" style="padding-top: 25px;">
								<div class="form-group">
									<asp:RegularExpressionValidator ID="revCorreoElectronico" runat="server"
										ControlToValidate="txtUsuariosCorreoElectronico"
										ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$" ForeColor="Red" Font-Bold="true">
									</asp:RegularExpressionValidator>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-3">
								<div class="form-group">
									<label id="lblUsuariosCorreoElectronicoRepetido" runat="server" class="control-label" for="txtUsuariosCorreoElectronicoRepetido"></label>
									<input class="form-control" id="txtUsuariosCorreoElectronicoRepetido" runat="server">
								</div>
							</div>
							<div class="col-md-9" style="padding-top: 25px;">
								<div class="form-group">
									<asp:RegularExpressionValidator ID="revCorreoElectronicoRepetido" runat="server"
										ControlToValidate="txtUsuariosCorreoElectronicoRepetido"
										ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$" ForeColor="Red" Font-Bold="true">
									</asp:RegularExpressionValidator>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-2">
								<div class="form-group">
									<label id="lblUsuariosPassNueva" runat="server" class="control-label" for="txtUsuariosPassNueva"></label>
									<input class="form-control" id="txtUsuariosPassNueva" runat="server" type="password">
								</div>
							</div>
							<div class="col-md-10" style="padding-top: 25px;">
								<div class="form-group">
									<asp:RegularExpressionValidator ID="revPassNueva" runat="server" ControlToValidate="txtUsuariosPassNueva"
										ValidationExpression="^([A-Za-z0-9]{8,35})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-2">
								<div class="form-group">
									<label id="lblUsuariosPassNuevaRepetida" runat="server" class="control-label" for="txtUsuariosPassNuevaRepetida"></label>
									<input class="form-control" id="txtUsuariosPassNuevaRepetida" runat="server" type="password">
								</div>
							</div>
							<div class="col-md-10" style="padding-top: 25px;">
								<div class="form-group">
									<asp:RegularExpressionValidator ID="revPassNuevaRepetida" runat="server"
										ControlToValidate="txtUsuariosPassNuevaRepetida"
										ValidationExpression="^([A-Za-z0-9]{8,35})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-3">
								<div class="form-group">
									<label id="lblUsuariosRoles" runat="server" class="control-label" for="ddlUsuariosRoles"></label>
									<asp:DropDownList ID="ddlUsuariosRoles" class="form-control" runat="server"></asp:DropDownList>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-3">
								<div class="form-group">
									<button id="btnUsuariosConfirmar" runat="server" type="button" class="btn btn-success"></button>
								</div>
							</div>
						</div>
						<%--	<div class="row">
							<div class="col-md-5">
								<div id="divUsuariosAlerta" runat="server" role="alert"></div>
							</div>
						</div>--%>
					</div>
				</div>
				<div id="divUsuariosListado" class="container" runat="server">
					<div class="vertical-center-row">
						<div class="row">
							<div class="col-md-12">
								<div class="scrolling-table-container" style="width: 1000px;">
									<asp:GridView ID="gvUsuariosUsuarios" runat="server" CssClass="table table-striped table-bordered table-hover" AllowPaging="True" PageSize="20">
										<Columns>
											<asp:CommandField ButtonType="Image" CancelImageUrl="~/images/14_Delete_16x16.png" CancelText="" DeleteText="" EditImageUrl="~/images/05_Edit_16x16.png" EditText="" ShowEditButton="True" UpdateImageUrl="~/images/15_Tick_16x16.png" UpdateText="">
												<ItemStyle Wrap="False" />
											</asp:CommandField>
											<asp:TemplateField>
												<ItemTemplate>
													<asp:ImageButton ID="btnUsuariosEliminar" CommandArgument='<%# Eval("Id")%>' OnClick="btnUsuariosEliminar_Click" runat="server" Text="" ImageUrl="~/images/14_Delete_16x16.png" AlternateText="Eliminar"></asp:ImageButton>
													<cc1:ConfirmButtonExtender ID="cbe" runat="server" DisplayModalPopupID="mpe" TargetControlID="btnUsuariosEliminar"></cc1:ConfirmButtonExtender>
													<cc1:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="btnUsuariosEliminar" OkControlID="btnYes"
														CancelControlID="btnNo" BackgroundCssClass="modalBackground">
													</cc1:ModalPopupExtender>
													<asp:Panel ID="pnlPopup" runat="server" CssClass="modal-dialog" Style="display: none">
														<div class="modal-content">
															<div class="modal-header">
																Confirmación
															</div>
															<div class="modal-body">
																¿Está seguro que desea eliminar el cliente?
															</div>
															<div class="modal-footer">
																<asp:Button ID="btnYes" CssClass="btn btn-primary btn-success" runat="server" Text="Si" />
																<asp:Button ID="btnNo" CssClass="btn btn-primary btn-danger" runat="server" Text="No" />
															</div>
														</div>
													</asp:Panel>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" Visible="False" />
											<asp:TemplateField HeaderText="DNI" ItemStyle-Wrap="false">
												<EditItemTemplate>
													<asp:TextBox ID="txtDNI" runat="server" Text='<%# Bind("DNI")%>'></asp:TextBox>
													<asp:RegularExpressionValidator ID="revDNI2" ErrorMessage="*" runat="server" ControlToValidate="txtDNI"
														ValidationExpression="^((\d{1,8}))$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
													<asp:RequiredFieldValidator ID="rfvDNI" runat="server" ErrorMessage="*"
														ControlToValidate="txtDNI" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
												</EditItemTemplate>
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# Bind("DNI")%>' ID="lblDNI"></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="CUIT" ItemStyle-Wrap="false">
												<EditItemTemplate>
													<asp:TextBox ID="txtCUIT" runat="server" Text='<%# Bind("CUIT")%>'></asp:TextBox>
													<asp:RegularExpressionValidator ID="revCUIT2" ErrorMessage="*" runat="server" ControlToValidate="txtCUIT"
														ValidationExpression="^(\d{1,2}-)+(\d{1,8}-)+(\d{1,1})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
													<asp:RequiredFieldValidator ID="rfvCUIT" runat="server" ErrorMessage="*"
														ControlToValidate="txtCUIT" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
												</EditItemTemplate>
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# Bind("CUIT")%>' ID="lblCUIT"></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Nombre" ItemStyle-Wrap="false">
												<EditItemTemplate>
													<asp:TextBox ID="txtNombre" runat="server" Text='<%# Bind("Nombre")%>'></asp:TextBox>
													<asp:RegularExpressionValidator ID="revNombre2" ErrorMessage="*" runat="server" ControlToValidate="txtNombre"
														ValidationExpression="^([\D\s]{1,50})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
													<asp:RequiredFieldValidator ID="rfvNombre" runat="server" ErrorMessage="*"
														ControlToValidate="txtNombre" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
												</EditItemTemplate>
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# Bind("Nombre")%>' ID="lblNombre"></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Apellido" ItemStyle-Wrap="false">
												<EditItemTemplate>
													<asp:TextBox ID="txtApellido" runat="server" Text='<%# Bind("Apellido")%>'></asp:TextBox>
													<asp:RegularExpressionValidator ID="revApellido2" ErrorMessage="*" runat="server" ControlToValidate="txtApellido"
														ValidationExpression="^([\D\s]{1,50})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
													<asp:RequiredFieldValidator ID="rfvApellido" runat="server" ErrorMessage="*"
														ControlToValidate="txtApellido" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
												</EditItemTemplate>
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# Bind("Apellido")%>' ID="lblApellido"></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="CorreoElectronico" ItemStyle-Wrap="false">
												<EditItemTemplate>
													<asp:TextBox ID="txtCorreoElectronico" runat="server" Text='<%# Bind("CorreoElectronico")%>'></asp:TextBox>
													<asp:RegularExpressionValidator ID="revCorreoElectronico2" ErrorMessage="*" runat="server" ControlToValidate="txtCorreoElectronico"
														ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
													<asp:RequiredFieldValidator ID="rfvCorreoElectronico" runat="server" ErrorMessage="*"
														ControlToValidate="txtCorreoElectronico" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
												</EditItemTemplate>
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# Bind("CorreoElectronico")%>' ID="lblCorreoElectronico"></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Domicilio" ItemStyle-Wrap="false">
												<EditItemTemplate>
													<asp:TextBox ID="txtDomicilio" runat="server" Text='<%# Bind("Domicilio")%>'></asp:TextBox>
													<asp:RegularExpressionValidator ID="revDomicilio2" ErrorMessage="*" runat="server" ControlToValidate="txtDomicilio"
														ValidationExpression="^([\w\s\.]{1,80})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
													<asp:RequiredFieldValidator ID="rfvDomicilio" runat="server" ErrorMessage="*"
														ControlToValidate="txtDomicilio" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
												</EditItemTemplate>
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# Bind("Domicilio")%>' ID="lblDomicilio"></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Localidad" ItemStyle-Wrap="false">
												<EditItemTemplate>
													<asp:TextBox ID="txtLocalidad" runat="server" Text='<%# Bind("Localidad")%>'></asp:TextBox>
													<asp:RegularExpressionValidator ID="revLocalidad2" ErrorMessage="*" runat="server" ControlToValidate="txtLocalidad"
														ValidationExpression="^([\w\s\.]{1,50})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
													<asp:RequiredFieldValidator ID="rfvLocalidad" runat="server" ErrorMessage="*"
														ControlToValidate="txtLocalidad" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
												</EditItemTemplate>
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# Bind("Localidad")%>' ID="lblLocalidad"></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Provincia" ItemStyle-Wrap="false">
												<EditItemTemplate>
													<asp:TextBox ID="txtProvincia" runat="server" Text='<%# Bind("Provincia")%>'></asp:TextBox>
													<asp:RegularExpressionValidator ID="revProvincia2" ErrorMessage="*" runat="server" ControlToValidate="txtProvincia"
														ValidationExpression="^([\w\s\.]{1,50})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
													<asp:RequiredFieldValidator ID="rfvProvincia" runat="server" ErrorMessage="*"
														ControlToValidate="txtProvincia" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
												</EditItemTemplate>
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# Bind("Provincia")%>' ID="lblProvincia"></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Telefono" ItemStyle-Wrap="false">
												<EditItemTemplate>
													<asp:TextBox ID="txtTelefono" runat="server" Text='<%# Bind("Telefono")%>'></asp:TextBox>
													<asp:RegularExpressionValidator ID="revTelefono2" ErrorMessage="*" runat="server" ControlToValidate="txtTelefono"
														ValidationExpression="^([\d]{2,3}-[\d]{3,4}-[\d]{3,4})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
													<asp:RequiredFieldValidator ID="rfvTelefono" runat="server" ErrorMessage="*"
														ControlToValidate="txtTelefono" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
												</EditItemTemplate>
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# Bind("Telefono")%>' ID="lblTelefono"></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Interno" ItemStyle-Wrap="false">
												<EditItemTemplate>
													<asp:TextBox ID="txtInterno" runat="server" Text='<%# Bind("Interno")%>'></asp:TextBox>
													<asp:RegularExpressionValidator ID="revInterno2" ErrorMessage="*" runat="server" ControlToValidate="txtInterno"
														ValidationExpression="^([\d]{1,10})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
												</EditItemTemplate>
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# Bind("Interno")%>' ID="lblInterno"></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="TelefonoCelular" ItemStyle-Wrap="false">
												<EditItemTemplate>
													<asp:TextBox ID="txtTelefonoCelular" runat="server" Text='<%# Bind("TelefonoCelular")%>'></asp:TextBox>
													<asp:RegularExpressionValidator ID="revTelefonoCelular2" ErrorMessage="*" runat="server" ControlToValidate="txtTelefonoCelular"
														ValidationExpression="^([\d]{2,3}-(15)-[\d]{3,4}-[\d]{3,4})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
												</EditItemTemplate>
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# Bind("TelefonoCelular")%>' ID="lblTelefonoCelular"></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:BoundField DataField="Contraseña" HeaderText="Contraseña" SortExpression="Contraseña" Visible="False" />
											<asp:BoundField DataField="IntentosFallidos" HeaderText="Intentos Fallidos" SortExpression="IntentosFallidos" Visible="False" />
											<asp:BoundField DataField="Bloqueado" HeaderText="Bloqueado" SortExpression="Bloqueado" Visible="False" />
											<asp:BoundField DataField="Validado" HeaderText="Validado" SortExpression="Validado" Visible="False" />
											<asp:BoundField DataField="Baja" HeaderText="Baja" SortExpression="Baja" Visible="False" />
										</Columns>
									</asp:GridView>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	</body>
	</html>
</asp:Content>
