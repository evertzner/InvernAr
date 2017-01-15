<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ReporteVentas.aspx.vb" Inherits="InvernArTFI.ReporteVentas" MasterPageFile="~/Master.Master" ValidateRequest="false" %>

<%@ MasterType VirtualPath="~/Master.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Main" runat="server" ContentPlaceHolderID="Main">
	<!DOCTYPE html>

	<html>
	<head>
		<script src="Scripts/jquery-1.9.1.min.js"></script>
		<script src="Scripts/bootstrap.min.js"></script>
		<link href="Content/bootstrap-datetimepicker.css" rel="stylesheet" />
		<script src="Scripts/bootstrap-datetimepicker.js"></script>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<title></title>
		<script type="text/javascript">
			function cargarHTML() {
				var hh = document.getElementById('Main_hiddenHTML');
				hh.value = document.getElementById('Main_reporte').innerHTML;
			}

			function mostrarCalendario(boton) {

				if (boton.id == 'dtpReporteVentasFechaDesde') {
					$('#dtpReporteVentasFechaDesde').datetimepicker({ format: "dd/mm/yyyy", minView: 2, todayBtn: true, endDate: new Date() });
				} else {
					$('#dtpReporteVentasFechaHasta').datetimepicker({ format: "dd/mm/yyyy", minView: 2, todayBtn: true, endDate: new Date() });
				}
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
		</style>
	</head>
	<body>
		<asp:HiddenField ID="hiddenHTML" runat="server" />
		<div id="divUsuariosControles" runat="server" class="container">
			<div class="vertical-center-row">
				<div class="row">
					<div class="col-sm-3">
						<div class="form-group">
							<label id="lblReporteVentasFechaDesde" runat="server" class="control-label" for="dtpReporteVentasFechaDesde"></label>
							<div class="input-group date" id="dtpReporteVentasFechaDesde" onclick="mostrarCalendario(this);">
								<input id="txtReporteVentasFechaDesde" runat="server" type="text" class="form-control" readonly="true" />
								<span class="input-group-addon">
									<span class="glyphicon glyphicon-calendar"></span>
								</span>
							</div>
						</div>
					</div>
					<div class="col-sm-3">
						<div class="form-group">
							<label id="lblReporteVentasFechaHasta" runat="server" class="control-label" for="dtpReporteVentasFechaHasta"></label>
							<div class="input-group date" id="dtpReporteVentasFechaHasta" onclick="mostrarCalendario(this);">
								<input id="txtReporteVentasFechaHasta" runat="server" type="text" class="form-control" readonly="true" />
								<span class="input-group-addon">
									<span class="glyphicon glyphicon-calendar"></span>
								</span>
							</div>
						</div>
					</div>
					<div class="col-sm-4" style="padding-top:25px;">
						<div class="form-group">
							<asp:RadioButton ID="rbReporteVentasAnual" runat="server" GroupName="1" />
							<label id="lblReportesVentasAnual" runat="server" class="control-label" for="rbReporteVentasAnual">Anual</label>
							<asp:RadioButton ID="rbReporteVentasMensual" runat="server" GroupName="1" />
							<label id="lblReporteVentasMensual" runat="server" class="control-label" for="rbReporteVentasMensual"></label>
							<asp:RadioButton ID="rbReporteVentasSemanal" runat="server" GroupName="1" />
							<label id="lbleporteVentasSemanal" runat="server" class="control-label" for="rbReporteVentasSemanal"></label>
							<asp:RadioButton ID="rbReporteVentasDiario" runat="server" GroupName="1" Checked="true" />
							<label id="lblReporteVentasDiario" runat="server" class="control-label" for="rbReporteVentasDiario"></label>
						</div>
					</div>
					<div class="col-md-2" style="padding-top: 24px;">
						<div class="form-group">
							<button id="btnReporteVentasFiltrar" runat="server" type="button" class="btn btn-success form-control"></button>
						</div>
					</div>
				</div>
			</div>
			<div id="reporte" runat="server">
				<cc1:BarChart ID="BarChart1" runat="server" ChartHeight="500" ChartWidth="1100" ChartType="Column" ChartTitleColor="#0E426C" Visible="false"
					CategoryAxisLineColor="#D08AD9" ValueAxisLineColor="#D08AD9" BaseLineColor="#A156AB">
				</cc1:BarChart>
			</div>
			<div class="vertical-center-row">
				<div class="row">
					<div class="col-md-3" style="padding-top: 25px;">
						<div class="form-group">
							<button id="btnReportesExportarPDF" runat="server" type="button" class="btn btn-success" onclick="cargarHTML();"></button>
						</div>
					</div>
				</div>
			</div>
		</div>
	</body>
	</html>
</asp:Content>
