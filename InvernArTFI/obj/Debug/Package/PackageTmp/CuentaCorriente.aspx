<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CuentaCorriente.aspx.vb" Inherits="InvernArTFI.CuentaCorriente" MasterPageFile="~/Master.Master" %>

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
		<div id="divCuentaCorrienteControles" runat="server" class="container">
			<div class="vertical-center-row">
				<div class="row">
					<div class="col-md-12">
						<asp:GridView ID="gvCuentaCorriente" runat="server" CssClass="table table-striped table-bordered table-hover" AllowPaging="True" PageSize="12">
							<Columns>
								<asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" Visible="false" />
								<asp:BoundField DataField="IdCliente" HeaderText="IdCliente" SortExpression="IdCliente" Visible="false" />
								<%--<asp:BoundField DataField="IdFactura" HeaderText="IdFactura" SortExpression="IdFactura" />--%>
								<asp:TemplateField SortExpression="IdFactura" HeaderText="IdFactura">
									<ItemTemplate>
										<asp:LinkButton ID="IdFactura" runat="server" Text='<%# Bind("IdFactura")%>' 
											CommandArgument='<%# Eval("IdFactura")%>' OnClick="IdFactura_Click"></asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateField>
								<%--<asp:BoundField DataField="IdNotaCredito" HeaderText="IdNotaCredito" SortExpression="IdNotaCredito" />--%>
								<asp:TemplateField SortExpression="IdNotaCredito" HeaderText="IdNotaCredito">
									<ItemTemplate>
										<asp:LinkButton ID="IdNotaCredito" runat="server" Text='<%# Bind("IdNotaCredito")%>' 
											CommandArgument='<%# Eval("Id")%>' OnClick="IdNotaCredito_Click"></asp:LinkButton>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:BoundField DataField="Motivo" HeaderText="Motivo" SortExpression="Motivo" />
								<asp:BoundField DataField="Debito" HeaderText="Debito" SortExpression="Debito" />
								<asp:BoundField DataField="Credito" HeaderText="Credito" SortExpression="Credito" />
								<asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="Fecha" />
							</Columns>
						</asp:GridView>
					</div>
				</div>
			</div>
		</div>
	</body>
	</html>
</asp:Content>
