<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="GestionCatalogo.aspx.vb" Inherits="InvernArTFI.GestionCatalogo" MasterPageFile="~/Master.Master" %>

<%@ MasterType VirtualPath="~/Master.Master" %>
<asp:Content ID="Main" runat="server" ContentPlaceHolderID="Main">
	<!DOCTYPE html>

	<html>
	<head>
		<script src="Scripts/jquery-1.9.1.min.js"></script>
		<script src="Scripts/bootstrap.min.js"></script>
		<link href="Content/simple-sidebar.css" rel="stylesheet" />
		<link href="Content/Catalogo.css" rel="stylesheet" />
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<title></title>
	</head>
	<body>
		<div id="divGestionCatalogoControles" class="container" runat="server">
			<div class="vertical-center-row">
				<div class="row">
					<div class="col-md-12">
						<div class="scrolling-table-container" style="overflow-y: scroll; height: 430px; width: 1000px;">
							<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
								<ContentTemplate>
									<asp:GridView ID="gvGestionCatalogoProductos" runat="server" CssClass="table table-striped table-bordered table-hover">
										<Columns>
											<asp:TemplateField ItemStyle-HorizontalAlign="Center">
												<ItemTemplate>
													<asp:CheckBox ID="chkAsignacionRolesSeleccionado" runat="server" CommandName="Select" />
												</ItemTemplate>
												<ItemStyle HorizontalAlign="Center" />
											</asp:TemplateField>
											<asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" Visible="false" />
											<asp:BoundField DataField="Codigo" HeaderText="Codigo" SortExpression="Codigo" ReadOnly="true" />
											<asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" ReadOnly="true" />
											<asp:BoundField DataField="Especificacion" HeaderText="Especificacion" SortExpression="Especificacion" 
												ReadOnly="true" />
											<asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario" SortExpression="PrecioUnitario" 
												ReadOnly="true" />
											<asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo" ReadOnly="true" />
											<asp:BoundField DataField="Imagen" HeaderText="Imagen" SortExpression="Imagen" Visible="False" />
											<asp:TemplateField HeaderText="Orden" SortExpression="Orden">
												<ItemTemplate>
													<asp:TextBox ID="txtGestionCatalogoOrden" runat="server" Width="30px"></asp:TextBox>
													<asp:RegularExpressionValidator ID="revOrden" ErrorMessage="*" runat="server"
														ControlToValidate="txtGestionCatalogoOrden"
														ValidationExpression="^[0-9]{0,4}$"
														ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator>
												</ItemTemplate>
											</asp:TemplateField>
										</Columns>
									</asp:GridView>
								</ContentTemplate>
								<Triggers>
									<asp:AsyncPostBackTrigger ControlID="btnGestionCatalogoAceptarCambios" EventName="ServerClick" />
								</Triggers>
							</asp:UpdatePanel>
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-md-2">
						<div class="form-group">
							<button id="btnGestionCatalogoAceptarCambios" runat="server" type="button" class="btn btn-success"></button>
						</div>
					</div>
					<div class="col-md-6">
						<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
							<ContentTemplate>
								<div id="divGestionCatalagoAlerta" runat="server" role="alert"></div>
							</ContentTemplate>
						</asp:UpdatePanel>
					</div>
				</div>
			</div>
		</div>
	</body>
	</html>
</asp:Content>
