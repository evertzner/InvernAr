<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Newsletter.aspx.vb" ValidateRequest="false" Inherits="InvernArTFI.Newsletter" MasterPageFile="~/Master.Master" %>

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
		<div id="divNewsletterControles" runat="server" class="container">
			<div id="wrapper">
				<div id="sidebar-wrapper">
					<ul class="sidebar-nav">
						<li id="liNewsletterNewsletter" runat="server" class="sidebar-brand"></li>
						<li>
							<a id="aNewsletterNuevo" runat="server" href="#"></a>
						</li>
						<li>
							<a id="aNewsletterListado" runat="server" href="#"></a>
						</li>
						<li>
							<a id="aNewsletterCategorias" runat="server" href="#"></a>
						</li>
					</ul>
				</div>
				<div id="divNewsletterNewsletter" class="container" runat="server">
					<div class="vertical-center-row">
						<div class="row">
							<div class="col-md-6">
								<div class="form-group">
									<label id="lblNewsletterNombre" runat="server" class="control-label" for="txtNewsletterNombre"></label>
									<input class="form-control" id="txtNewsletterNombre" runat="server">
								</div>
							</div>
							<div class="col-md-6" style="padding-top: 25px;">
								<div class="form-group">
									<asp:RegularExpressionValidator ID="revNewsletterNombre" runat="server" ControlToValidate="txtNewsletterNombre"
										ValidationExpression="^([\w\s]{1,50})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-8">
								<div class="form-group">
									<label id="lblNewsletterDescripcion" runat="server" class="control-label" for="txtNewsletterDescripcion"></label>
									<input class="form-control" id="txtNewsletterDescripcion" runat="server">
								</div>
							</div>
							<div class="col-md-4" style="padding-top: 25px;">
								<div class="form-group">
									<asp:RegularExpressionValidator ID="revNewsletterDescripcion" runat="server" ControlToValidate="txtNewsletterDescripcion"
										ValidationExpression="^([\w\s\.]+)$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-6">
								<div class="form-group">
									<label id="lblNewsletterAsunto" runat="server" class="control-label" for="txtNewsletterAsunto"></label>
									<input class="form-control" id="txtNewsletterAsunto" runat="server">
								</div>
							</div>
							<div class="col-md-6" style="padding-top: 25px;">
								<div class="form-group">
									<asp:RegularExpressionValidator ID="revNewsletterAsunto" runat="server" ControlToValidate="txtNewsletterAsunto"
										ValidationExpression="^([\w\s]{1,100})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-8">
								<div class="form-group">
									<label id="lblNewsletterCuerpo" runat="server" class="control-label" for="txtNewsletterCuerpo"></label>
									<textarea class="form-control" id="txtNewsletterCuerpo" runat="server" rows="10"></textarea>
								</div>
							</div>
							<div class="col-md-4" style="padding-top: 25px;">
								<div class="form-group">
									<asp:RegularExpressionValidator ID="revNewsletterCuerpo" runat="server" ControlToValidate="txtNewsletterCuerpo"
										ValidationExpression="^([\w\s\.]+)$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-3">
								<div class="form-group">
									<label id="lblNewsletterCategoria" runat="server" class="control-label" for="ddlNewsletterCategoria"></label>
									<asp:DropDownList ID="ddlNewsletterCategoria" class="form-control" runat="server"></asp:DropDownList>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-4">
								<div class="form-group">
									<label id="lblNewsletterImagen" runat="server" class="control-label" for="fuImagen"></label>
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
									<button id="btnNewsletterConfirmar" runat="server" type="button" class="btn btn-success"></button>
								</div>
							</div>
						</div>
						<%--<div class="row">
							<div class="col-md-5">
								<div id="divNewsletterAlerta" runat="server" role="alert"></div>
							</div>
						</div>--%>
					</div>
				</div>
				<div id="divNewsletterListado" class="container" runat="server">
					<div class="vertical-center-row">
						<div class="row">
							<div class="col-md-12">
								<div class="scrolling-table-container" style="overflow-y: scroll; height: 430px; width: 1000px;">
									<asp:GridView ID="gvNewsletterNewsletter" runat="server" CssClass="table table-striped table-bordered table-hover">
										<Columns>
											<asp:CommandField ButtonType="Image" CancelImageUrl="~/images/14_Delete_16x16.png" CancelText="" DeleteText="" CausesValidation="false" EditImageUrl="~/images/05_Edit_16x16.png" EditText="" ShowEditButton="True" UpdateImageUrl="~/images/15_Tick_16x16.png" UpdateText="">
												<ItemStyle Wrap="False" />
											</asp:CommandField>
											<asp:TemplateField>
												<ItemTemplate>
													<asp:ImageButton ID="btnNewsletterEliminar" CommandArgument='<%# Eval("Id")%>' OnClick="btnNewsletterEliminar_Click" runat="server" Text="" ImageUrl="~/images/14_Delete_16x16.png" AlternateText="Eliminar"></asp:ImageButton>
													<cc1:ConfirmButtonExtender ID="cbe" runat="server" DisplayModalPopupID="mpe" TargetControlID="btnNewsletterEliminar"></cc1:ConfirmButtonExtender>
													<cc1:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="btnNewsletterEliminar" OkControlID="btnYes"
														CancelControlID="btnNo" BackgroundCssClass="modalBackground">
													</cc1:ModalPopupExtender>
													<asp:Panel ID="pnlPopup" runat="server" CssClass="modal-dialog" Style="display: none">
														<div class="modal-content">
															<div class="modal-header">
																Confirmación
															</div>
															<div class="modal-body">
																¿Está seguro que desea eliminar el Newsletter?
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
											<asp:TemplateField HeaderText="Nombre">
												<EditItemTemplate>
													<asp:TextBox ID="txtNombre" runat="server" Text='<%# Bind("Nombre")%>' Width="300px" TextMode="MultiLine"></asp:TextBox>
													<asp:RegularExpressionValidator ID="revNewsletterNombre2" ErrorMessage="*" runat="server"
														ControlToValidate="txtNombre" ValidationExpression="^([\w\s]{1,50})$" ForeColor="Red"
														Font-Bold="true"></asp:RegularExpressionValidator>
													<asp:RequiredFieldValidator ID="rfvNewsletterNombre" runat="server" ErrorMessage="*"
														ControlToValidate="txtNombre" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
												</EditItemTemplate>
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# Bind("Nombre")%>' ID="lblNombre"></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Descripcion">
												<EditItemTemplate>
													<asp:TextBox ID="txtDescripcion" runat="server" Text='<%# Bind("Descripcion")%>' Width="300px" TextMode="MultiLine"></asp:TextBox>
													<asp:RegularExpressionValidator ID="revNewsletterDescripcion2" ErrorMessage="*" runat="server"
														ControlToValidate="txtDescripcion" ValidationExpression="^([\w\s\.]+)$" ForeColor="Red"
														Font-Bold="true"></asp:RegularExpressionValidator>
													<asp:RequiredFieldValidator ID="rfvNewsletterDescripcion" runat="server" ErrorMessage="*"
														ControlToValidate="txtDescripcion" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
												</EditItemTemplate>
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# Bind("Descripcion")%>' ID="lblDescripcion"></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Asunto">
												<EditItemTemplate>
													<asp:TextBox ID="txtAsunto" runat="server" Text='<%# Bind("Asunto")%>' Width="300px" TextMode="MultiLine"></asp:TextBox>
													<asp:RegularExpressionValidator ID="revNewsletterAsunto2" ErrorMessage="*" runat="server"
														ControlToValidate="txtAsunto" ValidationExpression="^([\w\s]{1,100})$" ForeColor="Red"
														Font-Bold="true"></asp:RegularExpressionValidator>
													<asp:RequiredFieldValidator ID="rfvNewsletterAsunto" runat="server" ErrorMessage="*"
														ControlToValidate="txtAsunto" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
												</EditItemTemplate>
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# Bind("Asunto")%>' ID="lblAsunto"></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Cuerpo">
												<EditItemTemplate>
													<asp:TextBox ID="txtCuerpo" runat="server" Text='<%# Bind("Cuerpo")%>' Width="300px" TextMode="MultiLine"></asp:TextBox>
													<asp:RegularExpressionValidator ID="revNewsletterCuerpo2" ErrorMessage="*" runat="server"
														ControlToValidate="txtCuerpo" ValidationExpression="^([\w\s\.]+)$" ForeColor="Red"
														Font-Bold="true"></asp:RegularExpressionValidator>
													<asp:RequiredFieldValidator ID="rfvNewsletterCuerpo" runat="server" ErrorMessage="*"
														ControlToValidate="txtCuerpo" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
												</EditItemTemplate>
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# Bind("Cuerpo")%>' ID="lblCuerpo"></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:BoundField DataField="FechaHora" HeaderText="FechaHora" SortExpression="FechaHora" ReadOnly="true" />
											<asp:TemplateField SortExpression="Categoria" HeaderText="Categoria">
												<EditItemTemplate>
													<asp:DropDownList ID="ddlNewsletterCategoriaGV" runat="server" DataSource="<%# ListarNewsletterCategoria()%>">
													</asp:DropDownList>
												</EditItemTemplate>
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# Bind("Categoria")%>' ID="Categoria"></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField ItemStyle-Width="100px" HeaderText="Imagen">
												<ItemTemplate>
													<img src='data:image/jpeg;base64,<%# If(Not Eval("Imagen") Is Nothing, Convert.ToBase64String(DirectCast(Eval("Imagen"), Byte())), String.Empty)%>' style="height: 50px; width: 50px;" />
												</ItemTemplate>
												<EditItemTemplate>
													<asp:FileUpload ID="fuProductosImagen" runat="server" />
												</EditItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField ItemStyle-Width="100px">
												<ItemTemplate>
													<asp:LinkButton ID="EnviarNewsletter" runat="server" CommandName="EnviarNewsletter" CausesValidation="false" OnClick="EnviarNewsletter_Click"
														CommandArgument='<%# Eval("Id") %>' Text="Enviar" CssClass="btn btn-primary" />
												</ItemTemplate>
											</asp:TemplateField>
										</Columns>
									</asp:GridView>
								</div>
							</div>
						</div>
					</div>
				</div>
				<div id="divNewsletterCategorias" class="container" runat="server">
					<div class="vertical-center-row">
						<div class="row">
							<div class="col-md-4">
								<div class="form-group">
									<label id="lblNewsletterNombreCategoria" runat="server" class="control-label" for="txtNewsletterNombreCategoria"></label>
									<input class="form-control" id="txtNewsletterNombreCategoria" runat="server">
								</div>
							</div>
							<div class="col-md-2" style="padding-top: 25px;">
								<div class="form-group">
									<button id="btnNewsletterCategoriaNueva" runat="server" type="button" class="btn btn-success"></button>
								</div>
							</div>
							<div class="col-md-4" style="padding-top: 25px;">
								<div class="form-group">
									<asp:RegularExpressionValidator ID="revNombreCategoria" runat="server" ControlToValidate="txtNewsletterNombreCategoria"
										ValidationExpression="^([\w\s\.]+)$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-md-6">
								<div class="scrolling-table-container" style="overflow-y: scroll; height: 430px; width: 1000px;">
									<asp:GridView ID="gvNewsletterCategorias" runat="server" CssClass="table table-striped table-bordered table-hover">
										<Columns>
											<asp:CommandField ButtonType="Image" CancelImageUrl="~/images/14_Delete_16x16.png" CancelText="" DeleteText="" EditImageUrl="~/images/05_Edit_16x16.png" EditText="" ShowEditButton="True" UpdateImageUrl="~/images/15_Tick_16x16.png" UpdateText="" CausesValidation="false">
												<ItemStyle Wrap="False" />
											</asp:CommandField>
											<asp:TemplateField>
												<ItemTemplate>
													<asp:ImageButton ID="btnNewsletterCategoriaEliminar" CommandArgument='<%# Eval("Id")%>' OnClick="btnNewsletterCategoriaEliminar_Click" runat="server" Text="" ImageUrl="~/images/14_Delete_16x16.png" AlternateText="Eliminar"></asp:ImageButton>
													<cc1:ConfirmButtonExtender ID="cbeCategoria" runat="server" DisplayModalPopupID="mpeCategoria" TargetControlID="btnNewsletterCategoriaEliminar"></cc1:ConfirmButtonExtender>
													<cc1:ModalPopupExtender ID="mpeCategoria" runat="server" PopupControlID="pnlPopupCategoria" TargetControlID="btnNewsletterCategoriaEliminar"
														OkControlID="btnYesCategoria" CancelControlID="btnNoCategoria" BackgroundCssClass="modalBackground">
													</cc1:ModalPopupExtender>
													<asp:Panel ID="pnlPopupCategoria" runat="server" CssClass="modal-dialog" Style="display: none">
														<div class="modal-content">
															<div class="modal-header">
																Confirmación
															</div>
															<div class="modal-body">
																¿Está seguro que desea eliminar la categoría?
															</div>
															<div class="modal-footer">
																<asp:Button ID="btnYesCategoria" CssClass="btn btn-primary btn-success" runat="server" Text="Si" />
																<asp:Button ID="btnNoCategoria" CssClass="btn btn-primary btn-danger" runat="server" Text="No" />
															</div>
														</div>
													</asp:Panel>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" Visible="False" />
											<asp:TemplateField HeaderText="Categoria">
												<EditItemTemplate>
													<asp:TextBox ID="txtCategoria" runat="server" Text='<%# Bind("Categoria")%>' Width="300px" TextMode="MultiLine"></asp:TextBox>
													<asp:RegularExpressionValidator ID="revNewsletterCategoria2" ErrorMessage="*" runat="server"
														ControlToValidate="txtCategoria" ValidationExpression="^([\w\s\.]+)$" ForeColor="Red"
														Font-Bold="true"></asp:RegularExpressionValidator>
													<asp:RequiredFieldValidator ID="rfvNewsletterCategoria" runat="server" ErrorMessage="*"
														ControlToValidate="txtCategoria" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
												</EditItemTemplate>
												<ItemTemplate>
													<asp:Label runat="server" Text='<%# Bind("Categoria")%>' ID="lblCategoria"></asp:Label>
												</ItemTemplate>
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
