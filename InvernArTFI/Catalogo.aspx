<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Catalogo.aspx.vb" Inherits="InvernArTFI.Catalogo" MasterPageFile="~/Master.Master" %>

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
		<script type="text/javascript">
			var cs = 0;
			var texto = "";
			function mostrar(check) {
				var hc = document.getElementById('Main_hiddenCuenta');
				var hp = document.getElementById('Main_hiddenProductos');

				if (check.checked == true) {
					cs += 1
				} else {
					cs -= 1
				}
				if (cs == 3) {
					var divRecorrido = document.getElementsByTagName('input');
					for (i = 0; i < divRecorrido.length; i++) {
						if (divRecorrido[i].type == 'checkbox') {
							if (divRecorrido[i].checked == false) {
								divRecorrido[i].disabled = true;
							}
						}
					}
				} else {
					var divRecorrido = document.getElementsByTagName('input');
					for (i = 0; i < divRecorrido.length; i++) {
						if (divRecorrido[i].type == 'checkbox') {
							if (divRecorrido[i].checked == false) {
								divRecorrido[i].disabled = false;
							}
						}
					}
				}

				var divRecorrido = document.getElementsByTagName('input');
				hp.value = "";
				for (i = 0; i < divRecorrido.length; i++) {
					if (divRecorrido[i].type == 'checkbox') {
						if (divRecorrido[i].checked == true) {
							hp.value += divRecorrido[i].id;
						}
					}
				}
				if (cs > 1) {
					hc.value = "true";
				} else {
					hc.value = "false";
				}
			}

			function limpiar() {
				var hp = document.getElementById('Main_hiddenProductos');
				hp.value = "";
			}
		</script>
	</head>
	<body>
		<asp:HiddenField ID="hiddenCuenta" runat="server" />
		<asp:HiddenField ID="hiddenProductos" runat="server" />
		<div id="divCatalogoControles" runat="server" class="container">
			<div class="vertical-center-row">
				<div class="row">
					<div class="col-md-2">
						<div class="form-group">
							<label id="lblCatalogoTipo" runat="server" class="control-label" for="ddlProductosTipo"></label>
							<asp:DropDownList ID="ddlCatalogoTipo" class="form-control" runat="server"></asp:DropDownList>
						</div>
					</div>
					<div class="col-md-2">
						<div class="form-group">
							<label id="lblOrdenarPor" runat="server" class="control-label" for="ddlOrdenarPor"></label>
							<asp:DropDownList ID="ddlOrdenarPor" class="form-control" runat="server"></asp:DropDownList>
						</div>
					</div>
					<div class="col-md-2">
						<div class="form-group">
							<label id="lblPrecioMayorA" runat="server" class="control-label" for="txtPrecioMayorA"></label>
							<input class="form-control" id="txtPrecioMayorA" runat="server" placeholder="Ej. 123,45">
						</div>
					</div>
					<div class="col-md-2">
						<div class="form-group">
							<label id="lblPrecioMenorA" runat="server" class="control-label" for="txtPrecioMenorA"></label>
							<input class="form-control" id="txtPrecioMenorA" runat="server" placeholder="Ej. 123,45">
						</div>
					</div>
					<div class="col-md-2" style="padding-top: 25px;">
						<div class="form-group">
							<asp:Button ID="btnCatalogoFiltrar" runat="server" class="btn btn-success" />
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-md-3">
						<div class="form-group">
							<button id="btnCatalogoComparar" runat="server" type="button" class="btn btn-success" onclick="cs=0;"></button>
						</div>
					</div>
					<div class="col-md-2 col-md-offset-1">
						<div class="form-group">
							<asp:RegularExpressionValidator ID="revPrecioMayorA" runat="server" ControlToValidate="txtPrecioMayorA"
								ValidationExpression="^([0-9]{1,7}(,?)[0-9]{0,2})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
						</div>
					</div>
					<div class="col-md-2">
						<div class="form-group">
							<asp:RegularExpressionValidator ID="revPrecioMenorA" runat="server" ControlToValidate="txtPrecioMenorA"
								ValidationExpression="^([0-9]{1,7}(,?)[0-9]{0,2})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
						</div>
					</div>
					<%--<div class="col-md-5">
								<div id="divCatalogoAlerta" runat="server" role="alert"></div>
							</div>--%>
				</div>
			</div>
			<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
				<ContentTemplate>
					<div id="divCatalogoContenedor" runat="server" class="catalogosContainer">
					</div>
				</ContentTemplate>
			</asp:UpdatePanel>
		</div>
	</body>
	</html>
</asp:Content>
