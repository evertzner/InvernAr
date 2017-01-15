<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="GestionNoticias.aspx.vb" Inherits="InvernArTFI.GestionNoticias" MasterPageFile="~/Master.Master" %>

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
		<div id="divGestionNoticiasControles" runat="server" class="container">
			<div id="wrapper">
				<div id="sidebar-wrapper">
					<ul class="sidebar-nav">
						<li id="liGestionNoticiasNoticias" runat="server" class="sidebar-brand"></li>
						<li>
							<a id="aGestionNoticiasNueva" runat="server" href="#"></a>
						</li>
						<li>
							<a id="aGestionNoticiasListado" runat="server" href="#"></a>
						</li>
					</ul>
				</div>
				<div id="divGestionNoticiasNoticias" class="container" runat="server">
					<div class="vertical-center-row">
						<div class="row">
							<div class="col-md-6">
								<div class="form-group">
									<label id="lblGestionNoticiasTitulo" runat="server" class="control-label" for="txtGestionNoticiasTitulo"></label>
									<input class="form-control" id="txtGestionNoticiasTitulo" runat="server">
								</div>
							</div>
							<div class="col-md-6" style="padding-top: 25px;">
								<div class="form-group">
									<asp:RegularExpressionValidator ID="revNoticiasTitulo" runat="server" ControlToValidate="txtGestionNoticiasTitulo"
										ValidationExpression="^([\w\s\.]+)$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-8">
								<div class="form-group">
									<label id="lblGestionNoticiasContenido" runat="server" class="control-label" for="txtGestionNoticiasContenido"></label>
									<textarea class="form-control" id="txtGestionNoticiasContenido" runat="server" rows="10"></textarea>
								</div>
							</div>
							<div class="col-md-4" style="padding-top: 25px;">
								<div class="form-group">
									<asp:RegularExpressionValidator ID="revNoticiasContenido" runat="server" ControlToValidate="txtGestionNoticiasContenido"
										ValidationExpression="^([\w\s\.]+)$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-4">
								<div class="form-group">
									<label id="lblGestionNoticiasImagen" runat="server" class="control-label" for="fuImagen"></label>
									<input runat="server" type="file" class="form-control" enctype="multipart/form-data" onchange="readURL(this);" id="fuImagen" placeholder="Imagen" required />
								</div>
							</div>
							<div class="col-md-5">
								<div class="imgVisualizerContainer">
									<img id="imageVisualizer" src="#" alt="" style="visibility: hidden; width: 300px;" />
									<div id="removerimagen"><span id="EliminarImagen" onclick="eliminarImg();" class="glyphicon glyphicon-trash" aria-hidden="true" style="visibility: hidden"></span></div>
								</div>
							</div>

						</div>
						<div class="row">
							<div class="col-md-3">
								<div class="form-group">
									<button id="btnGestionNoticiasConfirmar" runat="server" type="button" class="btn btn-success"></button>
								</div>
							</div>
						</div>
						<%--<div class="row">
							<div class="col-md-5">
								<div id="divGestionNoticiasAlerta" runat="server" role="alert"></div>
							</div>
						</div>--%>
					</div>
				</div>
				<div id="divGestionNoticiasListado" class="container" runat="server">
					<div class="vertical-center-row">
						<div class="row">
							<div class="col-md-12">
								<div class="scrolling-table-container" style="overflow-y: scroll; height: 430px; width: 1000px;">
									<asp:GridView ID="gvGestionNoticiasNoticias" runat="server" CssClass="table table-striped table-bordered table-hover">
										<Columns>
											<asp:CommandField ButtonType="Image" CausesValidation="false" CancelImageUrl="~/images/14_Delete_16x16.png" CancelText="" DeleteText="" EditImageUrl="~/images/05_Edit_16x16.png" EditText="" ShowEditButton="True" UpdateImageUrl="~/images/15_Tick_16x16.png" UpdateText="">
												<ItemStyle Wrap="False" />
											</asp:CommandField>
											<asp:TemplateField>
												<ItemTemplate>
													<asp:ImageButton ID="btnGestionNoticiasEliminar" CommandArgument='<%# Eval("Id")%>' OnClick="btnGestionNoticiasEliminar_Click" runat="server" Text="" ImageUrl="~/images/14_Delete_16x16.png" AlternateText="Eliminar"></asp:ImageButton>
													<cc1:ConfirmButtonExtender ID="cbe" runat="server" DisplayModalPopupID="mpe" TargetControlID="btnGestionNoticiasEliminar"></cc1:ConfirmButtonExtender>
													<cc1:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="btnGestionNoticiasEliminar" OkControlID="btnYes"
														CancelControlID="btnNo" BackgroundCssClass="modalBackground">
													</cc1:ModalPopupExtender>
													<asp:Panel ID="pnlPopup" runat="server" CssClass="modal-dialog" Style="display: none">
														<div class="modal-content">
															<div class="modal-header">
																Confirmación
															</div>
															<div class="modal-body">
																¿Está seguro que desea eliminar la noticia?
															</div>
															<div class="modal-footer">
																<asp:Button ID="btnYes" CssClass="btn btn-primary btn-success" runat="server" Text="Si" />
																<asp:Button ID="btnNo" CssClass="btn btn-primary btn-danger" runat="server" Text="No" />
															</div>
														</div>
													</asp:Panel>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" Visible="False" />
											<asp:TemplateField HeaderText="Titulo">
												<EditItemTemplate>
													<asp:TextBox ID="txtTitulo" runat="server" Text='<%# Bind("Titulo")%>' Width="300px"></asp:TextBox>
													<asp:RegularExpressionValidator ID="revNoticiasTitulo2" ErrorMessage="*" runat="server"
														ControlToValidate="txtTitulo" ValidationExpression="^([\w\s\.]+)$" ForeColor="Red"
														Font-Bold="true"></asp:RegularExpressionValidator>
													<asp:RequiredFieldValidator ID="rfvNoticiasTitulo" runat="server" ErrorMessage="*"
														ControlToValidate="txtTitulo" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
												</EditItemTemplate>
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# Bind("Titulo")%>' ID="lblTitulo"></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Contenido">
												<EditItemTemplate>
													<asp:TextBox ID="txtContenido" runat="server" Text='<%# Bind("Contenido")%>' Width="300px" TextMode="MultiLine"></asp:TextBox>
													<asp:RegularExpressionValidator ID="revNoticiasContenido2" ErrorMessage="*" runat="server"
														ControlToValidate="txtContenido" ValidationExpression="^([\w\s\.]+)$" ForeColor="Red"
														Font-Bold="true"></asp:RegularExpressionValidator>
													<asp:RequiredFieldValidator ID="rfvNoticiasContenido" runat="server" ErrorMessage="*"
														ControlToValidate="txtContenido" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
												</EditItemTemplate>
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# Bind("Contenido")%>' ID="lblContenido"></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:BoundField DataField="FechaHora" HeaderText="FechaHora" SortExpression="FechaHora" ReadOnly="true" />
											<asp:TemplateField ItemStyle-Width="100px" HeaderText="Imagen">
												<ItemTemplate>
													<img src='data:image/jpeg;base64,<%# If(Not Eval("Imagen") Is Nothing, Convert.ToBase64String(DirectCast(Eval("Imagen"), Byte())), String.Empty)%>' style="height: 50px; width: 50px;" />
												</ItemTemplate>
												<EditItemTemplate>
													<asp:FileUpload ID="fuProductosImagen" runat="server" />
												</EditItemTemplate>
											</asp:TemplateField>
										</Columns>
									</asp:GridView>
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
