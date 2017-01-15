<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CompararProductos.aspx.vb" Inherits="InvernArTFI.CompararProductos" MasterPageFile="~/Master.Master" %>

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
		<div id="divCompararProductosControles" runat="server" class="container">
			<div class="vertical-center-row">
				<div class="row">
					<div class="col-md-3">
						<div class="form-group">
							<a id="btnCompararProductosVolver" runat="server" href="Catalogo.aspx" class="btn btn-success"></a>
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-md-4">
						<div id="divCompararProductosImagenProducto1" runat="server" class="catalogosContainer">
						</div>
					</div>
					<div class="col-md-4">
						<div id="divCompararProductosImagenProducto2" runat="server" class="catalogosContainer">
						</div>
					</div>
					<div class="col-md-4">
						<div id="divCompararProductosImagenProducto3" runat="server" class="catalogosContainer">
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-md-4">
						<div id="divCompararProductosProducto1" runat="server" class="panel panel-default">
						</div>
					</div>
					<div class="col-md-4">
						<div id="divCompararProductosProducto2" runat="server" class="panel panel-default">
						</div>
					</div>
					<div class="col-md-4">
						<div id="divCompararProductosProducto3" runat="server" class="panel panel-default">
						</div>
					</div>
				</div>
			</div>
		</div>
	</body>
	</html>
</asp:Content>
