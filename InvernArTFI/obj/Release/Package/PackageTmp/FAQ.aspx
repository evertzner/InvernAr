<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FAQ.aspx.vb" Inherits="InvernArTFI.FAQ" MasterPageFile="~/Master.Master" %>

<%@ MasterType VirtualPath="~/Master.Master" %>
<asp:Content ID="Main" runat="server" ContentPlaceHolderID="Main">
	<!DOCTYPE html>

	<html>
	<head>
		<script src="Scripts/jquery-1.9.1.min.js"></script>
		<script src="Scripts/bootstrap.min.js"></script>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<title></title>
		<style>
			.panel-title {
				text-align: left;
			}

			.linkFAQ {
				color: black;
				font-size: 14px;
				text-underline-position: auto;
				padding: 0 0 0 0;
			}

				.linkFAQ:hover {
					color: black;
				}
		</style>
	</head>
	<body>
		<div class="container">
			<div id="accordion" class="panel-group">
				<div class="faqHeader">Registración</div>
				<div class="panel panel-default">
					<div class="panel-heading">
						<h4 class="panel-title">
							<a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapseOne">¿Cómo hago para registrarme?</a>
						</h4>
					</div>
					<div id="collapseOne" class="panel-collapse collapse in">
						<div class="panel-body">
							Para poder registrarse, usted deberá entrar al siguiente enlace de <b><a href="Registracion.aspx" class="linkFAQ">Registración</a></b>.
						</div>
					</div>
				</div>
				<div class="panel panel-default">
					<div class="panel-heading">
						<h4 class="panel-title">
							<a class="accordion-toggle collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo">¿Qué hago una vez que 
							completo mis datos de registración?</a>
						</h4>
					</div>
					<div id="collapseTwo" class="panel-collapse collapse">
						<div class="panel-body">
							Se le enviará un mail de confirmación en donde deberá entrar al enlace contenido dentro del mismo, y así usted ya podrá acceder 
							a las funciones designadas a los usuarios registrados.
						</div>
					</div>
				</div>
				<div class="panel panel-default">
					<div class="panel-heading">
						<h4 class="panel-title">
							<a class="accordion-toggle collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseThree">¿A qué funciones 
								podré acceder?</a>
						</h4>
					</div>
					<div id="collapseThree" class="panel-collapse collapse">
						<div class="panel-body">
							Usted podrá acceder a las siguientes funciones:
						<ul>
							<li>Datos personales</li>
							<li>Compras realizadas</li>
							<li>Cuenta Coriente</li>
							<li>Compra de productos en el catálogo</li>
							<li>Configuración del invernadero</li>
						</ul>
						</div>
					</div>
				</div>
				<div class="faqHeader">Compra</div>
				<div class="panel panel-default">
					<div class="panel-heading">
						<h4 class="panel-title">
							<a class="accordion-toggle collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseFour">¿Cómo compro un 
								producto del catálogo?</a>
						</h4>
					</div>
					<div id="collapseFour" class="panel-collapse collapse">
						<div class="panel-body">
							Al seleccionar la opción <b>Comprar</b>, éste se añadirá al carrito y el ícono con forma de Chancho, que se encuentra en la parte
							superior derecha, se pondrá de color rojo. Accediendo <b><a href="Comprar.aspx" class="linkFAQ">allí</a></b>, usted podrá tener
							acceso a su carrito y proceder a la compra.               
						</div>
					</div>
				</div>
				<div class="panel panel-default">
					<div class="panel-heading">
						<h4 class="panel-title">
							<a class="accordion-toggle collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseFive">¿Cuáles son los medios
								de pago?</a>
						</h4>
					</div>
					<div id="collapseFive" class="panel-collapse collapse">
						<div class="panel-body">
							Usted podrá pagar con <b>Tarjeta de crédito</b> o con alguna <b>Nota de crédito</b> que tenga a su favor.
						</div>
					</div>
				</div>
				<div class="panel panel-default">
					<div class="panel-heading">
						<h4 class="panel-title">
							<a class="accordion-toggle collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseSix">¿Cómo consulto las 
								compras realizadas?</a>
						</h4>
					</div>
					<div id="collapseSix" class="panel-collapse collapse">
						<div class="panel-body">
							Para consultar su compras realizadas, deberá hacerlo en su pestaña personal en la parte 
							<b><a href="Compras.aspx" class="linkFAQ">Compras</a></b>.
						</div>
					</div>
				</div>
				<div class="panel panel-default">
					<div class="panel-heading">
						<h4 class="panel-title">
							<a class="accordion-toggle collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseSeven">¿Cómo consulto mi 
								cuenta corriente?</a>
						</h4>
					</div>
					<div id="collapseSeven" class="panel-collapse collapse">
						<div class="panel-body">
							Para consultar su cuenta corriente, deberá hacerlo en su pestaña personal en la parte 
							<b><a href="CuentaCorriente.aspx" class="linkFAQ">Cuenta Corriente</a></b>.
						</div>
					</div>
				</div>
				<div class="faqHeader">Personal</div>
				<div class="panel panel-default">
					<div class="panel-heading">
						<h4 class="panel-title">
							<a class="accordion-toggle collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseEight">¿Cómo accedo a mis 
								datos personales?</a>
						</h4>
					</div>
					<div id="collapseEight" class="panel-collapse collapse">
						<div class="panel-body">
							Para acceder a sus datos personales, deberá hacerlo en su pestaña personal en la parte 
							<b><a href="DatosPersonales.aspx" class="linkFAQ">Mis Datos</a></b>.
						</div>
					</div>
				</div>
				<div class="panel panel-default">
					<div class="panel-heading">
						<h4 class="panel-title">
							<a class="accordion-toggle collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseNine">Olvidé mi contraseña,
								¿cómo hago para recuperarla?</a>
						</h4>
					</div>
					<div id="collapseNine" class="panel-collapse collapse">
						<div class="panel-body">
							Para recuperar su contraseña, usted debera entrar al enlace que se encuentra en la parte superior con el texto 
							<b>¿Olvidó su contraseña?</b>. Alli le pedirá su mail, y se le enviará un mail con la contraseña que usted olvidó.
						</div>
					</div>
				</div>
			</div>
		</div>
	</body>

	</html>
</asp:Content>
