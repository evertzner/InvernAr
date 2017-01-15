<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Instalaciones.aspx.vb" Inherits="InvernArTFI.Instalaciones" MasterPageFile="~/Master.Master" %>

<%@ MasterType VirtualPath="~/Master.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Main" runat="server" ContentPlaceHolderID="Main">
	<!DOCTYPE html>
	<html>
	<head>
		<script src="Scripts/jquery-1.9.1.min.js"></script>
		<script src="Scripts/bootstrap.min.js"></script>
		<link href="Content/bootstrap-datetimepicker.css" rel="stylesheet" />
		<script src="Scripts/bootstrap-datetimepicker.js"></script>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<title></title>
		<script type="text/javascript">
			function mostrarCalendario(boton) {
				if (boton.id == 'dtpInstalacionesFechaDeSolicitud') {
					$('#dtpInstalacionesFechaDeSolicitud').datetimepicker({ format: "dd/mm/yyyy", minView: 2, todayBtn: true });
				} else {
					$('#dtpGestionEncuestasFechaVencimientoGV').datetimepicker({ format: "dd/mm/yyyy", minView: 2, todayBtn: true });
				}
			}
		</script>
	</head>
	<body>
		<div id="divInstalacionesControles" runat="server" class="container">
			<div id="wrapper">
				<div id="sidebar-wrapper">
					<ul class="sidebar-nav">
						<li id="liInstalaciones" runat="server" class="sidebar-brand"></li>
						<li>
							<a id="aInstalacionesAlta" runat="server" href="#"></a>
						</li>
						<li>
							<a id="aInstalacionesListado" runat="server" href="#"></a>
						</li>
					</ul>
				</div>
				<div id="divInstalacionesAlta" runat="server">
					<div class="vertical-center-row">
						<div class="row">
							<div class="col-md-2">
								<div class="form-group">
									<label id="lblInstalacionesIdCliente" runat="server" class="control-label" for="txtInstalacionesIdCliente"></label>
									<input class="form-control" id="txtInstalacionesIdCliente" runat="server">
								</div>
							</div>
							<div class="col-md-10" style="padding-top: 25px;">
								<div class="form-group">
									<asp:RegularExpressionValidator ID="revIdCliente" runat="server" ControlToValidate="txtInstalacionesIdCliente"
										ValidationExpression="^((\d{1,8}))$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-2">
								<div class="form-group">
									<label id="lblInstalacionesFechaDeSolicitud" runat="server" class="control-label" for="dtpInstalacionesFechaDeSolicitud"></label>
									<div class="input-group date" id="dtpInstalacionesFechaDeSolicitud" onclick="mostrarCalendario(this);">
										<input id="txtInstalacionesFechaDeSolicitud" runat="server" type="text" class="form-control" readonly="true" />
										<span class="input-group-addon">
											<span class="glyphicon glyphicon-calendar"></span>
										</span>
									</div>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-4">
								<div class="form-group">
									<label id="lblInstalacionesDatosDeContacto" runat="server" class="control-label" for="txtInstalacionesDatosDeContacto"></label>
									<textarea class="form-control" id="txtInstalacionesDatosDeContacto" runat="server" rows="2"></textarea>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-4">
								<div class="form-group">
									<label id="lblInstalacionesDomicilioDeInstalacion" runat="server" class="control-label" for="txtInstalacionesDomicilioDeInstalacion"></label>
									<textarea class="form-control" id="txtInstalacionesDomicilioDeInstalacion" runat="server" rows="2"></textarea>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-4">
								<div class="form-group">
									<label id="lblInstalacionesObservaciones" runat="server" class="control-label" for="txtInstalacionesObservaciones"></label>
									<textarea class="form-control" id="txtInstalacionesObservaciones" runat="server" rows="3"></textarea>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-2">
								<div class="form-group">
									<button id="btnInstalacionesNuevo" runat="server" type="button" class="btn btn-success"></button>
								</div>
							</div>
						</div>
					</div>
				</div>
				<div id="divInstalacionesListado" runat="server">
					<div id="divInstalacionesInstalaciones" runat="server">
						<div class="vertical-center-row">
							<div class="row">
								<div class="col-md-12">
									<label id="lblInstalacionesInstalaciones" runat="server" class="control-label" for="gvInstalacionesInstalaciones"></label>
									<asp:GridView ID="gvInstalacionesInstalaciones" runat="server" CssClass="table table-striped table-bordered table-hover" AllowPaging="True" PageSize="10">
										<Columns>
											<asp:CommandField ButtonType="Image" CancelImageUrl="~/images/14_Delete_16x16.png" CancelText="" DeleteText="" EditImageUrl="~/images/05_Edit_16x16.png" EditText="" ShowEditButton="True" UpdateImageUrl="~/images/15_Tick_16x16.png" UpdateText="">
												<ItemStyle Wrap="False" />
											</asp:CommandField>
											<asp:TemplateField>
												<ItemTemplate>
													<asp:ImageButton ID="btnInstalacionesEliminar" CommandArgument='<%# Eval("Id")%>' OnClick="btnInstalacionesEliminar_Click" runat="server" Text="" ImageUrl="~/images/14_Delete_16x16.png" AlternateText="Eliminar"></asp:ImageButton>
													<cc1:ConfirmButtonExtender ID="cbe1" runat="server" DisplayModalPopupID="mpe1" TargetControlID="btnInstalacionesEliminar"></cc1:ConfirmButtonExtender>
													<cc1:ModalPopupExtender ID="mpe1" runat="server" PopupControlID="pnlPopup1" TargetControlID="btnInstalacionesEliminar" OkControlID="btnYes1"
														CancelControlID="btnNo1" BackgroundCssClass="modalBackground">
													</cc1:ModalPopupExtender>
													<asp:Panel ID="pnlPopup1" runat="server" CssClass="modal-dialog" Style="display: none">
														<div class="modal-content">
															<div class="modal-header">
																Confirmación
															</div>
															<div class="modal-body">
																¿Está seguro que desea eliminar la instalación?
															</div>
															<div class="modal-footer">
																<asp:Button ID="btnYes1" CssClass="btn btn-primary btn-success" runat="server" Text="Si" />
																<asp:Button ID="btnNo1" CssClass="btn btn-primary btn-danger" runat="server" Text="No" />
															</div>
														</div>
													</asp:Panel>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" ReadOnly="true" />
											<asp:BoundField DataField="IdCliente" HeaderText="IdCliente" SortExpression="IdCliente" ReadOnly="true" />
											<asp:BoundField DataField="FechaDeSolicitud" HeaderText="Fecha de solicitud" SortExpression="FechaDeSolicitud" ReadOnly="True" />
											<asp:TemplateField HeaderText="DatosDeContacto">
												<EditItemTemplate>
													<asp:TextBox ID="txtDatosDeContacto" runat="server" Text='<%# Bind("DatosDeContacto")%>'></asp:TextBox>
													<asp:RequiredFieldValidator ID="rfvDatosDeContacto" runat="server" ErrorMessage="*" ControlToValidate="txtDatosDeContacto"
														ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
												</EditItemTemplate>
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# Bind("DatosDeContacto")%>' ID="lblDatosDeContacto"></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="DomicilioDeInstalacion">
												<EditItemTemplate>
													<asp:TextBox ID="txtDomicilioDeInstalacion" runat="server" Text='<%# Bind("DomicilioDeInstalacion")%>'></asp:TextBox>
													<asp:RequiredFieldValidator ID="rfvDomicilioDeInstalacion" runat="server" ErrorMessage="*" ControlToValidate="txtDomicilioDeInstalacion"
														ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
												</EditItemTemplate>
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# Bind("DomicilioDeInstalacion")%>' ID="lblDomicilioDeInstalacion"></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Observaciones">
												<EditItemTemplate>
													<asp:TextBox ID="txtObservaciones" runat="server" Text='<%# Bind("Observaciones")%>'></asp:TextBox>
													<asp:RequiredFieldValidator ID="rfvObservaciones" runat="server" ErrorMessage="*" ControlToValidate="txtObservaciones"
														ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
												</EditItemTemplate>
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# Bind("Observaciones")%>' ID="lblObservaciones"></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:BoundField DataField="FechaDeRealizacion" HeaderText="Fecha de realizacion" SortExpression="FechaDeRealizacion" ReadOnly="true" />
											<asp:BoundField DataField="Realizado" HeaderText="Realizado" SortExpression="Realizado" ReadOnly="true" />
											<asp:TemplateField ItemStyle-Width="100px">
												<ItemTemplate>
													<asp:LinkButton ID="RealizarInstalacion" runat="server" CommandName="RealizarInstalacion" CausesValidation="false" OnClick="RealizarInstalacion_Click"
														CommandArgument='<%# Eval("Id") %>' Text="Realizar instalacion" CssClass="btn btn-primary" />
													<cc1:ConfirmButtonExtender ID="cbe" runat="server" DisplayModalPopupID="mpe" TargetControlID="RealizarInstalacion"></cc1:ConfirmButtonExtender>
													<cc1:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="RealizarInstalacion" OkControlID="btnYes"
														CancelControlID="btnNo" BackgroundCssClass="modalBackground">
													</cc1:ModalPopupExtender>
													<asp:Panel ID="pnlPopup" runat="server" CssClass="modal-dialog" Style="display: none">
														<div class="modal-content">
															<div class="modal-header">
																Confirmación
															</div>
															<div class="modal-body">
																¿Se realizó la instalación?
															</div>
															<div class="modal-footer">
																<asp:Button ID="btnYes" CssClass="btn btn-primary btn-success" runat="server" Text="Si" />
																<asp:Button ID="btnNo" CssClass="btn btn-primary btn-danger" runat="server" Text="No" />
															</div>
														</div>
													</asp:Panel>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:CommandField ButtonType="Image" SelectImageUrl="~/images/15_Tick_16x16.png" ShowSelectButton="True" ItemStyle-HorizontalAlign="Center" />
										</Columns>
									</asp:GridView>
								</div>
							</div>
						</div>
					</div>
					<div id="divInstalacionesInstalacionDetalle" runat="server">
						<div class="vertical-center-row">
							<div class="row">
								<div class="col-md-3">
									<div class="form-group">
										<label id="lblInstalacionesProductos" runat="server" class="control-label" for="ddlInstalacionesProductos"></label>
										<asp:DropDownList ID="ddlInstalacionesProductos" class="form-control" runat="server"></asp:DropDownList>
									</div>
								</div>
								<div class="col-md-1">
									<div class="form-group">
										<label id="lblInstalacionesCantidadProductos" runat="server" class="control-label" for="txtInstalacionesCantidadProductos"></label>
										<input class="form-control" id="txtInstalacionesCantidadProductos" runat="server">
									</div>
								</div>
								<div class="col-md-2" style="padding-top:25px;">
									<div class="form-group">
										<button id="btnInstalacionesProductosNuevo" runat="server" type="button" class="btn btn-success"></button>
									</div>
								</div>
								<div class="col-md-3" style="padding-top: 25px;">
									<div class="form-group">
										<asp:RegularExpressionValidator ID="revCantidad" runat="server"
											ControlToValidate="txtInstalacionesCantidadProductos"
											ValidationExpression="^(\d{1,7})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
									</div>
								</div>

							</div>
						</div>
						<div class="row">
							<div class="col-md-12">
								<label id="lblInstalacionesInstalacionDetalle" runat="server" class="control-label" for="gvInstalacionesInstalacionDetalle"></label>
								<asp:GridView ID="gvInstalacionesInstalacionDetalle" runat="server" CssClass="table table-striped table-bordered table-hover" AllowPaging="True" PageSize="20">
									<Columns>
										<asp:CommandField ButtonType="Image" CancelImageUrl="~/images/14_Delete_16x16.png" CancelText="" DeleteText="" EditImageUrl="~/images/05_Edit_16x16.png" EditText="" ShowEditButton="True" UpdateImageUrl="~/images/15_Tick_16x16.png" UpdateText="">
											<ItemStyle Wrap="False" />
										</asp:CommandField>
										<asp:TemplateField>
											<ItemTemplate>
												<asp:ImageButton ID="btnInstalacionesProductoEliminar" CommandArgument='<%# Eval("Id")%>' OnClick="btnInstalacionesProductoEliminar_Click" runat="server" Text="" ImageUrl="~/images/14_Delete_16x16.png" AlternateText="Eliminar"></asp:ImageButton>
												<cc1:ConfirmButtonExtender ID="cbe2" runat="server" DisplayModalPopupID="mpe2" TargetControlID="btnInstalacionesProductoEliminar"></cc1:ConfirmButtonExtender>
												<cc1:ModalPopupExtender ID="mpe2" runat="server" PopupControlID="pnlPopup2" TargetControlID="btnInstalacionesProductoEliminar" OkControlID="btnYes2"
													CancelControlID="btnNo2" BackgroundCssClass="modalBackground">
												</cc1:ModalPopupExtender>
												<asp:Panel ID="pnlPopup2" runat="server" CssClass="modal-dialog" Style="display: none">
													<div class="modal-content">
														<div class="modal-header">
															Confirmación
														</div>
														<div class="modal-body">
															¿Está seguro que desea eliminar el producto?
														</div>
														<div class="modal-footer">
															<asp:Button ID="btnYes2" CssClass="btn btn-primary btn-success" runat="server" Text="Si" />
															<asp:Button ID="btnNo2" CssClass="btn btn-primary btn-danger" runat="server" Text="No" />
														</div>
													</div>
												</asp:Panel>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" Visible="false" />
										<asp:BoundField DataField="IdInstalacion" HeaderText="IdInstalacion" SortExpression="IdInstalacion" Visible="false" />
										<asp:BoundField DataField="IdProducto" HeaderText="IdProducto" SortExpression="IdProducto" Visible="false" />
										<asp:BoundField DataField="Producto" HeaderText="Producto" SortExpression="Producto" ReadOnly="true" />
										<asp:TemplateField HeaderText="Cantidad">
											<EditItemTemplate>
												<asp:TextBox ID="txtCantidad" runat="server" Text='<%# Bind("Cantidad")%>' Width="100px"></asp:TextBox>
												<asp:RegularExpressionValidator ID="revCantidad2" ErrorMessage="*" runat="server"
													ControlToValidate="txtCantidad" ValidationExpression="^((\d{1,7}))$" ForeColor="Red"
													Font-Bold="true"></asp:RegularExpressionValidator>
												<asp:RequiredFieldValidator ID="rfvCantidad" runat="server" ErrorMessage="*"
													ControlToValidate="txtCantidad" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
											</EditItemTemplate>
											<ItemTemplate>
												<asp:Label runat="server" Text='<%# Bind("Cantidad")%>' ID="lblCantidad"></asp:Label>
											</ItemTemplate>
										</asp:TemplateField>
									</Columns>
								</asp:GridView>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
		<%--<div class="modal fade" id="modalInstalacionesMensaje" role="dialog">
			<div class="modal-dialog">
				<div class="modal-content">
					<div class="modal-header">
						<h4 class="modal-title" id="lblMasterModalMensajeTitulo" runat="server"></h4>
					</div>
					<div class="modal-body">
						<asp:Label ID="lblMasterModalMensajeMensaje" runat="server" Text=""></asp:Label>
					</div>
					<div class="modal-footer">
						<button id="botonNotificacionAceptar" runat="server" type="button" class="btn btn-primary" data-dismiss="modal"></button>
					</div>
				</div>
			</div>
		</div>--%>
	</body>
	</html>
</asp:Content>
