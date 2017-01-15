<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Compras.aspx.vb" Inherits="InvernArTFI.Compras" MasterPageFile="~/Master.Master" %>

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
		<div id="divComprasControles" runat="server" class="container">
			<div id="divComprasFacturas" runat="server">
				<div class="vertical-center-row">
					<div class="row">
						<div class="col-md-12">
							<label id="lblComprasFacturas" runat="server" class="control-label" for="gvComprasFacturas"></label>
							<asp:GridView ID="gvComprasFacturas" runat="server" CssClass="table table-striped table-bordered table-hover" AllowPaging="True" PageSize="10">
								<Columns>
									<asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" ReadOnly="true" />
									<asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="Fecha" ReadOnly="True" />
									<asp:BoundField DataField="IdCliente" HeaderText="IdCliente" SortExpression="IdCliente" Visible="false" />
									<asp:BoundField DataField="Total" HeaderText="Total" SortExpression="Total" ReadOnly="true" />
									<asp:BoundField DataField="Cancelada" HeaderText="Cancelada" SortExpression="Cancelada" ReadOnly="true" />
									<asp:TemplateField ItemStyle-Width="100px">
										<ItemTemplate>
											<asp:LinkButton ID="CancelarFactura" runat="server" CommandName="CancelarFactura" CausesValidation="false" OnClick="CancelarFactura_Click"
												CommandArgument='<%# Eval("Id") %>' Text="Cancelar Factura" CssClass="btn btn-primary" />
											<cc1:ConfirmButtonExtender ID="cbe" runat="server" DisplayModalPopupID="mpe" TargetControlID="CancelarFactura"></cc1:ConfirmButtonExtender>
											<cc1:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="CancelarFactura" OkControlID="btnYes"
												CancelControlID="btnNo" BackgroundCssClass="modalBackground">
											</cc1:ModalPopupExtender>
											<asp:Panel ID="pnlPopup" runat="server" CssClass="modal-dialog" Style="display: none">
												<div class="modal-content">
													<div class="modal-header">
														Confirmación
													</div>
													<div class="modal-body">
														¿Está seguro que desea cancelar la factura?
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
			<div id="divComprasFacturaDetalle" runat="server">
				<div class="vertical-center-row">
					<div class="row">
						<div class="col-md-12">
							<label id="lblComprasSeguimiento" runat="server" class="control-label"></label>
						</div>
					</div>
					<div class="row">
						<div class="col-md-12">
							<label id="lblComprasFacturaDetalle" runat="server" class="control-label" for="gvComprasFacturaDetalle"></label>
							<asp:GridView ID="gvComprasFacturaDetalle" runat="server" CssClass="table table-striped table-bordered table-hover" AllowPaging="True" PageSize="20">
								<Columns>
									<asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" Visible="false" />
									<asp:BoundField DataField="IdFactura" HeaderText="IdFactura" SortExpression="IdFactura" Visible="false" />
									<asp:BoundField DataField="IdProducto" HeaderText="IdProducto" SortExpression="IdProducto" Visible="false" />
									<asp:BoundField DataField="ProductoNombre" HeaderText="Producto" SortExpression="ProductoNombre" ReadOnly="true" />
									<asp:BoundField DataField="Cantidad" HeaderText="Cantidad" SortExpression="Cantidad" ReadOnly="true" />
									<asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario" SortExpression="PrecioUnitario" ReadOnly="true" />
									<asp:BoundField DataField="Precio" HeaderText="Precio" SortExpression="Precio" ReadOnly="true" />
								</Columns>
							</asp:GridView>
						</div>
					</div>
				</div>
			</div>
			<div class="modal fade" id="ModalComprasCancelarFactura" role="dialog">
				<div class="modal-dialog">
					<div class="modal-content">
						<div class="modal-header">
							<%--<button type="button" class="close" data-dismiss="modal" aria-label="Close" runat="server" id="btnModalClose"><span aria-hidden="true">&times;</span></button>--%>
							<h4 class="modal-title" id="modalComprasCancelarFacturaTitle" runat="server"></h4>
						</div>
						<div class="modal-body">
							<div class="form-group" id="cuerpo" runat="server">
								<label id="lblmodalComprasFichaOpinionMotivo" runat="server" class="control-label" style="font-size: 20px; font-weight: 700;"></label>
								<br />
								<%--<input id="Text2" runat="server" class="form-control" type="text">--%>
								<asp:RadioButton ID="Radio1a" runat="server" Text="No quiero el producto" GroupName="1" Checked="true" />
								<br />
								<asp:RadioButton ID="Radio1b" runat="server" Text="Nunca me llegó el producto" GroupName="1" />
								<br />
								<asp:RadioButton ID="Radio1c" runat="server" Text="Se me incendió el invernadero" GroupName="1" />
								<br />
								<asp:RadioButton ID="Radio1d" runat="server" Text="Conseguí otro proveedor más copado" GroupName="1" />
								<br />
								<asp:RadioButton ID="Radio1e" runat="server" Text="Otro motivo que no quiero dar" GroupName="1" />
							</div>
						</div>
						<div class="modal-footer">
							<button id="btnComprasModalAceptar" runat="server" type="button" class="btn btn-primary">Enviar</button>
						</div>
					</div>
				</div>
			</div>
		</div>
	</body>
	</html>
</asp:Content>
