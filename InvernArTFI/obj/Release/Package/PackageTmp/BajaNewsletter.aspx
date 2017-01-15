<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BajaNewsletter.aspx.vb" Inherits="InvernArTFI.BajaNewsletter" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<meta charset="utf-8" />
	<meta http-equiv="X-UA-Compatible" content="IE=edge" />
	<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
	<!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
	<title>InvernAr</title>
	<link rel="stylesheet" href="Content/bootstrap.min.css" />
	<link rel="stylesheet" href="Content/jumbotron.css" />
	<link href="Content/narrow-jumbotron.css" rel="stylesheet" />
	<link href="Content/bajanewsletter.css" rel="stylesheet" />
	<script type="text/javascript" src="/Scripts/jquery-1.9.1.min.js"></script>
	<script src="Scripts/bootstrap.min.js"></script>
</head>
<body>
	<form id="formBajaNewsletter" method="post" runat="server">
		<div class="container">
			<div class="header clearfix">
				<a class="navbar-brand" href="Inicio.aspx">
					<img alt="Brand" src="images/LogoWeb.png" /></a>
			</div>
			<div id="divBajaNewsletterControles" runat="server" class="jumbotron">
				<h1 id="lblBajaNewsletterTitulo" runat="server" class="display-3">Baja de suscripción al Newsletter</h1>
				<p id="lblBajaNewsletterMensaje" runat="server" class="lead">Para darse de baja de la suscripción al Newsletter, por favor ingrese su 
					correo electrónico en la caja de texto.</p>
				<p>
					<input id="txtBajaNewsletterCorreoElectronico" runat="server" type="email" class="form-control" placeholder="Ingrese su correo electrónico" />
				</p>
				<div class="form-group">
					<label id="lblBajaNewsletterCategoria" runat="server" class="control-label" for="ddlBajaNewsletterCategoria"></label>
					<asp:DropDownList ID="ddlBajaNewsletterCategoria" class="form-control" runat="server"></asp:DropDownList>
				</div>
				<p><a class="btn btn-lg btn-success" href="Inicio.aspx" role="button" id="btnBajaNewsletterAceptar" runat="server">Aceptar</a></p>
			</div>
			<footer class="footer">
				<p>© Invernar 2016</p>
			</footer>
			<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
			<div class="modal fade" id="modalMensaje" role="dialog">
				<div class="modal-dialog">
					<asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
						<ContentTemplate>
							<div class="modal-content">
								<div class="modal-header">
									<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
									<h4 class="modal-title" id="lblMasterModalMensajeTitulo" runat="server"></h4>
								</div>
								<div class="modal-body">
									<asp:Label ID="lblMasterModalMensajeMensaje" runat="server" Text=""></asp:Label>
								</div>
								<div class="modal-footer">
									<button id="btnMasterModalCerrar" runat="server" type="button" class="btn btn-primary" data-dismiss="modal"></button>
								</div>
							</div>
						</ContentTemplate>
					</asp:UpdatePanel>
				</div>
			</div>
		</div>
	</form>
</body>
