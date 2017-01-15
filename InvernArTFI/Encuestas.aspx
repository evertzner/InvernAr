<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Encuestas.aspx.vb" Inherits="InvernArTFI.Encuestas" MasterPageFile="~/Master.Master" %>

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
		<style>
			label {
				font-weight: 100;
			}
		</style>
	</head>
	<body>
		<div id="divEncuestasControles" runat="server" class="container">
			<div id="divEncuestasEncuesta" runat="server" class="vertical-center-row">
				<div class="row">
					<div class="col-md-4">
						<div class="form-group">
							<label id="lblEncuestasEncuestas" runat="server" class="control-label" for="ddlEncuestasEncuestas" style="font-weight: 700;"></label>
							<asp:DropDownList ID="ddlEncuestasEncuestas" class="form-control" runat="server"></asp:DropDownList>
						</div>
					</div>
					<div class="col-md-2" style="padding-top: 24px;">
						<div class="form-group">
							<button id="btnEncuestasConsultar" runat="server" type="button" class="btn btn-success"></button>
						</div>
					</div>
					<div class="col-md-2" style="padding-top: 24px;">
						<div class="form-group">
							<asp:Button ID="btnEncuestasResponder" runat="server" Text="Button" class="btn btn-success" />
							<cc1:ConfirmButtonExtender ID="cbe" runat="server" DisplayModalPopupID="mpe" TargetControlID="btnEncuestasResponder"></cc1:ConfirmButtonExtender>
							<cc1:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="btnEncuestasResponder" OkControlID="btnYes"
								CancelControlID="btnNo" BackgroundCssClass="modalBackground">
							</cc1:ModalPopupExtender>
							<asp:Panel ID="pnlPopup" runat="server" CssClass="modal-dialog" Style="display: none">
								<div class="modal-content">
									<div class="modal-header">
										Confirmación
									</div>
									<div class="modal-body">
										¿Enviar resultados?
									</div>
									<div class="modal-footer">
										<asp:Button ID="btnYes" CssClass="btn btn-primary btn-success" runat="server" Text="Si" />
										<asp:Button ID="btnNo" CssClass="btn btn-primary btn-danger" runat="server" Text="No" />
									</div>
								</div>
							</asp:Panel>
						</div>
					</div>
					<div class="col-md-4">
						<div id="divEncuestasAlerta" runat="server" role="alert"></div>
					</div>
				</div>
			</div>
			<div id="divEncuestasPreguntas" runat="server" class="vertical-center-row">
				<div id="div1" class="col-md-8">
					<div class="form-group">
						<label id="Label1" runat="server" class="control-label" for="Text1" style="font-size: 20px; font-weight: 700;"></label>
						<br />
						<%--<input id="Text1" runat="server" class="form-control" type="text">--%>
						<asp:RadioButton ID="Radio1a" runat="server" Text="Bajo" GroupName="1" />
						<asp:RadioButton ID="Radio1b" runat="server" Text="Medio" GroupName="1" />
						<asp:RadioButton ID="Radio1c" runat="server" Text="Alto" GroupName="1" />
					</div>
				</div>
				<div id="div2" class="col-md-8">
					<div class="form-group">
						<label id="Label2" runat="server" class="control-label" for="Text2" style="font-size: 20px; font-weight: 700;"></label>
						<br />
						<%--<input id="Text2" runat="server" class="form-control" type="text">--%>
						<asp:RadioButton ID="Radio2a" runat="server" Text="Bajo" GroupName="2" />
						<asp:RadioButton ID="Radio2b" runat="server" Text="Medio" GroupName="2" />
						<asp:RadioButton ID="Radio2c" runat="server" Text="Alto" GroupName="2" />
					</div>
				</div>
				<div id="div3" class="col-md-8">
					<div class="form-group">
						<label id="Label3" runat="server" class="control-label" for="Text3" style="font-size: 20px; font-weight: 700;"></label>
						<br />
						<%--<input id="Text3" runat="server" class="form-control" type="text">--%>
						<asp:RadioButton ID="Radio3a" runat="server" Text="Bajo" GroupName="3" />
						<asp:RadioButton ID="Radio3b" runat="server" Text="Medio" GroupName="3" />
						<asp:RadioButton ID="Radio3c" runat="server" Text="Alto" GroupName="3" />
					</div>
				</div>
				<div id="div4" class="col-md-8">
					<div class="form-group">
						<label id="Label4" runat="server" class="control-label" for="Text4" style="font-size: 20px; font-weight: 700;"></label>
						<br />
						<%--<input id="Text4" runat="server" class="form-control" type="text">--%>
						<asp:RadioButton ID="Radio4a" runat="server" Text="Bajo" GroupName="4" />
						<asp:RadioButton ID="Radio4b" runat="server" Text="Medio" GroupName="4" />
						<asp:RadioButton ID="Radio4c" runat="server" Text="Alto" GroupName="4" />
					</div>
				</div>
				<div id="div5" class="col-md-8">
					<div class="form-group">
						<label id="Label5" runat="server" class="control-label" for="Text5" style="font-size: 20px; font-weight: 700;"></label>
						<br />
						<%--<input id="Text5" runat="server" class="form-control" type="text">--%>
						<asp:RadioButton ID="Radio5a" runat="server" Text="Bajo" GroupName="5" />
						<asp:RadioButton ID="Radio5b" runat="server" Text="Medio" GroupName="5" />
						<asp:RadioButton ID="Radio5c" runat="server" Text="Alto" GroupName="5" />
					</div>
				</div>
				<div id="div6" class="col-md-8">
					<div class="form-group">
						<label id="Label6" runat="server" class="control-label" for="Text6" style="font-size: 20px; font-weight: 700;"></label>
						<br />
						<%--<input id="Text6" runat="server" class="form-control" type="text">--%>
						<asp:RadioButton ID="Radio6a" runat="server" Text="Bajo" GroupName="6" />
						<asp:RadioButton ID="Radio6b" runat="server" Text="Medio" GroupName="6" />
						<asp:RadioButton ID="Radio6c" runat="server" Text="Alto" GroupName="6" />
					</div>
				</div>
				<div id="div7" class="col-md-8">
					<div class="form-group">
						<label id="Label7" runat="server" class="control-label" for="Text7" style="font-size: 20px; font-weight: 700;"></label>
						<br />
						<%--<input id="Text7" runat="server" class="form-control" type="text">--%>
						<asp:RadioButton ID="Radio7a" runat="server" Text="Bajo" GroupName="7" />
						<asp:RadioButton ID="Radio7b" runat="server" Text="Medio" GroupName="7" />
						<asp:RadioButton ID="Radio7c" runat="server" Text="Alto" GroupName="7" />
					</div>
				</div>
				<div id="div8" class="col-md-8">
					<div class="form-group">
						<label id="Label8" runat="server" class="control-label" for="Text8" style="font-size: 20px; font-weight: 700;"></label>
						<br />
						<%--<input id="Text8" runat="server" class="form-control" type="text">--%>
						<div style="font-weight: 100;">
							<asp:RadioButton ID="Radio8a" runat="server" Text="Bajo" GroupName="8" />
							<asp:RadioButton ID="Radio8b" runat="server" Text="Medio" GroupName="8" />
							<asp:RadioButton ID="Radio8c" runat="server" Text="Alto" GroupName="8" />
						</div>
					</div>
				</div>
				<div id="div9" class="col-md-8">
					<div class="form-group">
						<label id="Label9" runat="server" class="control-label" for="Text9" style="font-size: 20px; font-weight: 700;"></label>
						<br />
						<%--<input id="Text9" runat="server" class="form-control" type="text">--%>
						<asp:RadioButton ID="Radio9a" runat="server" Text="Bajo" GroupName="9" />
						<asp:RadioButton ID="Radio9b" runat="server" Text="Medio" GroupName="9" />
						<asp:RadioButton ID="Radio9c" runat="server" Text="Alto" GroupName="9" />
					</div>
				</div>
				<div id="div10" class="col-md-8">
					<div class="form-group">
						<label id="Label10" runat="server" class="control-label" for="Text10" style="font-size: 20px; font-weight: 700;"></label>
						<br />
						<%--<input id="Text10" runat="server" class="form-control" type="text">--%>
						<asp:RadioButton ID="Radio10a" runat="server" Text="Bajo" GroupName="10" />
						<asp:RadioButton ID="Radio10b" runat="server" Text="Medio" GroupName="10" />
						<asp:RadioButton ID="Radio10c" runat="server" Text="Alto" GroupName="10" />
					</div>
				</div>
			</div>
			<div class="modal fade" id="modalEncuestasResponder" role="dialog">
				<div class="modal-dialog">
					<div class="modal-content">
						<div class="modal-header">
							<button type="button" class="close" runat="server" id="btnEncuestasModalCerrar" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
							<h4 class="modal-title" id="modalEncuestasResponderTitle" runat="server"></h4>
						</div>
						<div class="modal-body">
							<div class="form-group">
								<input class="form-control" id="txtEncuestasModalCorreoElectronico" runat="server" type="email" />
								<br />
								<asp:RegularExpressionValidator ID="revCorreoElectronico" runat="server"
									ControlToValidate="txtEncuestasModalCorreoElectronico"
									ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$" ForeColor="Red" Font-Bold="true">
								</asp:RegularExpressionValidator>
							</div>
						</div>
						<div class="modal-footer">
							<button id="btnEncuestasModalAceptar" runat="server" type="button" class="btn btn-primary">Enviar</button>
						</div>
					</div>
				</div>
			</div>
		</div>
	</body>
	</html>
</asp:Content>
