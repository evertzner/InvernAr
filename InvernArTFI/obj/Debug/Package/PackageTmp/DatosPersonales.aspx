<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DatosPersonales.aspx.vb" Inherits="InvernArTFI.DatosPersonales" MasterPageFile="~/Master.Master" %>

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
		<div id="divDatosPersonalesControles" runat="server" class="container">
			<div class="vertical-center-row">
				<div class="row">
					<div class="col-md-4">
						<div class="form-group">
							<label id="lblDatosPersonalesCorreoElectronico" runat="server" class="control-label" for="txtDatosPersonalesCorreoElectronico"></label>
							<input class="form-control" id="txtDatosPersonalesCorreoElectronico" runat="server">
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-md-2">
						<div class="form-group">
							<label id="lblDatosPersonalesDNI" runat="server" class="control-label" for="txtDatosPersonalesDNI"></label>
							<input class="form-control" id="txtDatosPersonalesDNI" runat="server">
						</div>
					</div>
					<div class="col-md-10" style="padding-top: 25px;">
						<div class="form-group">
							<asp:RegularExpressionValidator ID="revDNI" runat="server" ControlToValidate="txtDatosPersonalesDNI"
								ValidationExpression="^((\d{1,8}))$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-md-2">
						<div class="form-group">
							<label id="lblDatosPersonalesCUIT" runat="server" class="control-label" for="txtDatosPersonalesCUIT"></label>
							<input class="form-control" id="txtDatosPersonalesCUIT" runat="server">
						</div>
					</div>
					<div class="col-md-10" style="padding-top: 25px;">
						<div class="form-group">
							<asp:RegularExpressionValidator ID="revCUIT" runat="server" ControlToValidate="txtDatosPersonalesCUIT"
								ValidationExpression="^(\d{1,2}-)+(\d{1,8}-)+(\d{1,1})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-md-3">
						<div class="form-group">
							<label id="lblDatosPersonalesNombre" runat="server" class="control-label" for="txtDatosPersonalesNombre"></label>
							<input class="form-control" id="txtDatosPersonalesNombre" runat="server">
						</div>
					</div>
					<div class="col-md-9" style="padding-top: 25px;">
						<div class="form-group">
							<asp:RegularExpressionValidator ID="revNombre" runat="server" ControlToValidate="txtDatosPersonalesNombre"
								ValidationExpression="^([\D\s]{1,50})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-md-3">
						<div class="form-group">
							<label id="lblDatosPersonalesApellido" runat="server" class="control-label" for="txtDatosPersonalesApellido"></label>
							<input class="form-control" id="txtDatosPersonalesApellido" runat="server">
						</div>
					</div>
					<div class="col-md-9" style="padding-top: 25px;">
						<div class="form-group">
							<asp:RegularExpressionValidator ID="revApellido" runat="server" ControlToValidate="txtDatosPersonalesApellido"
								ValidationExpression="^([\D\s]{1,50})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-md-4">
						<div class="form-group">
							<label id="lblDatosPersonalesDomicilio" runat="server" class="control-label" for="txtDatosPersonalesDomicilio"></label>
							<input class="form-control" id="txtDatosPersonalesDomicilio" runat="server">
						</div>
					</div>
					<div class="col-md-8" style="padding-top: 25px;">
						<div class="form-group">
							<asp:RegularExpressionValidator ID="revDomicilio" runat="server" ControlToValidate="txtDatosPersonalesDomicilio"
								ValidationExpression="^([\w\s\.]{1,80})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-md-3">
						<div class="form-group">
							<label id="lblDatosPersonalesLocalidad" runat="server" class="control-label" for="txtDatosPersonalesLocalidad"></label>
							<input class="form-control" id="txtDatosPersonalesLocalidad" runat="server">
						</div>
					</div>
					<div class="col-md-9" style="padding-top: 25px;">
						<div class="form-group">
							<asp:RegularExpressionValidator ID="revLocalidad" runat="server" ControlToValidate="txtDatosPersonalesLocalidad"
								ValidationExpression="^([\w\s\.]{1,50})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-md-3">
						<div class="form-group">
							<label id="lblDatosPersonalesProvincia" runat="server" class="control-label" for="txtDatosPersonalesProvincia"></label>
							<input class="form-control" id="txtDatosPersonalesProvincia" runat="server">
						</div>
					</div>
					<div class="col-md-9" style="padding-top: 25px;">
						<div class="form-group">
							<asp:RegularExpressionValidator ID="revProvincia" runat="server" ControlToValidate="txtDatosPersonalesProvincia"
								ValidationExpression="^([\w\s\.]{1,50})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-md-2">
						<div class="form-group">
							<label id="lblDatosPersonalesTelefono" runat="server" class="control-label" for="txtDatosPersonalesTelefono"></label>
							<input class="form-control" id="txtDatosPersonalesTelefono" runat="server">
						</div>
					</div>
					<div class="col-md-10" style="padding-top: 25px;">
						<div class="form-group">
							<asp:RegularExpressionValidator ID="revTelefono" runat="server" ControlToValidate="txtDatosPersonalesTelefono"
								ValidationExpression="^([\d]{2,3}-[\d]{3,4}-[\d]{3,4})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-md-2">
						<div class="form-group">
							<label id="lblDatosPersonalesInterno" runat="server" class="control-label" for="txtDatosPersonalesInterno"></label>
							<input class="form-control" id="txtDatosPersonalesInterno" runat="server">
						</div>
					</div>
					<div class="col-md-10" style="padding-top: 25px;">
						<div class="form-group">
							<asp:RegularExpressionValidator ID="revInterno" runat="server" ControlToValidate="txtDatosPersonalesInterno"
								ValidationExpression="^([\d]{1,10})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-md-2">
						<div class="form-group">
							<label id="lblDatosPersonalesTelefonoCelular" runat="server" class="control-label" for="txtDatosPersonalesTelefonoCelular"></label>
							<input class="form-control" id="txtDatosPersonalesTelefonoCelular" runat="server">
						</div>
					</div>
					<div class="col-md-10" style="padding-top: 25px;">
						<div class="form-group">
							<asp:RegularExpressionValidator ID="revTelefonoCelular" runat="server"
								ControlToValidate="txtDatosPersonalesTelefonoCelular"
								ValidationExpression="^([\d]{2,3}-(15)-[\d]{3,4}-[\d]{3,4})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-md-4">
						<div class="form-group">
							<button id="btnDatosPersonalesModificar" runat="server" type="button" class="btn btn-success"></button>
						</div>
					</div>
				</div>
			</div>
			<div class="row">
				<div class="col-md-3">
					<div class="form-group">
						<label id="lblDatosPersonalesPassActual" runat="server" class="control-label" for="txtDatosPersonalesPassActual"></label>
						<input class="form-control" id="txtDatosPersonalesPassActual" runat="server" type="password">
					</div>
				</div>
				<div class="col-md-9" style="padding-top: 25px;">
					<div class="form-group">
						<asp:RegularExpressionValidator ID="revPass" runat="server" ControlToValidate="txtDatosPersonalesPassActual"
							ValidationExpression="^([A-Za-z0-9]{8,35})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
					</div>
				</div>
			</div>
			<div class="row">
				<div class="col-md-3">
					<div class="form-group">
						<label id="lblDatosPersonalesPassNueva" runat="server" class="control-label" for="txtDatosPersonalesPassNueva"></label>
						<input class="form-control" id="txtDatosPersonalesPassNueva" runat="server" type="password">
					</div>
				</div>
				<div class="col-md-9" style="padding-top: 25px;">
					<div class="form-group">
						<asp:RegularExpressionValidator ID="revPassNueva" runat="server" ControlToValidate="txtDatosPersonalesPassNueva"
							ValidationExpression="^([A-Za-z0-9]{8,35})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
					</div>
				</div>
			</div>
			<div class="row">
				<div class="col-md-3">
					<div class="form-group">
						<label id="lblDatosPersonalesPassNuevaRepetida" runat="server" class="control-label" for="txtDatosPersonalesPassNuevaRepetida"></label>
						<input class="form-control" id="txtDatosPersonalesPassNuevaRepetida" runat="server" type="password">
					</div>
				</div>
				<div class="col-md-9" style="padding-top: 25px;">
					<div class="form-group">
						<asp:RegularExpressionValidator ID="revPassNuevaRepetida" runat="server" ControlToValidate="txtDatosPersonalesPassNuevaRepetida"
							ValidationExpression="^([A-Za-z0-9]{8,35})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
					</div>
				</div>
			</div>
			<div class="row">
				<div class="col-md-4">
					<div class="form-group">
						<button id="btnDatosPersonalesCambiarContrasena" runat="server" type="button" class="btn btn-success"></button>
					</div>
				</div>
			</div>
			<div class="row">
				<div class="col-md-10">
					<div id="divDatosPersonalesAlerta" runat="server" role="alert"></div>
				</div>
			</div>
		</div>
	</body>
	</html>
</asp:Content>
