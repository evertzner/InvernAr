<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AsignacionRoles.aspx.vb" Inherits="InvernArTFI.AsignacionRoles" MasterPageFile="~/Master.Master" %>

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
		<div id="divAsignacionRolesControles" runat="server" class="container">
			<div class="vertical-center-row">
				<div class="row">
					<div class="col-md-6">
						<div class="form-group">
							<label id="lblAsignacionRolesRoles" runat="server" class="control-label" for="gvAsignacionRolesRoles"></label>
							<div class="scrolling-table-container">
								<asp:GridView ID="gvAsignacionRolesRoles" runat="server" CssClass="table table-striped table-bordered table-hover">
									<Columns>
										<asp:CommandField ButtonType="Image" CancelImageUrl="~/images/14_Delete_16x16.png" CancelText="" DeleteText="" EditImageUrl="~/images/05_Edit_16x16.png" EditText="" ShowEditButton="True" UpdateImageUrl="~/images/15_Tick_16x16.png" UpdateText="">
											<ItemStyle Wrap="False" />
										</asp:CommandField>
										<asp:TemplateField>
											<ItemTemplate>
												<asp:ImageButton ID="btnAsignacionRolesEliminarRol" CommandArgument='<%# Eval("Codigo")%>' OnClick="btnAsignacionRolesEliminarRol_Click" runat="server" Text="" ImageUrl="~/images/14_Delete_16x16.png" AlternateText="Eliminar"></asp:ImageButton>
												<cc1:ConfirmButtonExtender ID="cbe" runat="server" DisplayModalPopupID="mpe" TargetControlID="btnAsignacionRolesEliminarRol"></cc1:ConfirmButtonExtender>
												<cc1:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="btnAsignacionRolesEliminarRol" OkControlID="btnYes"
													CancelControlID="btnNo" BackgroundCssClass="modalBackground">
												</cc1:ModalPopupExtender>
												<asp:Panel ID="pnlPopup" runat="server" CssClass="modal-dialog" Style="display: none">
													<div class="modal-content">
														<div class="modal-header">
															Confirmación
														</div>
														<div class="modal-body">
															¿Está seguro que desea eliminar el rol?
														</div>
														<div class="modal-footer">
															<asp:Button ID="btnYes" CssClass="btn btn-primary btn-success" runat="server" Text="Si" />
															<asp:Button ID="btnNo" CssClass="btn btn-primary btn-danger" runat="server" Text="No" />
														</div>
													</div>
												</asp:Panel>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:BoundField DataField="Codigo" HeaderText="Codigo" SortExpression="Codigo" ReadOnly="true">
											<HeaderStyle Wrap="False" />
											<ItemStyle Wrap="False" />
										</asp:BoundField>
										<asp:TemplateField HeaderText="Nombre">
											<EditItemTemplate>
												<asp:TextBox ID="txtNombre" runat="server" Text='<%# Bind("Nombre")%>' Width="300px"></asp:TextBox>
												<asp:RegularExpressionValidator ID="revNombre2" ErrorMessage="*" runat="server"
													ControlToValidate="txtNombre" ValidationExpression="^([\w\s]{1,50})$" ForeColor="Red"
													Font-Bold="true"></asp:RegularExpressionValidator>
												<asp:RequiredFieldValidator ID="rfvNombre" runat="server" ErrorMessage="*"
													ControlToValidate="txtNombre" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
											</EditItemTemplate>
											<ItemTemplate>
												<asp:Label runat="server" Text='<%# Bind("Nombre")%>' ID="lblNombre"></asp:Label>
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
							<label id="lblAsignacionRolesPermisos" runat="server" class="control-label" for="gvAsignacionRolesPermisos"></label>
							<div class="scrolling-table-container" style="overflow-y: scroll; height: 430px;">
								<asp:GridView ID="gvAsignacionRolesPermisos" runat="server" CssClass="table table-striped table-bordered table-hover">
									<Columns>
										<asp:TemplateField ItemStyle-HorizontalAlign="Center">
											<ItemTemplate>
												<asp:CheckBox ID="chkAsignacionRolesSeleccionado" runat="server" CommandName="Select" />
											</ItemTemplate>
										</asp:TemplateField>
										<asp:BoundField DataField="Codigo" HeaderText="Codigo" SortExpression="Codigo" ReadOnly="true">
											<HeaderStyle Wrap="False" />
											<ItemStyle Wrap="False" />
										</asp:BoundField>
										<asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" ReadOnly="true">
											<HeaderStyle Wrap="False" />
											<ItemStyle Wrap="False" />
										</asp:BoundField>
										<asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="Descripcion" ReadOnly="true">
											<HeaderStyle Wrap="False" />
											<ItemStyle Wrap="False" />
										</asp:BoundField>
									</Columns>
								</asp:GridView>
							</div>
						</div>
					</div>
				</div>
				<div class="vertical-center-row">
					<div class="row">
						<div class="col-md-1">
							<div class="form-group">
								<label id="lblAsignacionRolesCodigo" runat="server" class="control-label" for="txtAsignacionRolesCodigo"></label>
								<input class="form-control" id="txtAsignacionRolesCodigo" runat="server">
								<asp:RegularExpressionValidator ID="revCodigoRol" runat="server" ControlToValidate="txtAsignacionRolesCodigo"
									ValidationExpression="^([a-zA-Z0-9]{1,5})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
							</div>
						</div>
						<div class="col-md-3">
							<div class="form-group">
								<label id="lblAsignacionRolesNombre" runat="server" class="control-label" for="txtAsignacionRolesNombre"></label>
								<input class="form-control" id="txtAsignacionRolesNombre" runat="server">
								<asp:RegularExpressionValidator ID="revNombre" runat="server" ControlToValidate="txtAsignacionRolesNombre"
									ValidationExpression="^([\w\s]{1,50})$" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
							</div>
						</div>
						<div class="col-md-2" style="padding-top: 25px;">
							<div class="form-group">
								<button id="btnAsignacionRolesNuevo" runat="server" type="button" class="btn btn-success"></button>
							</div>
						</div>
						<div class="col-md-2">
							<div class="form-group">
								<button id="btnAsignacionRolesModificarAsignacion" runat="server" type="button" class="btn btn-success"></button>
							</div>
						</div>
						<div class="col-md-4">
							<div id="divAsignacionRolesAlerta" runat="server" role="alert"></div>
						</div>
					</div>
				</div>
			</div>
		</div>
	</body>
	</html>
</asp:Content>

