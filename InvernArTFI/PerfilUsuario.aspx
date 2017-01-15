<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PerfilUsuario.aspx.vb" Inherits="InvernArTFI.PerfilUsuario" MasterPageFile="~/Master.Master" %>

<%@ MasterType VirtualPath="~/Master.Master" %>
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
		<div id="divPerfilUsuarioControles" runat="server" class="container">
			<div class="vertical-center-row">
				<div class="row">
					<div class="col-md-6">
						<div class="form-group">
							<label id="lblPerfilUsuarioUsuarios" runat="server" class="control-label" for="gvPerfilUsuarioUsuarios"></label>
							<div class="scrolling-table-container" style="overflow-y: scroll; height: 430px;">
								<asp:GridView ID="gvPerfilUsuarioUsuarios" runat="server" CssClass="table table-striped table-bordered table-hover">
									<Columns>
										<asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" Visible="False" />
										<asp:BoundField DataField="CorreoElectronico" HeaderText="Correo Electronico" SortExpression="CorreoElectronico">
											<HeaderStyle Wrap="False" />
											<ItemStyle Wrap="False" />
										</asp:BoundField>
										<asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre">
											<HeaderStyle Wrap="False" />
											<ItemStyle Wrap="False" />
										</asp:BoundField>
										<asp:BoundField DataField="Apellido" HeaderText="Apellido" SortExpression="Apellido">
											<HeaderStyle Wrap="False" />
											<ItemStyle Wrap="False" />
										</asp:BoundField>
										<asp:CommandField ButtonType="Image" SelectImageUrl="~/images/15_Tick_16x16.png" ShowSelectButton="True" ItemStyle-HorizontalAlign="Center" />
									</Columns>
								</asp:GridView>

							</div>
						</div>
					</div>
					<div class="col-md-6">
						<div class="form-group">
							<label id="lblPerfilUsuarioRoles" runat="server" class="control-label" for="gvPerfilUsuarioRoles"></label>
							<div class="scrolling-table-container" style="overflow-y: scroll; height: 430px;">
								<asp:GridView ID="gvPerfilUsuarioRoles" runat="server" CssClass="table table-striped table-bordered table-hover">
									<Columns>
										<asp:TemplateField ItemStyle-HorizontalAlign="Center">
											<ItemTemplate>
												<asp:CheckBox ID="chkPerfilUsuarioSeleccionado" runat="server" CommandName="Select" />
											</ItemTemplate>
										</asp:TemplateField>
										<asp:BoundField DataField="Codigo" HeaderText="Codigo" SortExpression="Codigo" ReadOnly="true">
											<HeaderStyle Wrap="False" />
											<ItemStyle Wrap="False" />
										</asp:BoundField>
										<asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre">
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
						<div class="col-md-2 col-md-offset-6">
							<div class="form-group">
								<button id="btnPerfilUsuarioModificarAsignacion" runat="server" type="button" class="btn btn-success"></button>
							</div>
						</div>
						<div class="col-md-4">
							<div id="divPerfilUsuarioAlerta" runat="server" role="alert"></div>
						</div>
					</div>
				</div>
			</div>
		</div>
	</body>
	</html>
</asp:Content>
