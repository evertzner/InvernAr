<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Multidioma.aspx.vb" Inherits="InvernArTFI.Multidioma" MasterPageFile="~/Master.Master" %>

<%@ MasterType VirtualPath="~/Master.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Main" runat="server" ContentPlaceHolderID="Main">
	<!DOCTYPE html>

	<html>
	<head>
		<script src="Scripts/jquery-1.9.1.min.js"></script>
		<script src="Scripts/bootstrap.min.js"></script>
		<link href="Content/simple-sidebar.css" rel="stylesheet" />
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<title></title>
	</head>
	<body>
		<div id="divMultidiomaControles" runat="server" class="container">
			<div id="wrapper">
				<div id="sidebar-wrapper">
					<ul class="sidebar-nav">
						<li id="liMultidiomaMultidioma" runat="server" class="sidebar-brand"></li>
						<li>
							<a id="aMultidiomaAdministrar" runat="server" href="#"></a>
						</li>
						<li>
							<a id="aMultidiomaNuevo" runat="server" href="#"></a>
						</li>
					</ul>
				</div>
				<div id="divMultidiomaAdministrar" class="container" runat="server">
					<div class="vertical-center-row">
						<div class="row">
							<div class="col-md-3">
								<div class="form-group">
									<label id="lblMultidiomaIdiomasTraducidos" runat="server" class="control-label" for="ddlMultidiomaIdiomasTraducidos"></label>
									<asp:DropDownList ID="ddlMultidiomaIdiomasTraducidos" class="form-control" runat="server"></asp:DropDownList>
								</div>
							</div>
							<div class="col-md-3" style="padding-top: 24px;">
								<div class="form-group">
									<button class="btn btn-success form-control" id="btnMultidiomaConsultarTraduccion" runat="server" type="button"></button>
								</div>
							</div>
							<div class="col-md-3" style="padding-top: 24px;">
								<div class="form-group">
									<asp:Button ID="btnMultidiomaEliminar" class="btn btn-success form-control" runat="server" />
									<cc1:ConfirmButtonExtender ID="cbe1" runat="server" DisplayModalPopupID="mpe1" TargetControlID="btnMultidiomaEliminar"></cc1:ConfirmButtonExtender>
									<cc1:ModalPopupExtender ID="mpe1" runat="server" PopupControlID="pnlPopup1" TargetControlID="btnMultidiomaEliminar" OkControlID="btnYes1"
										CancelControlID="btnNo1" BackgroundCssClass="modalBackground">
									</cc1:ModalPopupExtender>
									<asp:Panel ID="pnlPopup1" runat="server" CssClass="modal-dialog" Style="display: none">
										<div class="modal-content">
											<div class="modal-header">
												Confirmación
											</div>
											<div class="modal-body">
												¿Está seguro que desea eliminar el idioma seleccionado?
											</div>
											<div class="modal-footer">
												<asp:Button ID="btnYes1" CssClass="btn btn-primary btn-success" runat="server" Text="Si" />
												<asp:Button ID="btnNo1" CssClass="btn btn-primary btn-danger" runat="server" Text="No" />
											</div>
										</div>
									</asp:Panel>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-12">
								<div class="scrolling-table-container" style="overflow-y: scroll; width: 1000px; height: 400px;">
									<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
										<ContentTemplate>
											<asp:GridView ID="gvMultidiomaMultidioma" runat="server" CssClass="table table-striped table-bordered table-hover">
												<Columns>
													<asp:CommandField ButtonType="Image" CancelImageUrl="~/images/14_Delete_16x16.png" CancelText="" DeleteText="" EditImageUrl="~/images/05_Edit_16x16.png" EditText="" ShowEditButton="True" UpdateImageUrl="~/images/15_Tick_16x16.png" UpdateText="">
														<ItemStyle Wrap="False" />
													</asp:CommandField>
													<asp:BoundField DataField="Idioma" HeaderText="Idioma" SortExpression="Idioma" Visible="False" />
													<asp:BoundField DataField="Etiqueta" HeaderText="Etiqueta" SortExpression="Etiqueta" ReadOnly="true">
														<HeaderStyle Wrap="False" />
														<ItemStyle Wrap="False" />
													</asp:BoundField>
													<asp:BoundField DataField="Traduccion" HeaderText="Traduccion" SortExpression="Traduccion">
														<HeaderStyle Wrap="False" />
														<ItemStyle Wrap="False" />
													</asp:BoundField>
												</Columns>
											</asp:GridView>
										</ContentTemplate>
									</asp:UpdatePanel>
								</div>
							</div>
						</div>
					</div>
				</div>
				<div id="divMultidiomaNuevo" class="container" runat="server">
					<div class="vertical-center-row">
						<div class="row">
							<div class="col-md-3">
								<div class="form-group">
									<label id="lblMultidiomaIdiomasNoTraducidos" runat="server" class="control-label" for="ddlMultidiomaIdiomasNoTraducidos"></label>
									<asp:DropDownList ID="ddlMultidiomaIdiomasNoTraducidos" class="form-control" runat="server"></asp:DropDownList>
								</div>
							</div>
							<div class="col-md-3" style="padding-top: 24px;">
								<div class="form-group">
									<button class="btn btn-success form-control" id="btnMultidiomaConsultarTraduccionNueva" runat="server" type="button"></button>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-12">
								<div class="scrolling-table-container" style="overflow-y: scroll; width: 1000px; height: 400px;">
									<asp:GridView ID="gvMultidiomaNuevo" runat="server" CssClass="table table-striped table-bordered table-hover">
										<Columns>
											<asp:BoundField DataField="Idioma" HeaderText="Idioma" SortExpression="Idioma" Visible="False" />
											<asp:BoundField DataField="Etiqueta" HeaderText="Etiqueta" SortExpression="Etiqueta" ReadOnly="true">
												<HeaderStyle Wrap="False" />
												<ItemStyle Wrap="False" Width="250px" />
											</asp:BoundField>
											<asp:TemplateField HeaderText="Traduccion" SortExpression="Traduccion">
												<ItemTemplate>
													<asp:TextBox ID="txtMultidiomaTraduccion" runat="server" Text='<%# Eval("Traduccion")%>' Width="650px"></asp:TextBox>
												</ItemTemplate>
											</asp:TemplateField>
										</Columns>
									</asp:GridView>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-3">
								<div class="form-group">
									<asp:Button ID="btnMultidiomaAceptarCambios" class="btn btn-success form-control" runat="server" />
									<cc1:ConfirmButtonExtender ID="cbe2" runat="server" DisplayModalPopupID="mpe2" TargetControlID="btnMultidiomaAceptarCambios"></cc1:ConfirmButtonExtender>
									<cc1:ModalPopupExtender ID="mpe2" runat="server" PopupControlID="pnlPopup2" TargetControlID="btnMultidiomaAceptarCambios" OkControlID="btnYes2"
										CancelControlID="btnNo2" BackgroundCssClass="modalBackground">
									</cc1:ModalPopupExtender>
									<asp:Panel ID="pnlPopup2" runat="server" CssClass="modal-dialog" Style="display: none">
										<div class="modal-content">
											<div class="modal-header">
												Confirmación
											</div>
											<div class="modal-body">
												¿Está seguro que desea aplicar los cambios?
											</div>
											<div class="modal-footer">
												<asp:Button ID="btnYes2" CssClass="btn btn-primary btn-success" runat="server" Text="Si" />
												<asp:Button ID="btnNo2" CssClass="btn btn-primary btn-danger" runat="server" Text="No" />
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
