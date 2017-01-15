<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="GestionEncuestas.aspx.vb" Inherits="InvernArTFI.GestionEncuestas" MasterPageFile="~/Master.Master" %>

<%@ MasterType VirtualPath="~/Master.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Main" runat="server" ContentPlaceHolderID="Main">
	<!DOCTYPE html>

	<html>
	<head>
		<script src="Scripts/jquery-1.9.1.min.js"></script>
		<script src="Scripts/bootstrap.min.js"></script>
		<script src="Scripts/bootstrap-datetimepicker.js"></script>
		<link href="Content/bootstrap-datetimepicker.css" rel="stylesheet" />
		<link href="Content/simple-sidebar.css" rel="stylesheet" />
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<title></title>
		<script type="text/javascript">
			function mostrarCalendario(boton) {
				if (boton.id == 'dtpGestionEncuestasFechaVencimiento') {
					$('#dtpGestionEncuestasFechaVencimiento').datetimepicker({ format: "dd/mm/yyyy", minView: 2, todayBtn: true });
				} else {
					$('#dtpGestionEncuestasFechaVencimientoGV').datetimepicker({ format: "dd/mm/yyyy", minView: 2, todayBtn: true });
				}
			}
		</script>
	</head>
	<body>
		<div id="divGestionEncuestasControles" runat="server" class="container">
			<div class="vertical-center-row">
				<div class="row">
					<div class="col-md-3">
						<div class="form-group">
							<label id="lblGestionEncuestasEncuesta" runat="server" class="control-label" for="txtGestionEncuestasEncuesta"></label>
							<input class="form-control" id="txtGestionEncuestasEncuesta" runat="server" type="text">
						</div>
					</div>
					<div class="col-md-2">
						<div class="form-group">
							<label id="lblGestionEncuestasFechaVencimiento" runat="server" class="control-label" for="dtpGestionEncuestasFechaVencimiento"></label>
							<div class="input-group date" id="dtpGestionEncuestasFechaVencimiento" onclick="mostrarCalendario(this);">
								<input id="txtGestionEncuestasFechaVencimiento" runat="server" type="text" class="form-control" readonly="true" />
								<span class="input-group-addon">
									<span class="glyphicon glyphicon-calendar"></span>
								</span>
							</div>
						</div>
					</div>
					<div class="col-md-1" style="padding-top: 25px;">
						<div class="form-group">
							<button id="btnGestionEncuestasEncuesta" runat="server" type="button" class="btn btn-success"></button>
						</div>
					</div>
					<div class="col-md-1">
						<div class="form-group">
							<label id="lblGestionEncuestaNumeroPregunta" runat="server" class="control-label" for="txtGestionEncuestaNumeroPregunta"></label>
							<input class="form-control" id="txtGestionEncuestaNumeroPregunta" runat="server">
						</div>
					</div>
					<div class="col-md-3" style="padding-top: 25px;">
						<div class="form-group">
							<asp:RegularExpressionValidator ID="revNumeroPregunta" runat="server"
								ControlToValidate="txtGestionEncuestaNumeroPregunta"
								ValidationExpression="^([1-9]|10)?$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-md-3 col-md-offset-6">
						<div class="form-group">
							<label id="lblGestionEncuestasPregunta" runat="server" class="control-label" for="txtGestionEncuestasPregunta"></label>
							<input class="form-control" id="txtGestionEncuestasPregunta" runat="server" type="text">
						</div>
					</div>
					<div class="col-md-1" style="padding-top: 25px;">
						<div class="form-group">
							<button id="btnGestionEncuestasPregunta" runat="server" type="button" class="btn btn-success"></button>
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-md-6">
						<div class="form-group">
							<label id="lblGestionEncuestasEncuestas" runat="server" class="control-label" for="gvGestionEncuestasEncuestas"></label>
							<div class="scrolling-table-container">
								<asp:GridView ID="gvGestionEncuestasEncuestas" runat="server" CssClass="table table-striped table-bordered table-hover" EnableModelValidation="true">
									<Columns>
										<asp:CommandField ButtonType="Image" CancelImageUrl="~/images/14_Delete_16x16.png" CancelText="" DeleteText="" EditImageUrl="~/images/05_Edit_16x16.png" EditText="" ShowEditButton="True" UpdateImageUrl="~/images/15_Tick_16x16.png" UpdateText="">
											<ItemStyle Wrap="False" />
										</asp:CommandField>
										<asp:TemplateField>
											<ItemTemplate>
												<asp:ImageButton ID="btnEncuestasEliminar" CommandArgument='<%# Eval("Id")%>' OnClick="btnEncuestasEliminar_Click" runat="server" Text="" ImageUrl="~/images/14_Delete_16x16.png" AlternateText="Eliminar"></asp:ImageButton>
												<cc1:ConfirmButtonExtender ID="cbe" runat="server" DisplayModalPopupID="mpe" TargetControlID="btnEncuestasEliminar"></cc1:ConfirmButtonExtender>
												<cc1:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="btnEncuestasEliminar" OkControlID="btnYes"
													CancelControlID="btnNo" BackgroundCssClass="modalBackground">
												</cc1:ModalPopupExtender>
												<asp:Panel ID="pnlPopup" runat="server" CssClass="modal-dialog" Style="display: none">
													<div class="modal-content">
														<div class="modal-header">
															Confirmación
														</div>
														<div class="modal-body">
															¿Está seguro que desea eliminar la encuesta?
														</div>
														<div class="modal-footer">
															<asp:Button ID="btnYes" CssClass="btn btn-primary btn-success" runat="server" Text="Si" />
															<asp:Button ID="btnNo" CssClass="btn btn-primary btn-danger" runat="server" Text="No" />
														</div>
													</div>
												</asp:Panel>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" Visible="false" />
										<asp:TemplateField HeaderText="Tema">
											<EditItemTemplate>
												<asp:TextBox ID="txtTema" runat="server" Text='<%# Bind("Tema")%>'></asp:TextBox>
												<asp:RequiredFieldValidator ID="rfvTema" runat="server" ErrorMessage="*" ControlToValidate="txtTema" 
													ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
											</EditItemTemplate>
											<ItemTemplate>
												<asp:Label runat="server" Text='<%# Bind("Tema")%>' ID="lblPregunta"></asp:Label>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField SortExpression="FechaVencimiento" HeaderText="Fecha Vencimiento">
											<EditItemTemplate>
												<div class="input-group date" id="dtpGestionEncuestasFechaVencimientoGV" onclick="mostrarCalendario(this);">
													<input id="txtGestionEncuestasFechaVencimientoGV" runat="server" type="text" class="form-control" 
														readonly="true" value='<%# Bind("FechaVencimiento")%>'  />
													<span class="input-group-addon">
														<span class="glyphicon glyphicon-calendar"></span>
													</span>
													<asp:RequiredFieldValidator ID="rfvFechaVencimiento" runat="server" ErrorMessage="*" 
														ControlToValidate="txtGestionEncuestasFechaVencimientoGV" ForeColor="Red" Font-Bold="true">
													</asp:RequiredFieldValidator>
												</div>
											</EditItemTemplate>
											<ItemTemplate>
												<asp:Label runat="server" Text='<%# Bind("FechaVencimiento")%>' ID="FechaVencimiento"></asp:Label>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:CommandField ButtonType="Image" SelectImageUrl="~/images/15_Tick_16x16.png" ShowSelectButton="True" ItemStyle-HorizontalAlign="Center" />
									</Columns>
								</asp:GridView>
							</div>
						</div>
					</div>
					<div class="col-md-6">
						<div class="form-group">
							<label id="lblGestionEncuestasPreguntas" runat="server" class="control-label" for="gvGestionEncuestasPreguntas"></label>
							<div class="scrolling-table-container" style="overflow-y: scroll; height: 430px;">
								<asp:GridView ID="gvGestionEncuestasPreguntas" runat="server" CssClass="table table-striped table-bordered table-hover">
									<Columns>
										<asp:CommandField ButtonType="Image" CancelImageUrl="~/images/14_Delete_16x16.png" CancelText="" DeleteText="" EditImageUrl="~/images/05_Edit_16x16.png" EditText="" ShowEditButton="True" UpdateImageUrl="~/images/15_Tick_16x16.png" UpdateText="">
											<ItemStyle Wrap="False" />
										</asp:CommandField>
										<asp:TemplateField>
											<ItemTemplate>
												<asp:ImageButton ID="btnEncuestasPreguntasEliminar" CommandArgument='<%# Eval("Id")%>' OnClick="btnEncuestasPreguntasEliminar_Click" runat="server" Text="" ImageUrl="~/images/14_Delete_16x16.png" AlternateText="Eliminar"></asp:ImageButton>
												<cc1:ConfirmButtonExtender ID="cbe" runat="server" DisplayModalPopupID="mpe" TargetControlID="btnEncuestasPreguntasEliminar"></cc1:ConfirmButtonExtender>
												<cc1:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="btnEncuestasPreguntasEliminar" OkControlID="btnYes"
													CancelControlID="btnNo" BackgroundCssClass="modalBackground">
												</cc1:ModalPopupExtender>
												<asp:Panel ID="pnlPopup" runat="server" CssClass="modal-dialog" Style="display: none">
													<div class="modal-content">
														<div class="modal-header">
															Confirmación
														</div>
														<div class="modal-body">
															¿Está seguro que desea eliminar la pregunta?
														</div>
														<div class="modal-footer">
															<asp:Button ID="btnYes" CssClass="btn btn-primary btn-success" runat="server" Text="Si" />
															<asp:Button ID="btnNo" CssClass="btn btn-primary btn-danger" runat="server" Text="No" />
														</div>
													</div>
												</asp:Panel>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" Visible="false" />
										<asp:BoundField DataField="IdEncuesta" HeaderText="IdEncuesta" SortExpression="IdEncuesta" Visible="false" />
										<asp:TemplateField HeaderText="IdPregunta">
											<EditItemTemplate>
												<asp:TextBox ID="txtIdPregunta" runat="server" Text='<%# Bind("IdPregunta")%>' Width="30px"></asp:TextBox>
												<asp:RegularExpressionValidator ID="revNumeroPregunta2" ErrorMessage="*" runat="server"
													ControlToValidate="txtIdPregunta" ValidationExpression="^([1-9]|10)?$" ForeColor="Red"
													Font-Bold="true"></asp:RegularExpressionValidator>
												<asp:RequiredFieldValidator ID="rfvNumeroPregunta" runat="server" ErrorMessage="*"
													ControlToValidate="txtIdPregunta" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
											</EditItemTemplate>
											<ItemTemplate>
												<asp:Label runat="server" Text='<%# Bind("IdPregunta")%>' ID="lblIdPregunta"></asp:Label>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Pregunta">
											<EditItemTemplate>
												<asp:TextBox ID="txtPregunta" runat="server" Text='<%# Bind("Pregunta")%>'></asp:TextBox>
												<asp:RequiredFieldValidator ID="rfvPregunta" runat="server" ErrorMessage="*" ControlToValidate="txtPregunta" 
													ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
											</EditItemTemplate>
											<ItemTemplate>
												<asp:Label runat="server" Text='<%# Bind("Pregunta")%>' ID="lblPregunta"></asp:Label>
											</ItemTemplate>
										</asp:TemplateField>
									</Columns>
								</asp:GridView>
							</div>
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-md-6">
						<div id="divGestionEncuestasAlerta" runat="server" role="alert"></div>
					</div>
				</div>
			</div>
		</div>
	</body>
	</html>
</asp:Content>

