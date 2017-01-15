<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Registracion.aspx.vb" Inherits="InvernArTFI.Registracion" MasterPageFile="~/Master.Master" %>

<%@ MasterType VirtualPath="~/Master.Master" %>
<asp:Content ID="Main" runat="server" ContentPlaceHolderID="Main">
	<!DOCTYPE html>

	<html>
	<head>
		<script src="Scripts/jquery-1.9.1.min.js"></script>
		<script src="Scripts/bootstrap.min.js"></script>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<title></title>
		<script type="text/javascript">
			function cambiarEstadoBoton(checkBox) {
				var b = document.getElementById('Main_btnRegistracionConfirmar');
				if (checkBox.checked == false) {
					b.setAttribute('disabled', 'disabled')
				} else {
					b.removeAttribute('disabled');
				}
			}
		</script>
	</head>
	<body>
		<div id="divRegistracionControles" runat="server" class="container">
			<div class="vertical-center-row">
				<div class="row">
					<div class="col-md-2">
						<div class="form-group">
							<label id="lblRegistracionDNI" runat="server" class="control-label" for="txtRegistracionDNI"></label>
							<input class="form-control" id="txtRegistracionDNI" runat="server" placeholder="Ej. 11111111">
						</div>
					</div>
					<div class="col-md-10" style="padding-top: 25px;">
						<div class="form-group">
							<asp:RegularExpressionValidator ID="revDNI" runat="server" ControlToValidate="txtRegistracionDNI"
								ValidationExpression="^((\d{1,8}))$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-md-2">
						<div class="form-group">
							<label id="lblRegistracionCUIT" runat="server" class="control-label" for="txtRegistracionCUIT"></label>
							<input class="form-control" id="txtRegistracionCUIT" runat="server" placeholder="Ej. 11-11111111-1">
						</div>
					</div>
					<div class="col-md-10" style="padding-top: 25px;">
						<div class="form-group">
							<asp:RegularExpressionValidator ID="revCUIT" runat="server" ControlToValidate="txtRegistracionCUIT"
								ValidationExpression="^(\d{1,2}-)+(\d{1,8}-)+(\d{1,1})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-md-3">
						<div class="form-group">
							<label id="lblRegistracionNombre" runat="server" class="control-label" for="txtRegistracionNombre"></label>
							<input class="form-control" id="txtRegistracionNombre" runat="server" placeholder="Ej. Juan Pablo">
						</div>
					</div>
					<div class="col-md-9" style="padding-top: 25px;">
						<div class="form-group">
							<asp:RegularExpressionValidator ID="revNombre" runat="server" ControlToValidate="txtRegistracionNombre"
								ValidationExpression="^([\D\s]{1,50})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-md-3">
						<div class="form-group">
							<label id="lblRegistracionApellido" runat="server" class="control-label" for="txtRegistracionApellido"></label>
							<input class="form-control" id="txtRegistracionApellido" runat="server" placeholder="Ej. Del Toro">
						</div>
					</div>
					<div class="col-md-9" style="padding-top: 25px;">
						<div class="form-group">
							<asp:RegularExpressionValidator ID="revApellido" runat="server" ControlToValidate="txtRegistracionApellido"
								ValidationExpression="^([\D\s]{1,50})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-md-4">
						<div class="form-group">
							<label id="lblRegistracionDomicilio" runat="server" class="control-label" for="txtRegistracionDomicilio"></label>
							<input class="form-control" id="txtRegistracionDomicilio" runat="server" placeholder="Ej. Calle Falsa 123">
						</div>
					</div>
					<div class="col-md-8" style="padding-top: 25px;">
						<div class="form-group">
							<asp:RegularExpressionValidator ID="revDomicilio" runat="server" ControlToValidate="txtRegistracionDomicilio"
								ValidationExpression="^([\w\s\.]{1,80})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-md-3">
						<div class="form-group">
							<label id="lblRegistracionLocalidad" runat="server" class="control-label" for="txtRegistracionLocalidad"></label>
							<input class="form-control" id="txtRegistracionLocalidad" runat="server" placeholder="Ej. San Isidro">
						</div>
					</div>
					<div class="col-md-9" style="padding-top: 25px;">
						<div class="form-group">
							<asp:RegularExpressionValidator ID="revLocalidad" runat="server" ControlToValidate="txtRegistracionLocalidad"
								ValidationExpression="^([\w\s\.]{1,50})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-md-3">
						<div class="form-group">
							<label id="lblRegistracionProvincia" runat="server" class="control-label" for="txtRegistracionProvincia"></label>
							<input class="form-control" id="txtRegistracionProvincia" runat="server" placeholder="Ej. Buenos Aires">
						</div>
					</div>
					<div class="col-md-9" style="padding-top: 25px;">
						<div class="form-group">
							<asp:RegularExpressionValidator ID="revProvincia" runat="server" ControlToValidate="txtRegistracionProvincia"
								ValidationExpression="^([\w\s\.]{1,50})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-md-2">
						<div class="form-group">
							<label id="lblRegistracionTelefono" runat="server" class="control-label" for="txtRegistracionTelefono"></label>
							<input class="form-control" id="txtRegistracionTelefono" runat="server" placeholder="Ej. 011-1111-1111">
						</div>
					</div>
					<div class="col-md-10" style="padding-top: 25px;">
						<div class="form-group">
							<asp:RegularExpressionValidator ID="revTelefono" runat="server" ControlToValidate="txtRegistracionTelefono"
								ValidationExpression="^([\d]{2,3}-[\d]{3,4}-[\d]{3,4})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-md-2">
						<div class="form-group">
							<label id="lblRegistracionInterno" runat="server" class="control-label" for="txtRegistracionInterno"></label>
							<input class="form-control" id="txtRegistracionInterno" runat="server" placeholder="Ej. 111111">
						</div>
					</div>
					<div class="col-md-10" style="padding-top: 25px;">
						<div class="form-group">
							<asp:RegularExpressionValidator ID="revInterno" runat="server" ControlToValidate="txtRegistracionInterno"
								ValidationExpression="^([\d]{1,10})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-md-2">
						<div class="form-group">
							<label id="lblRegistracionTelefonoCelular" runat="server" class="control-label" for="txtRegistracionTelefonoCelular"></label>
							<input class="form-control" id="txtRegistracionTelefonoCelular" runat="server" placeholder="Ej. 011-15-1111-1111">
						</div>
					</div>
					<div class="col-md-10" style="padding-top: 25px;">
						<div class="form-group">
							<asp:RegularExpressionValidator ID="revTelefonoCelular" runat="server"
								ControlToValidate="txtRegistracionTelefonoCelular"
								ValidationExpression="^([\d]{2,3}-(15)-[\d]{3,4}-[\d]{3,4})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-md-3">
						<div class="form-group">
							<label id="lblRegistracionCorreoElectronico" runat="server" class="control-label" for="txtRegistracionCorreoElectronico"></label>
							<input class="form-control" id="txtRegistracionCorreoElectronico" runat="server" type="email"
								placeholder="Ej. ejemplo@invernar.com.ar">
						</div>
					</div>
					<div class="col-md-9" style="padding-top: 25px;">
						<div class="form-group">
							<asp:RegularExpressionValidator ID="revCorreoElectronico" runat="server"
								ControlToValidate="txtRegistracionCorreoElectronico"
								ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$" ForeColor="Red" Font-Bold="true">
							</asp:RegularExpressionValidator>
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-md-3">
						<div class="form-group">
							<label id="lblRegistracionCorreoElectronicoRepetido" runat="server" class="control-label"
								for="txtRegistracionCorreoElectronicoRepetido">
							</label>
							<input class="form-control" id="txtRegistracionCorreoElectronicoRepetido" runat="server" type="email">
						</div>
					</div>
					<div class="col-md-9" style="padding-top: 25px;">
						<div class="form-group">
							<asp:RegularExpressionValidator ID="revCorreoElectronicoRepetido" runat="server"
								ControlToValidate="txtRegistracionCorreoElectronicoRepetido"
								ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$" ForeColor="Red" Font-Bold="true">
							</asp:RegularExpressionValidator>
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-md-2">
						<div class="form-group">
							<label id="lblRegistracionPassNueva" runat="server" class="control-label" for="txtRegistracionPassNueva"></label>
							<input class="form-control" id="txtRegistracionPassNueva" runat="server" type="password">
						</div>
					</div>
					<div class="col-md-10" style="padding-top: 25px;">
						<div class="form-group">
							<asp:RegularExpressionValidator ID="revPassNueva" runat="server" ControlToValidate="txtRegistracionPassNueva"
								ValidationExpression="^([A-Za-z0-9]{8,35})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-md-2">
						<div class="form-group">
							<label id="lblRegistracionPassNuevaRepetida" runat="server" class="control-label" for="txtRegistracionPassNuevaRepetida"></label>
							<input class="form-control" id="txtRegistracionPassNuevaRepetida" runat="server" type="password">
						</div>
					</div>
					<div class="col-md-10" style="padding-top: 25px;">
						<div class="form-group">
							<asp:RegularExpressionValidator ID="revPassNuevaRepetida" runat="server"
								ControlToValidate="txtRegistracionPassNuevaRepetida"
								ValidationExpression="^([A-Za-z0-9]{8,35})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-md-6">
						<input id="chkRegistracionAceptarTC" type="checkbox" runat="server" onchange="cambiarEstadoBoton(this);" />
						<label id="lblRegistracionAceptarTC" runat="server" style="font-weight: 100;"></label>
						<button id="btnRegistracionTC" runat="server" type="button" class="btn btn-link" onclick="$('#modalTC').modal();" tabindex="99">
						</button>
					</div>
				</div>
				<div class="row">
					<div class="col-md-3">
						<div class="form-group">
							<button id="btnRegistracionConfirmar" runat="server" type="button" class="btn btn-success"></button>
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-md-5">
						<div id="divRegistracionAlerta" runat="server" role="alert"></div>
					</div>
				</div>
			</div>
			<div class="modal fade" id="modalTC" role="dialog">
				<div class="modal-dialog">
					<div class="modal-content">
						<div class="modal-header">
							<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
							<h4 class="modal-title" id="modalRegistracionTitulo" runat="server"></h4>
						</div>
						<div class="modal-body">
							<p><strong>1. Introducción</strong></p>
							<p>
								Las presentes condiciones generales de uso de la página web, regulan los términos y condiciones de acceso y uso de 
								www.direcciónweb.com, propiedad de (indicar el nombre de la empresa o profesional propietario de el portal), con domicilio en 
								(indicar) y con Código de Identificación Fiscal número (indicar), en adelante, «la Empresa», que el usuario del Portal deberá 
								de leer y aceptar para usar todos los servicios e información que se facilitan desde el portal. El mero acceso y/o utilización 
								del portal, de todos o parte de sus contenidos y/o servicios significa la plena aceptación de las presentes condiciones 
								generales de uso.
							</p>
							<p><strong>2. Condiciones de uso</strong></p>
							<p>
								Las presentes condiciones generales de uso del portal regulan el acceso y la utilización del portal, incluyendo los contenidos y 
								los servicios puestos a disposición de los usuarios en y/o a través del portal, bien por el portal, bien por sus usuarios, bien 
								por terceros. No obstante, el acceso y la utilización de ciertos contenidos y/o servicios puede encontrarse sometido a 
								determinadas condiciones específicas.
							</p>
							<p><strong>3.   Modificaciones</strong></p>
							<p>
								La empresa se reserva la facultad de modificar en cualquier momento las condiciones generales de uso del portal. En todo caso, 
								se recomienda que consulte periódicamente los presentes términos de uso del portal, ya que pueden ser modificados.
							</p>
							<p><strong>4. Obligaciones del Usuario</strong></p>
							<p>
								El usuario deberá respetar en todo momento los términos y condiciones establecidos en las presentes condiciones generales de uso 
								del portal. De forma expresa el usuario manifiesta que utilizará el portal de forma diligente y asumiendo cualquier 
								responsabilidad que pudiera derivarse del incumplimiento de las normas.
							</p>
							<p>
								Así mismo, el usuario no podrá utilizar el portal para transmitir, almacenar, divulgar promover o distribuir datos o contenidos 
								que sean portadores de virus o cualquier otro código informático, archivos o programas diseñados para interrumpir, destruir o 
								perjudicar el funcionamiento de cualquier programa o equipo informático o de telecomunicaciones.
							</p>
							<p><strong>5. Responsabilidad del portal</strong></p>
							<p>
								El usuario conoce y acepta que el portal no otorga ninguna garantía de cualquier naturaleza, ya sea expresa o implícita, sobre 
								los datos, contenidos, información y servicios que se incorporan y ofrecen desde el Portal.
							</p>
							<p>
								Exceptuando los casos que la Ley imponga expresamente lo contrario, y exclusivamente con la medida y extensión en que lo 
								imponga, el Portal no garantiza ni asume responsabilidad alguna respecto a los posibles daños y perjuicios causados por el uso 
								y utilización de la información, datos y servicios del Portal.
							</p>
							<p>
								En todo caso, el Portal excluye cualquier responsabilidad por los daños y perjuicios que puedan deberse a la información y/o 
								servicios prestados o suministrados por terceros diferentes de la Empresa. Toda responsabilidad será del tercero ya sea 
								proveedor o colaborador.
							</p>
							<p><strong>6. Propiedad intelectual e industrial</strong></p>
							<p>
								Todos los contenidos, marcas, logos, dibujos, documentación, programas informáticos o cualquier otro elemento susceptible de 
								protección por la legislación de propiedad intelectual o industrial, que sean accesibles en el portal corresponden 
								exclusivamente a la empresa o a sus legítimos titulares y quedan expresamente reservados todos los derechos sobre los mismos. 
								Queda expresamente prohibida la creación de enlaces de hipertexto (links) a cualquier elemento integrante de las páginas web 
								del Portal sin la autorización de la empresa, siempre que no sean a una página web del Portal que no requiera identificación o 
								autenticación para su acceso, o el mismo esté restringido.
							</p>
							<p>
								En cualquier caso, el portal se reserva todos los derechos sobre los contenidos, información datos y servicios que ostente sobre 
								los mismos. El portal no concede ninguna licencia o autorización de uso al usuario sobre sus contenidos, datos o servicios, 
								distinta de la que expresamente se detalle en las presentes condiciones generales de uso del portal.
							</p>
							<p><strong>7. Legislación aplicable, jurisdicción competente y notificaciones</strong></p>
							<p>
								Las presentes condiciones se rigen y se interpretan de acuerdo con las Leyes de España. Para cualquier reclamación serán 
								competentes los juzgados y tribunales de (indicar la ciudad). Todas las notificaciones, requerimientos, peticiones y otras 
								comunicaciones que el Usuario desee efectuar a la Empresa titular del Portal deberán realizarse por escrito y se entenderá que 
								han sido correctamente realizadas cuando hayan sido recibidas en la siguiente dirección (indicar dirección de correo en la que 
								se desean recibir las notificaciones).
							</p>
						</div>
						<div class="modal-footer">
							<button id="btnRegistracionAceptarTC" runat="server" type="button" class="btn btn-primary" data-dismiss="modal"></button>
						</div>
					</div>
				</div>
			</div>
		</div>
	</body>
	</html>
</asp:Content>
