<%@ Page Language="vb" AutoEventWireup="true" CodeBehind="Comprar.aspx.vb" Inherits="InvernArTFI.Comprar" MasterPageFile="~/Master.Master" %>

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
		<div id="divComprarControles" runat="server" class="container">
			<div id="divComprarCarrito" runat="server">
				<div class="vertical-center-row">
					<div class="row">
						<div class="col-md-12">
							<asp:GridView ID="gvComprarCarrito" runat="server" CssClass="table table-striped table-bordered table-hover" AllowPaging="True" PageSize="10">
								<Columns>
									<asp:CommandField ButtonType="Image" CancelImageUrl="~/images/14_Delete_16x16.png" CancelText="" DeleteText="" EditImageUrl="~/images/05_Edit_16x16.png" EditText="" ShowEditButton="True" UpdateImageUrl="~/images/15_Tick_16x16.png" UpdateText="">
										<ItemStyle Wrap="False" />
									</asp:CommandField>
									<asp:CommandField ButtonType="Image" DeleteImageUrl="~/images/14_Delete_16x16.png" ShowDeleteButton="True" />
									<asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" Visible="False" />
									<asp:BoundField DataField="IdFactura" HeaderText="IdFactura" SortExpression="IdFactura" Visible="false" />
									<asp:BoundField DataField="IdProducto" HeaderText="IdProducto" SortExpression="IdProducto" Visible="false" />
									<asp:BoundField DataField="ProductoNombre" HeaderText="Producto" SortExpression="ProductoNombre" ReadOnly="true" />
									<asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario" SortExpression="PrecioUnitario" ReadOnly="true" />
									<asp:TemplateField HeaderText="Cantidad">
										<EditItemTemplate>
											<asp:TextBox ID="txtCantidad" runat="server" Text='<%# Bind("Cantidad")%>' Width="100px"></asp:TextBox>
											<asp:RegularExpressionValidator ID="revComprarCantidad2" ErrorMessage="*" runat="server"
												ControlToValidate="txtCantidad" ValidationExpression="^((\d{1,4}))$" ForeColor="Red"
												Font-Bold="true"></asp:RegularExpressionValidator>
											<asp:RequiredFieldValidator ID="rfvComprarCantidad" runat="server" ErrorMessage="*"
												ControlToValidate="txtCantidad" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
										</EditItemTemplate>
										<ItemTemplate>
											<asp:Label runat="server" Text='<%# Bind("Cantidad")%>' ID="lblCantidad"></asp:Label>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:BoundField DataField="Precio" HeaderText="Precio" SortExpression="Precio" ReadOnly="true" />
								</Columns>
							</asp:GridView>
						</div>
					</div>
					<div class="row">
						<div class="col-md-2">
							<div class="form-group">
								<button id="btnComprarSiguiente1" runat="server" type="button" class="btn btn-success"></button>
							</div>
						</div>
						<div class="col-md-3 col-md-offset-7">
							<div class="form-group">
								<label id="lblComprarTotal" runat="server" class="control-label"></label>
							</div>
						</div>
					</div>
				</div>
			</div>
			<div id="divComprarMedioPago" runat="server">
				<div class="vertical-center-row">
					<div class="row">
						<div class="col-md-5">
							<div class="form-group">
								<label id="lblComprarMedioPago" runat="server" class="control-label" for="ddlComprarMedioPago"></label>
								<asp:DropDownList ID="ddlComprarMedioPago" class="form-control" runat="server"></asp:DropDownList>
							</div>
						</div>
					</div>
					<div class="row">
						<div class="col-md-2 ">
							<div class="form-group">
								<button id="btnComprarAnterior2" runat="server" type="button" class="btn btn-info"></button>
							</div>
						</div>
						<div class="col-md-2 col-md-offset-1">
							<div class="form-group">
								<button id="btnComprarSiguiente2" runat="server" type="button" class="btn btn-success"></button>
							</div>
						</div>
					</div>
				</div>
			</div>
			<div id="divComprarNotaCredito" runat="server">
				<div class="vertical-center-row">
					<div class="row">
						<div class="col-md-12">
							<label id="lblComprarNotaCredito" runat="server" class="control-label"></label>
						</div>
					</div>
					<div class="row">
						<div class="col-md-2 ">
							<div class="form-group">
								<button id="btnComprarAnterior4" runat="server" type="button" class="btn btn-info"></button>
							</div>
						</div>
						<div class="col-md-2 col-md-offset-1">
							<div class="form-group">
								<button id="btnComprarComprar4" runat="server" type="button" class="btn btn-success"></button>
							</div>
						</div>
					</div>
				</div>
			</div>
			<div id="divComprarVerificarTarjeta" runat="server">
				<div class="vertical-center-row">
					<div class="row">
						<div class="col-md-5">
							<div class="form-group">
								<label id="lblComprarTarjeta" runat="server" class="control-label" for="ddlComprarTarjeta"></label>
								<asp:DropDownList ID="ddlComprarTarjeta" class="form-control" runat="server"></asp:DropDownList>
							</div>
						</div>
					</div>
					<div class="row">
						<div class="col-md-5">
							<div class="form-group">
								<label id="lblComprarNombre" runat="server" class="control-label" for="txtComprarNombre"></label>
								<input class="form-control" id="txtComprarNombre" runat="server">
							</div>
						</div>
						<div class="col-md-7" style="padding-top: 25px;">
							<div class="form-group">
								<asp:RegularExpressionValidator ID="revNombreTarjeta" runat="server" ControlToValidate="txtComprarNombre"
									ValidationExpression="^([\D\s]{1,80})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
							</div>
						</div>
					</div>
					<div class="row">
						<div class="col-md-3">
							<div class="form-group">
								<label id="lblComprarNumero" runat="server" class="control-label" for="txtComprarNumero"></label>
								<input class="form-control" id="txtComprarNumero" runat="server">
							</div>
						</div>
						<div class="col-md-7" style="padding-top: 25px;">
							<div class="form-group">
								<asp:RegularExpressionValidator ID="revNumeroTarjeta" runat="server" ControlToValidate="txtComprarNumero"
									ValidationExpression="^((\d{15,16}))$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
							</div>
						</div>
					</div>
					<div class="row">
						<div class="col-md-2">
							<div class="form-group">
								<label id="lblComprarCodigo" runat="server" class="control-label" for="txtComprarCodigo"></label>
								<input class="form-control" id="txtComprarCodigo" runat="server">
							</div>
						</div>
						<div class="col-md-7" style="padding-top: 25px;">
							<div class="form-group">
								<asp:RegularExpressionValidator ID="revCodigoTarjeta" runat="server" ControlToValidate="txtComprarCodigo"
									ValidationExpression="^((\d{3,5}))$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
							</div>
						</div>
					</div>
					<div class="row">
						<div class="col-md-3">
							<div class="form-group">
								<label id="lblComprarMes" runat="server" class="control-label" for="ddlComprarMes"></label>
								<asp:DropDownList ID="ddlComprarMes" class="form-control" runat="server"></asp:DropDownList>
							</div>
						</div>
						<div class="col-md-2">
							<label id="lblComprarAno" runat="server" class="control-label" for="ddlComprarAno"></label>
							<asp:DropDownList ID="ddlComprarAno" class="form-control" runat="server"></asp:DropDownList>
							<div class="form-group">
							</div>
						</div>
					</div>
					<div class="row">
						<div class="col-md-2 ">
							<div class="form-group">
								<button id="btnComprarAnterior3" runat="server" type="button" class="btn btn-info"></button>
							</div>
						</div>
						<div class="col-md-2 col-md-offset-1">
							<div class="form-group">
								<button id="btnComprarComprar3" runat="server" type="button" class="btn btn-success"></button>
							</div>
						</div>
					</div>
				</div>
			</div>
			<div class="modal fade" id="modalComprarFichaOpinion" role="dialog" data-keyboard="false">
				<div class="modal-dialog">
					<div class="modal-content">
						<div class="modal-header">
							<button id="btnModalClose" type="button" class="close" data-dismiss="modal" aria-label="Close" runat="server"><span aria-hidden="true">&times;</span></button>
							<h4 class="modal-title" id="modalComprarFichaOpinionTitle" runat="server"></h4>
						</div>
						<div class="modal-body">
							<div class="form-group" id="cuerpo" runat="server">
								<label id="lblmodalComprarFichaOpinionDificultad" runat="server" class="control-label" style="font-size: 20px; font-weight: 700;"></label>
								<br />
								<%--<input id="Text2" runat="server" class="form-control" type="text">--%>
								<asp:RadioButton ID="Radio1a" runat="server" Text="Facil" GroupName="1" Checked="true" />
								<asp:RadioButton ID="Radio1b" runat="server" Text="Normal" GroupName="1" />
								<asp:RadioButton ID="Radio1c" runat="server" Text="Difícil" GroupName="1" />
								<br />
								<label id="lblmodalComprarFichaOpinionDiseño" runat="server" class="control-label" style="font-size: 20px; font-weight: 700;"></label>
								<br />
								<%--<input id="Text2" runat="server" class="form-control" type="text">--%>
								<asp:RadioButton ID="Radio2a" runat="server" Text="Muy Desagradable" GroupName="2" Checked="true" />
								<asp:RadioButton ID="Radio2b" runat="server" Text="Desagradable" GroupName="2" />
								<asp:RadioButton ID="Radio2c" runat="server" Text="Normal" GroupName="2" />
								<asp:RadioButton ID="Radio2d" runat="server" Text="Agradable" GroupName="2" />
								<asp:RadioButton ID="Radio2e" runat="server" Text="Muy Agradable" GroupName="2" />
								<label id="Label2" runat="server" class="control-label" style="font-size: 20px; font-weight: 700;"></label>
								<br />
								<label id="lblmodalComprarFichaOpinionRetorno" runat="server" class="control-label" style="font-size: 20px; font-weight: 700;"></label>
								<br />
								<%--<input id="Text2" runat="server" class="form-control" type="text">--%>
								<asp:RadioButton ID="Radio3a" runat="server" Text="Si" GroupName="3" Checked="true" />
								<asp:RadioButton ID="Radio3b" runat="server" Text="No" GroupName="3" />
								<label id="Label4" runat="server" class="control-label" style="font-size: 20px; font-weight: 700;"></label>
							</div>
						</div>
						<div class="modal-footer">
							<button id="btnComprarModalAceptar" runat="server" type="button" class="btn btn-primary">Enviar</button>
						</div>
					</div>
				</div>
			</div>
		</div>
	</body>
	</html>
</asp:Content>
