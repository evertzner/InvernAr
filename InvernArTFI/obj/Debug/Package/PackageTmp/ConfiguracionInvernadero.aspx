<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ConfiguracionInvernadero.aspx.vb" Inherits="InvernArTFI.ConfiguracionInvernadero" MasterPageFile="~/Master.Master" %>

<%@ MasterType VirtualPath="~/Master.Master" %>
<asp:Content ID="Main" runat="server" ContentPlaceHolderID="Main">
	<!DOCTYPE html>

	<html>
	<head>
		<script src="Scripts/jquery-1.9.1.min.js"></script>
		<script src="Scripts/bootstrap.min.js"></script>
		<script src="Scripts/jquery-ui.min.js"></script>
		<script src="Scripts/html2canvas.js"></script>
		<link href="Content/ConfiguracionInvernadero.css" rel="stylesheet" />
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<title></title>
		<script type="text/javascript">
			$(document).ready(function () {
				//Counter
				counter = 0;
				//Make element draggable
				$("#Main_Herramientas div").draggable({
					helper: 'clone',
					containment: 'Main_Lienzo',

					//When first dragged
					stop: function (ev, ui) {
						var pos = $(ui.helper).offset();
						objName = "#clonediv" + counter
						$(objName).css({ "left": pos.left, "top": pos.top, "position": "absolute" });
						$(objName).removeClass("drag");

						//When an existiung object is dragged
						$(objName).draggable({
							containment: 'parent',
							stop: function (ev, ui) {
							}
						});
					}
				});
				//Make element droppable
				$("#Main_Lienzo").droppable({
					drop: function (ev, ui) {
						if (ui.helper.context.id.search(/Main_drag[0-9]/) != -1) {
							counter++;
							var element = $(ui.draggable).clone();
							element.addClass("tempclass");
							$(this).append(element);
							$(".tempclass").attr("id", "clonediv" + counter);
							$("#clonediv" + counter).removeClass("tempclass");

							//Get the dynamically item id
							draggedNumber = ui.helper.context.id.search(/Main_drag([0-9])/);
							itemDragged = "dragged" + RegExp.$1;

							$("#clonediv" + counter).addClass(itemDragged);
							sumar();
						}
					}
				});

				$('.itemDragged').dblclick(function () {

					$(this).remove();
					sumar();

				});
			});

			$(document).on('dblclick', '#Main_Lienzo div', function () {
				$(this).remove();
				sumar();
			});

			function SetComponente(id) {
				var hc = document.getElementById('Main_hiddenComponentes');
				hc.value = id;
			}

			function sumar() {
				var suma = 0;
				var ids = '';
				$('#Main_Lienzo div').each(function () {
					var precio = $(this).attr('precio');
					ids += $(this).attr('idProducto') + ';';
					suma += parseFloat(precio);
				});
				SetComponente(ids);
				$('#Presupuesto').text("$ " + suma);
			}

		</script>
	</head>
	<body>
		<asp:HiddenField ID="hiddenComponentes" runat="server" />
		<div class="container">
			<div class="row">
				<div class="col-md-3">
					<label id="PresupuestoLbl" style="font-size: 23px;" >Presupuesto parcial</label><br />
				</div>
				<div class="col-md-2">
					<label id="Presupuesto" style="font-size: 23px;">$ 0</label><br />
				</div>
				<div class="col-md-2">
					<asp:Button runat="server" ID="DescargarPresupuesto" Text="Crear presupuesto" CssClass="btn btn-success" Visible="True" />
				</div>
			</div>

			<div class="row">
				<div id="content" style="padding-top: 10px;">
					<div id="Lienzo" runat="server" style="background-image: url('images/Pasto Canvas.jpg')"></div>
				</div>
				<div class="col-md-3">
					<div id="PanelIzquierdo" class="grid-Container5" style="background-image: url('images/Textura metal.jpg')">
						<div id="Herramientas" runat="server">
						</div>
					</div>
				</div>
			</div>
		</div>
	</body>
	</html>
</asp:Content>
