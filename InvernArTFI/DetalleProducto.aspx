<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DetalleProducto.aspx.vb" Inherits="InvernArTFI.DetalleProducto" MasterPageFile="~/Master.Master" %>

<%@ MasterType VirtualPath="~/Master.Master" %>
<asp:Content ID="Main" runat="server" ContentPlaceHolderID="Main">
	<!DOCTYPE html>
	<html>
	<head>
		<script src="Scripts/jquery-1.9.1.min.js"></script>
		<script src="Scripts/bootstrap.min.js"></script>
		<link href="Content/simple-sidebar.css" rel="stylesheet" />
		<link href="Content/Catalogo.css" rel="stylesheet" />
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<title></title>
		<style>
			.star {
				color: grey;
				font-size: 20px;
			}

			input[type="radio"] {
				display: none;
			}

			label:hover,
			label:hover ~ label {
				color: #5cb85c;
			}

			input[type="radio"]:checked ~ label {
				color: #5cb85c;
			}

			.clasificacion {
				direction: rtl;
				unicode-bidi: bidi-override;
				margin-left: -80px;
			}
		</style>
		<script type="text/javascript">
			function valorizar(radio) {
				var hv = document.getElementById('Main_hiddenValorizacion');
				if (radio.checked == true) {
					hv.value = radio.value;
				}
			}
		</script>
	</head>
	<body>
		<asp:HiddenField ID="hiddenValorizacion" runat="server" />
		<div id="divDetalleProductoControles" runat="server" class="container">
			<div class="vertical-center-row">
				<div class="row">
					<div class="col-md-5">
						<div id="divDetalleProductoContenedor" runat="server" class="catalogosContainer">
						</div>
					</div>
					<div class="col-md-5">
						<div id="divDetalleProductoDetalles" runat="server" class="panel panel-default">
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-md-12">
						<div class="scrolling-table-container">
							<asp:GridView ID="gvDetalleProductoComentarios" runat="server" CssClass="table table-striped table-bordered table-hover" AllowPaging="True" PageSize="10">
								<Columns>
									<asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" Visible="False" />
									<asp:BoundField DataField="IdProducto" HeaderText="IdProducto" SortExpression="IdProducto" Visible="False" />
									<asp:BoundField DataField="IdUsuario" HeaderText="IdUsuario" SortExpression="IdUsuario" Visible="False" />
									<asp:BoundField DataField="NombreUsuario" HeaderText="Nombre Usuario" SortExpression="NombreUsuario">
										<HeaderStyle Wrap="False" />
										<ItemStyle Wrap="False" />
									</asp:BoundField>
									<asp:BoundField DataField="Comentario" HeaderText="Comentario" SortExpression="Comentario">
										<HeaderStyle Wrap="False" />
										<ItemStyle Wrap="False" />
									</asp:BoundField>
									<asp:TemplateField ItemStyle-CssClass="clasificacion" HeaderStyle-Width="140px">
										<ItemTemplate>
											<asp:Label ID="estrella5" class="star" runat="server">★</asp:Label>
											<asp:Label ID="estrella4" class="star" runat="server">★</asp:Label>
											<asp:Label ID="estrella3" class="star" runat="server">★</asp:Label>
											<asp:Label ID="estrella2" class="star" runat="server">★</asp:Label>
											<asp:Label ID="estrella1" class="star" runat="server">★</asp:Label>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:BoundField DataField="FechaComentado" HeaderText="Fecha Comentado" SortExpression="FechaComentado">
										<HeaderStyle Wrap="False" />
										<ItemStyle Wrap="False" />
									</asp:BoundField>
									<asp:BoundField DataField="Valorizacion" HeaderText="Valorizacion" SortExpression="Valorizacion" Visible="False" />
								</Columns>
							</asp:GridView>
						</div>
					</div>
				</div>
				<div id="divDetalleProductoComentar" runat="server" class="row">
					<div class="col-md-6">
						<div class="form-group">
							<input class="form-control" id="txtDetalleProductoComentario" runat="server" type="text">
						</div>
					</div>
					<div class="col-md-2 clasificacion" id="Radios">
						<input id="radio1" type="radio" name="estrellas" value="5" onclick="valorizar(this);"><!--
    --><label class="star" for="radio1">★</label><!--
    --><input id="radio2" type="radio" name="estrellas" value="4" onclick="valorizar(this);"><!--
    --><label class="star" for="radio2">★</label><!--
    --><input id="radio3" type="radio" name="estrellas" value="3" onclick="valorizar(this);"><!--
    --><label class="star" for="radio3">★</label><!--
    --><input id="radio4" type="radio" name="estrellas" value="2" onclick="valorizar(this);"><!--
    --><label class="star" for="radio4">★</label><!--
    --><input id="radio5" type="radio" name="estrellas" value="1" onclick="valorizar(this);"><!--
    --><label class="star" for="radio5">★</label>
					</div>
					<div class="col-md-3">
						<div class="form-group">
							<button id="btnDetalleProductoComentar" runat="server" type="button" class="btn btn-success"></button>
						</div>
					</div>
				</div>
			</div>
		</div>
	</body>
	</html>
</asp:Content>
