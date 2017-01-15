<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Bitacora.aspx.vb" Inherits="InvernArTFI.Bitacora" MasterPageFile="~/Master.Master" %>

<%@ MasterType VirtualPath="~/Master.Master" %>
<asp:Content ID="Main" runat="server" ContentPlaceHolderID="Main">
	<!DOCTYPE html>
	<html>
	<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
		<ContentTemplate>
			<head>
				<script src="Scripts/jquery-1.9.1.min.js"></script>
				<script src="Scripts/bootstrap.min.js"></script>
				<link href="Content/bootstrap-datetimepicker.css" rel="stylesheet" />
				<script src="Scripts/bootstrap-datetimepicker.js"></script>
				<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
				<title></title>
				<script type="text/javascript">
					function mostrarCalendario(boton) {

						if (boton.id == 'dtpBitacoraFechaDesde') {
							$('#dtpBitacoraFechaDesde').datetimepicker({ format: "dd/mm/yyyy", minView: 2, todayBtn: true, endDate: new Date() });
						} else {
							$('#dtpBitacoraFechaHasta').datetimepicker({ format: "dd/mm/yyyy", minView: 2, todayBtn: true, endDate: new Date() });
						}
					}
				</script>
			</head>
			<body>
				<div id="divBitacoraControles" runat="server" class="container">
					<div class="vertical-center-row">
						<div class="row">
							<div class="col-md-4">
								<div class="form-group">
									<label id="lblBitacoraUsuarios" runat="server" class="control-label" for="ddlBitacoraUsuarios"></label>
									<asp:DropDownList ID="ddlBitacoraUsuarios" class="form-control" runat="server"></asp:DropDownList>
								</div>
							</div>
							<div class="col-sm-3">
								<div class="form-group">
									<label id="lblBitacoraFechaDesde" runat="server" class="control-label" for="dtpBitacoraFechaDesde"></label>
									<div class="input-group date" id="dtpBitacoraFechaDesde" onclick="mostrarCalendario(this);">
										<input id="txtBitacoraFechaDesde" runat="server" type="text" class="form-control" readonly="true"/>
										<span class="input-group-addon">
											<span class="glyphicon glyphicon-calendar"></span>
										</span>
									</div>
								</div>
							</div>
							<div class="col-sm-3">
								<div class="form-group">
									<label id="lblBitacoraFechaHasta" runat="server" class="control-label" for="dtpBitacoraFechaHasta"></label>
									<div class="input-group date" id="dtpBitacoraFechaHasta" onclick="mostrarCalendario(this);">
										<input id="txtBitacoraFechaHasta" runat="server" type="text" class="form-control" readonly="truew"/>
										<span class="input-group-addon">
											<span class="glyphicon glyphicon-calendar"></span>
										</span>
									</div>
								</div>
							</div>
							<div class="col-md-2" style="padding-top: 24px;">
								<div class="form-group">
									<button id="btnBitacoraFiltrar" runat="server" type="button" class="btn btn-success form-control"></button>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-12">
								<asp:GridView ID="gvBitacoraBitacora" runat="server" CssClass="table table-striped table-bordered table-hover" AllowPaging="True" PageSize="40">
									<Columns>
										<asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" Visible="False" />
										<asp:BoundField DataField="FechaActividad" HeaderText="Fecha Actividad" InsertVisible="False" ReadOnly="True" SortExpression="FechaActividad" />
										<asp:BoundField DataField="Usuario" HeaderText="Usuario" InsertVisible="False" ReadOnly="True" SortExpression="Usuario" />
										<asp:BoundField DataField="Actividad" HeaderText="Actividad" InsertVisible="False" ReadOnly="True" SortExpression="Actividad" />
									</Columns>
								</asp:GridView>
							</div>
						</div>
					</div>
				</div>
			</body>
		</ContentTemplate>
	</asp:UpdatePanel>
	</html>
</asp:Content>
