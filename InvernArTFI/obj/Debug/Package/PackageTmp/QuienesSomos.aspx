<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="QuienesSomos.aspx.vb" Inherits="InvernArTFI.QuienesSomos" MasterPageFile="~/Master.Master" %>

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
		<div class="panel panel-default container" id="ContenedorContacto">
			<div class="panel-heading">
				<label id="quienesSomosHead" runat="server">Quienes Somos</label>
			</div>
			<div class="panel-body">
				<p>
					<strong>
						<label id="lblIntroduccion" runat="server">INTRODUCCIÓN</label>
					</strong>
				</p>
				<p>InvernAr es una empresa dedicada a la automatización de invernaderos, ofreciendo un producto adaptable y customizable al entorno que necesita el cliente.</p>
				<p>Nuestros clientes son aquellos emprendedores y pequeños productores agrícolas que desean incrementar su productividad obteniendo la misma calidad en sus productos y pudiendo producirlos en cualquier época del año sin estar restringidos a la estacionalidad característica de cada especificación.</p>
				<p>Actualmente, según el escenario impuesto en el mercado, si un cliente desea adquirir la automatización de su invernadero, tiene que comunicarse o asistir a un proveedor de este producto y planear el armado. </p>
				<p>Nuestro producto será comercializado principalmente a través de una plataforma web, donde también se ofrecerá un control en tiempo real de los sensores que se encuentran instalados en el invernadero de los clientes, generando así una ventaja competitiva por sobre nuestros competidores.</p>
				<p>Las características de nuestro desarrollo tecnológico son:</p>
				<ul>
					<li>Herramienta para poder configurar un invernadero con sensores y actuadores pertinentes y de utilidad buscada.</li>
					<li>Presupuestación instantánea según especificaciones previas del cliente luego de la realización del paso anterior.</li>
					<li>Control en tiempo real de los sensores luego de haber sido instalados, proveyendo información pertinente, alarma de valores extremos y estado actual.</li>
				</ul>

				<br />
				<p>
					<strong>
						<label id="lblProposito" runat="server">PROPÓSITO</label></strong>
				</p>
				<p>Gracias a este desarrollo tecnológico, el cliente logrará acortar sus tiempos de planificación y desarrollo, permitiendo así abarcar otras actividades relacionadas a su producción. Al poder configurar su producto en nuestra plataforma, el cliente maneja sus tiempos a su debida necesidad ya que es autosuficiente y no depende de otro agente. Cuándo él lo disponga, generará su presupuesto y evaluará los costos pertinentes, y en caso de necesitarlo, volverá a realizar la configuración que desee, pudiendo así realizarla la cantidad de veces que quiera.</p>
				<p>El control en tiempo real de los sensores genera, en el cliente, un sentimiento de seguridad y tranquilidad, ya que sabe que tiene a su disposición información relevante a toda hora sin tener que recurrir a terceros o a la adquisición de otro dispositivo para poder acceder a ésta.</p>
				<br />
				<p>
					<strong>
						<label id="lblMision" runat="server">MISIÓN</label></strong>
				</p>
				<p>Somos un emprendimiento joven ubicado en la zona norte de la Ciudad de Buenos Aires, y ofrecemos la automatización de invernaderos para productores agrícolas que desarrollan sus actividades dentro de la República Argentina.</p>
				<p>Nos enfocamos en ofrecer nuestro servicio a emprendedores y pequeños productores, generando así una sinergia de crecimiento económico entre ambas partes.</p>
				<br />
				<p>
					<strong>
						<label id="lblVision" runat="server">VISIÓN</label></strong>
				</p>
				<p>Nuestro objetivo es lograr una expansión geográfica plena, donde InvernAr sea reconocida en cada rincón de nuestro país y llegar a brindar servicio a grandes productores agrícolas pero que representan la misma importancia de trato que hay con emprendedores y pequeños productores, no olvidando nunca nuestros orígenes y gracias a quién pudimos lograr lo que llegamos a ser.</p>
				<br />
			</div>
		</div>
	</body>
	</html>
</asp:Content>
