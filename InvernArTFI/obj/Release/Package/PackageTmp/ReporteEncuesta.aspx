<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ReporteEncuesta.aspx.vb" Inherits="InvernArTFI.ReporteEncuesta" MasterPageFile="~/Master.Master" ValidateRequest="false" %>

<%@ MasterType VirtualPath="~/Master.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Main" runat="server" ContentPlaceHolderID="Main">
	<!DOCTYPE html>

	<html>
	<head>
		<script src="Scripts/jquery-1.9.1.min.js"></script>
		<script src="Scripts/bootstrap.min.js"></script>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<title></title>
		<script type="text/javascript">
			function cargarHTML() {
				var hh = document.getElementById('Main_hiddenHTML');
				hh.value = document.getElementById('Main_reporte').innerHTML;
			}
		</script>
		<style>
			canvas {
				max-height: 100%;
				max-width: 100%;
			}

			#canvas {
				width: 400px;
				height: 300px;
				border-style: solid;
				border-width: 2px;
			}

			#Main_BarChart1__ParentDiv {
				width: 1150px;
			}

			#SeriesAxis {
				color: red;
				fill: #232323;
				fill-opacity: 1;
				font-family: Arial,Helvetica,sans-serif;
				font-size: 11px;
			}

			/*svg {
				padding-left: 130px;
			}*/
		</style>
	</head>
	<body>
		<asp:HiddenField ID="hiddenHTML" runat="server" />
		<div id="divUsuariosControles" runat="server" class="container">
			<div class="vertical-center-row">
				<div class="row">
					<div class="col-md-3">
						<div class="form-group">
							<asp:RadioButton ID="rbReporteEncuestaNoVencido" runat="server" GroupName="1" Checked="true" />
							<label id="lblReporteEncuestaNoVencido" runat="server" class="control-label" for="rbReporteEncuestaNoVencido"></label>
							<asp:RadioButton ID="rbReporteEncuestaVencido" runat="server" GroupName="1" />
							<label id="lblReporteEncuestaVencido" runat="server" class="control-label" for="rbReporteEncuestaVencido"></label>
							<asp:RadioButton ID="rbReporteEncuestaTodo" runat="server" GroupName="1" />
							<label id="lblReporteEncuestaTodo" runat="server" class="control-label" for="rbReporteEncuestaTodo"></label>
						</div>
					</div>
					<div class="col-md-2">
						<div class="form-group">
							<button id="btnReporteEncuestaCargarEncuestas" runat="server" type="button" class="btn btn-success"></button>
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-md-4">
						<div class="form-group">
							<label id="lblReportesEncuestas" runat="server" class="control-label" for="ddlReportesEncuestas"></label>
							<asp:DropDownList ID="ddlReportesEncuestas" class="form-control" runat="server" OnSelectedIndexChanged="ddlReportesEncuestas_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
						</div>
					</div>
					<div class="col-md-3 col-md-offset-4" style="padding-top: 25px;">
						<div class="form-group">
							<button id="btnReportesExportarPDF" runat="server" type="button" class="btn btn-success" onclick="cargarHTML();"></button>
						</div>
					</div>
				</div>
			</div>
			<div id="reporte" runat="server">
				<cc1:BarChart ID="BarChart1" runat="server" ChartHeight="500" ChartWidth="1100" ChartType="Column" ChartTitleColor="#0E426C" Visible="false"
					CategoryAxisLineColor="#D08AD9" ValueAxisLineColor="#D08AD9" BaseLineColor="#A156AB">
				</cc1:BarChart>
			</div>
		</div>
	</body>
	</html>
</asp:Content>

