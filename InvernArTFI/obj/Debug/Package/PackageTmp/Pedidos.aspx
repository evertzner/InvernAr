<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Pedidos.aspx.vb" Inherits="InvernArTFI.Pedidos" MasterPageFile="~/Master.Master" %>

<%@ MasterType VirtualPath="~/Master.Master" %>
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
		<div id="divPedidosControles" runat="server" class="container">
			<div class="vertical-center-row">
				<div class="row">
					<div class="col-md-12">
						<asp:GridView ID="gvPedidosPedidos" runat="server" CssClass="table table-striped table-bordered table-hover" AllowPaging="True" PageSize="10">
							<Columns>
								<asp:CommandField ButtonType="Image" CancelImageUrl="~/images/14_Delete_16x16.png" CancelText="" DeleteText="" EditImageUrl="~/images/05_Edit_16x16.png" EditText="" ShowEditButton="True" UpdateImageUrl="~/images/15_Tick_16x16.png" UpdateText="" CausesValidation="false">
									<ItemStyle Wrap="False" />
								</asp:CommandField>
								<asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" Visible="True" ReadOnly="true" />
								<asp:BoundField DataField="IdFactura" HeaderText="IdFactura" SortExpression="IdFactura" Visible="True" ReadOnly="true" />
								<asp:TemplateField SortExpression="Estado" HeaderText="Estado">
									<EditItemTemplate>
										<asp:DropDownList ID="ddlPedidosEstado" runat="server" SelectedValue='<%# Bind("Estado")%>'
											DataTextField="Estado" DataValueField="Estado" DataSource="<%# ListarEstados()%>">
										</asp:DropDownList>
									</EditItemTemplate>
									<ItemTemplate>
										<asp:Label runat="server" Text='<%# Bind("Estado")%>' ID="Estado"></asp:Label>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="Fecha" Visible="True" ReadOnly="true" />
								<asp:CommandField ButtonType="Image" SelectImageUrl="~/images/15_Tick_16x16.png" ShowSelectButton="True" ItemStyle-HorizontalAlign="Center" />
							</Columns>
						</asp:GridView>
					</div>
				</div>
				<div id="divPedidosFacturaDetalle" runat="server" class="row">
					<div class="row">
						<div class="col-md-12">
							<label id="lblPedidosFacturaDetalle" runat="server" class="control-label" for="gvPedidosFacturaDetalle"></label>
							<asp:GridView ID="gvPedidosFacturaDetalle" runat="server" CssClass="table table-striped table-bordered table-hover" AllowPaging="True" PageSize="20">
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
		</div>
	</body>
	</html>
</asp:Content>
