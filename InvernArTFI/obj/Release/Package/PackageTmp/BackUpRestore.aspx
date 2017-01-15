<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BackUpRestore.aspx.vb" Inherits="InvernArTFI.BackUpRestore" MasterPageFile="~/Master.Master" %>

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
	</head>
	<body>
		<div id="divBRControles" runat="server" class="container">
			<div class="vertical-center-row">
				<div class="row">
					<div class="col-md-6">
						<div class="row">
							<div class="col-md-6">
								<div class="form-group">
									<label id="lblBRBackUp" runat="server" class="control-label"></label>
								</div>
							</div>
							
						</div>
						<div class="row">
							<div class="col-md-6">
								<div class="form-group">
									<input class="form-control" id="txtBRDestino" runat="server" type="text">
								</div>
							</div>
							<div class="col-md-5">
								<div class="form-group">
								<asp:RegularExpressionValidator ID="revBRDestino" runat="server" ControlToValidate="txtBRDestino" 
									ValidationExpression="^([\D]?:[\w\W]+(.bak))$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-3">
								<div class="form-group">
									<asp:Button ID="btnBRRealizarBackUp" runat="server" class="btn btn-success" />
									<cc1:ConfirmButtonExtender ID="cbeBackUp" runat="server" DisplayModalPopupID="mpeBackUp" TargetControlID="btnBRRealizarBackUp"></cc1:ConfirmButtonExtender>
									<cc1:ModalPopupExtender ID="mpeBackUp" runat="server" PopupControlID="panelBackUp" TargetControlID="btnBRRealizarBackUp" OkControlID="btnBackUpYes"
										CancelControlID="btnBackUpNo" BackgroundCssClass="modalBackground">
									</cc1:ModalPopupExtender>
									<asp:Panel ID="panelBackUp" runat="server" CssClass="modal-dialog" Style="display: none">
										<div class="modal-content">
											<div class="modal-header">
												Confirmación
											</div>
											<div class="modal-body">
												¿Está seguro que desea realizar el BackUp?
											</div>
											<div class="modal-footer">
												<asp:Button ID="btnBackUpYes" CssClass="btn btn-primary btn-success" runat="server" Text="Si" />
												<asp:Button ID="btnBackUpNo" CssClass="btn btn-primary btn-danger" runat="server" Text="No" />
											</div>
										</div>
									</asp:Panel>
								</div>
							</div>
						</div>
					</div>
					<div class="col-md-6">
						<div class="row">
							<div class="col-md-6">
								<div class="form-group">
									<label id="lblBRRestore" runat="server" class="control-label"></label>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-6">
								<div class="form-group">
									<input runat="server" type="file" class="form-control" id="fuArchivo" placeholder="Archivo" required />
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-3">
								<div class="form-group">
									<asp:Button ID="btnBRRealizarRestore" runat="server" class="btn btn-success" />
									<cc1:ConfirmButtonExtender ID="cbeRestore" runat="server" DisplayModalPopupID="mpeRestore" TargetControlID="btnBRRealizarRestore"></cc1:ConfirmButtonExtender>
									<cc1:ModalPopupExtender ID="mpeRestore" runat="server" PopupControlID="panelRestore" TargetControlID="btnBRRealizarRestore" OkControlID="btnRestoreYes"
										CancelControlID="btnRestoreNo" BackgroundCssClass="modalBackground">
									</cc1:ModalPopupExtender>
									<asp:Panel ID="panelRestore" runat="server" CssClass="modal-dialog" Style="display: none">
										<div class="modal-content">
											<div class="modal-header">
												Confirmación
											</div>
											<div class="modal-body">
												¿Está seguro que desea realizar el Restore?
											</div>
											<div class="modal-footer">
												<asp:Button ID="btnRestoreYes" CssClass="btn btn-primary btn-success" runat="server" Text="Si" />
												<asp:Button ID="btnRestoreNo" CssClass="btn btn-primary btn-danger" runat="server" Text="No" />
											</div>
										</div>
									</asp:Panel>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	</body>
	</html>
</asp:Content>
