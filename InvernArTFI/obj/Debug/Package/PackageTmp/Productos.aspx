<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Productos.aspx.vb" Inherits="InvernArTFI.Productos" MasterPageFile="~/Master.Master" %>

<%@ MasterType VirtualPath="~/Master.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Main" runat="server" ContentPlaceHolderID="Main">
	<!DOCTYPE html>

	<html>
	<head>
		<script src="Scripts/jquery-1.9.1.min.js"></script>
		<script src="Scripts/bootstrap.min.js"></script>
		<link href="Content/simple-sidebar.css" rel="stylesheet" />
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<title></title>
	</head>
	<body>
		<div id="divProductosControles" runat="server" class="container">
			<div id="wrapper">
				<div id="sidebar-wrapper">
					<ul class="sidebar-nav">
						<li id="liProductosProductos" runat="server" class="sidebar-brand"></li>
						<li>
							<a id="aProductosAlta" runat="server" href="#"></a>
						</li>
						<li>
							<a id="aProductosListado" runat="server" href="#"></a>
						</li>
					</ul>
				</div>
				<div id="divProductosAlta" class="container" runat="server">
					<div class="vertical-center-row">
						<div class="row">
							<div class="col-md-2">
								<div class="form-group">
									<label id="lblProductosCodigo" runat="server" class="control-label" for="txtProductosCodigo"></label>
									<input class="form-control" id="txtProductosCodigo" runat="server">
								</div>
							</div>
							<div class="col-md-10" style="padding-top: 25px;">
								<div class="form-group">
									<asp:RegularExpressionValidator ID="revCodigoProducto" runat="server" ControlToValidate="txtProductosCodigo"
										ValidationExpression="^[a-zA-Z0-9]{1,10}$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-4">
								<div class="form-group">
									<label id="lblProductosNombre" runat="server" class="control-label" for="txtProductosNombre"></label>
									<input class="form-control" id="txtProductosNombre" runat="server">
								</div>
							</div>
							<div class="col-md-8" style="padding-top: 25px;">
								<div class="form-group">
									<asp:RegularExpressionValidator ID="revNombreProducto" runat="server" ControlToValidate="txtProductosNombre"
										ValidationExpression="^([\w\s\.]{1,100})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-7">
								<div class="form-group">
									<label id="lblProductosEspecificacion" runat="server" class="control-label" for="txtProductosEspecificacion"></label>
									<input class="form-control" id="txtProductosEspecificacion" runat="server">
								</div>
							</div>
							<div class="col-md-5" style="padding-top: 25px;">
								<div class="form-group">
									<asp:RegularExpressionValidator ID="revEspecificacionProducto" runat="server" ControlToValidate="txtProductosEspecificacion"
										ValidationExpression="^([\w\s\.]+)$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-2">
								<div class="form-group">
									<label id="lblPrecioUnitario" runat="server" class="control-label" for="txtProductosPrecioUnitario"></label>
									<input class="form-control" id="txtProductosPrecioUnitario" runat="server" placeholder="Ej. 123,45">
								</div>
							</div>
							<div class="col-md-10" style="padding-top: 25px;">
								<div class="form-group">
									<asp:RegularExpressionValidator ID="revPrecio" runat="server" ControlToValidate="txtProductosPrecioUnitario"
										ValidationExpression="^([0-9]{1,7}(,?)[0-9]{0,2})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-2">
								<div class="form-group">
									<label id="lblProductosTipo" runat="server" class="control-label" for="ddlProductosTipo"></label>
									<asp:DropDownList ID="ddlProductosTipo" class="form-control" runat="server"></asp:DropDownList>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-4">
								<div class="form-group">
									<label id="lblProductosImagen" runat="server" class="control-label" for="fuImagen"></label>
									<input runat="server" type="file" class="form-control" onchange="readURL(this);" id="fuImagen" placeholder="Imagen" 
										required />
								</div>
							</div>
							<div class="col-md-5">
								<div class="imgVisualizerContainer">
									<img id="imageVisualizer" src="#" alt="" style="visibility: hidden; width: 300px;" />
									<div id="removerimagen"><span id="EliminarImagen" onclick="eliminarImg();" class="glyphicon glyphicon-trash" 
										aria-hidden="true" style="visibility: hidden"></span></div>
								</div>
							</div>

						</div>
						<div class="row">
							<div class="col-md-3">
								<div class="form-group">
									<button id="btnProductosConfirmar" runat="server" type="button" class="btn btn-success"></button>
								</div>
							</div>
						</div>
						<%--	<div class="row">
							<div class="col-md-5">
								<div id="divProductosAlerta" runat="server" role="alert"></div>
							</div>
						</div>--%>
					</div>
				</div>
				<div id="divProductosListado" class="container" runat="server">
					<div class="vertical-center-row">
						<div class="row">
							<div class="col-md-12">
								<div class="scrolling-table-container" style="overflow-y: scroll; height: 430px; width: 1000px;">
									<asp:GridView ID="gvProductosProductos" runat="server" CssClass="table table-striped table-bordered table-hover">
										<Columns>
											<asp:CommandField ButtonType="Image" CancelImageUrl="~/images/14_Delete_16x16.png" CancelText="" DeleteText="" 
												EditImageUrl="~/images/05_Edit_16x16.png" EditText="" ShowEditButton="True" 
												UpdateImageUrl="~/images/15_Tick_16x16.png" UpdateText="" CausesValidation="false">
												<ItemStyle Wrap="False" />
											</asp:CommandField>
											<asp:TemplateField>
												<ItemTemplate>
													<asp:ImageButton ID="btnProductosEliminar" CommandArgument='<%# Eval("Id")%>' 
														OnClick="btnAsignacionRolesEliminarRol_Click" runat="server" Text="" 
														ImageUrl="~/images/14_Delete_16x16.png" AlternateText="Eliminar"></asp:ImageButton>
													<cc1:ConfirmButtonExtender ID="cbe" runat="server" DisplayModalPopupID="mpe" 
														TargetControlID="btnProductosEliminar"></cc1:ConfirmButtonExtender>
													<cc1:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" 
														TargetControlID="btnProductosEliminar" OkControlID="btnYes"
														CancelControlID="btnNo" BackgroundCssClass="modalBackground">
													</cc1:ModalPopupExtender>
													<asp:Panel ID="pnlPopup" runat="server" CssClass="modal-dialog" Style="display: none">
														<div class="modal-content">
															<div class="modal-header">
																Confirmación
															</div>
															<div class="modal-body">
																¿Está seguro que desea eliminar el producto?
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
											<asp:TemplateField HeaderText="Codigo">
												<EditItemTemplate>
													<asp:TextBox ID="txtCodigo" runat="server" Text='<%# Bind("Codigo")%>'
														Width="100px"></asp:TextBox>
													<asp:RegularExpressionValidator ID="revCodigoProducto2" ErrorMessage="*" runat="server"
														ControlToValidate="txtCodigo"
														ValidationExpression="^[a-zA-Z0-9]{1,10}$"
														ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
													<asp:RequiredFieldValidator ID="rfvCodigo" runat="server" ErrorMessage="*"
														ControlToValidate="txtCodigo" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
												</EditItemTemplate>
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# Bind("Codigo")%>' ID="lblCodigo"></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Nombre">
												<EditItemTemplate>
													<asp:TextBox ID="txtNombre" runat="server" Text='<%# Bind("Nombre")%>'
														Width="300px"></asp:TextBox>
													<asp:RegularExpressionValidator ID="revNombreProducto2" ErrorMessage="*" runat="server"
														ControlToValidate="txtNombre"
														ValidationExpression="^([\w\s\.]{1,100})$"
														ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
													<asp:RequiredFieldValidator ID="rfvNombre" runat="server" ErrorMessage="*"
														ControlToValidate="txtNombre" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
												</EditItemTemplate>
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# Bind("Nombre")%>' ID="lblNombre"></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Especificacion">
												<EditItemTemplate>
													<asp:TextBox ID="txtEspecificacion" runat="server" Text='<%# Bind("Especificacion")%>'
														Width="300px" TextMode="MultiLine"></asp:TextBox>
													<asp:RegularExpressionValidator ID="revEspecificacionProducto2" ErrorMessage="*" runat="server"
														ControlToValidate="txtEspecificacion"
														ValidationExpression="^([\w\s\.]+)$"
														ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
													<asp:RequiredFieldValidator ID="rfvEspecificacion" runat="server" ErrorMessage="*"
														ControlToValidate="txtEspecificacion" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
												</EditItemTemplate>
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# Bind("Especificacion")%>' ID="lblEspecificacion"></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="PrecioUnitario">
												<EditItemTemplate>
													<asp:TextBox ID="txtPrecioUnitario" runat="server" Text='<%# Bind("PrecioUnitario")%>'
														Width="100px"></asp:TextBox>
													<asp:RegularExpressionValidator ID="revPrecioUnitarioProducto2" ErrorMessage="*" runat="server"
														ControlToValidate="txtPrecioUnitario"
														ValidationExpression="^([0-9]{1,7}(,?)[0-9]{0,2})$"
														ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
													<asp:RequiredFieldValidator ID="rfvPrecioUnitario" runat="server" ErrorMessage="*"
														ControlToValidate="txtPrecioUnitario" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
												</EditItemTemplate>
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# Bind("PrecioUnitario")%>' ID="lblPrecioUnitario"></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField SortExpression="Tipo" HeaderText="Tipo">
												<EditItemTemplate>
													<asp:DropDownList ID="ddlProductosTipoGV" runat="server" SelectedValue='<%# Bind("Tipo")%>'
														DataTextField="Tipo" DataValueField="Tipo" DataSource="<%# ListarTipoProducto()%>">
													</asp:DropDownList>
												</EditItemTemplate>
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# Bind("Tipo")%>'
														ID="Tipo"></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField ItemStyle-Width="100px" HeaderText="Imagen">
												<ItemTemplate>
													<img src='data:image/jpeg;base64,<%# If(Not Eval("Imagen") Is Nothing, Convert.ToBase64String(DirectCast(Eval("Imagen"), Byte())), String.Empty)%>' 
														style="height: 50px; width: 50px;" />
												</ItemTemplate>
												<EditItemTemplate>
													<asp:FileUpload ID="fuProductosImagen" runat="server" />
												</EditItemTemplate>
											</asp:TemplateField>
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
