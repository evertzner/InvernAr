<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RecuperoContrasena.aspx.vb" Inherits="InvernArTFI.RecuperoContrasena" MasterPageFile="~/Master.Master" %>

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
		<div id="divRecuperoContrasenaControles" runat="server" class="container">
			<div class="col-md-10">
				<div class="row">
					<div class="col-md-10">
						<div class="form-group">
							<label id="lblRecuperoContrasenaCodigo" runat="server" class="col-sm-3 control-label"></label>
							<div class="col-sm-3">
								<input class="form-control" id="txtRecuperoContrasenaCodigo" runat="server" type="text">
							</div>
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-md-10">
						<div class="form-group">
							<label id="lblRecuperoContrasenaCorreoElectronico" runat="server" class="col-sm-3 control-label"></label>
							<div class="col-sm-4">
								<input class="form-control" id="txtRecuperoContrasenaCorreoElectronico" runat="server" type="email">
							</div>
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-md-10">
						<div class="form-group">
							<label id="lblRecuperoContrasenaPassNueva" runat="server" class="col-sm-3 control-label"></label>
							<div class="col-sm-4">
								<input class="form-control" id="txtRecuperoContrasenaPassNueva" runat="server" type="password">
							</div>
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-md-10">
						<div class="form-group">
							<label id="lblRecuperoContrasenaPassNuevaRepetida" runat="server" class="col-sm-3 control-label"></label>
							<div class="col-sm-4">
								<input class="form-control" id="txtRecuperoContrasenaPassNuevaRepetida" runat="server" type="password">
							</div>
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-md-7">
						<div class="form-group">
							<button id="btnRecuperoContrasenaConfirmar" runat="server" type="button" class="btn btn-success">Confirmar</button>
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-md-10">
						<div id="divRecuperoContrasenaAlerta" runat="server" role="alert"></div>
					</div>
				</div>
			</div>
		</div>
	</body>
	</html>
</asp:Content>
